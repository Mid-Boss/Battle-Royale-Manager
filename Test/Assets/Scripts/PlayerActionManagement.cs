using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class PlayerActionManagement : MonoBehaviour {

    Vector3 direction;
    private int speed = 1;
    public float timeToAction = 1.0f;
    public float count = 0;
    public int circleDistance;
    IAstarAI ai;
    GameObject deathCircle;
    public GameObject bullet;
    public int bulletSpeed = 2;
    int weapon = 0;
    bool isShooting = false;
    float randX;
    float randY;
    float width;
    float height;
    public int detectRange = 15;
    GameObject detectedEnemy;
    Camera firstCamera;

    // Use this for initialization
    void Start () {
        //Pathing Component
        ai = GetComponent<IAstarAI>();
        //Camera parameters
        firstCamera = Camera.main;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = firstCamera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        width = 2.0f * camHalfWidth;
        height = 2.0f * camHalfHeight;
    }
	
	// Update is called once per frame
	void Update () {
        if (weapon == 0)
        {
            MoveToLoot();
        }
        else
        {
            detectedEnemy = EngageEnemy();
            if (!isShooting && detectedEnemy != null)
            {
                isShooting = true;
                ShootEm(detectedEnemy);
                print("Shooting");
            }
            count += Time.deltaTime;
            if (count >= timeToAction)
            {
                MoveToCircle();
                count = 0;
            }
        }
	}

    void MoveToCircle ()
    {
        deathCircle = GameObject.FindGameObjectWithTag("Circle");
        SpriteRenderer spriteRenderer = deathCircle.GetComponent<SpriteRenderer>();
        float circleX = deathCircle.transform.position.x;
        float circleY = deathCircle.transform.position.y;
        float circleRadius = spriteRenderer.sprite.bounds.max.x * deathCircle.transform.localScale.x;
        float a = UnityEngine.Random.Range(0.0f, 1.0f) * 2.0f * (float)Math.PI;
        float r = circleRadius * (float)Math.Sqrt(UnityEngine.Random.Range(0.0f, 1.0f));
        float destX = r * (float)Math.Cos(a) + circleX;
        float destY = r * (float)Math.Sin(a) + circleY;
        Mathf.Clamp(destX, 0 - width / 2, 0 + width / 2);
        Mathf.Clamp(destY, 0 - height / 2, 0 + height / 2);
        ai.destination = new Vector3(destX, destY, -1);
        ai.SearchPath();
    }

    void MoveToLoot ()
    {
        GameObject[] weaponLocations = GameObject.FindGameObjectsWithTag("Weapon");
        Transform weaponDest = weaponLocations[0].transform;
        for (int i = 0; i < weaponLocations.Length; i++)
        {
            if (Vector3.Distance(this.transform.position, weaponDest.position) > Vector3.Distance(this.transform.position, weaponLocations[i].transform.position))
            {
                weaponDest = weaponLocations[i].transform;
            }
        }
        ai.destination = weaponDest.position;
        ai.SearchPath();
    }

    private void OnTriggerEnter(Collider collision)
    {
        print("Weapon Collision");
        if (collision.gameObject.tag == "Weapon")
        {
            weapon = 1;
        }
    }

    GameObject EngageEnemy ()
    {
        //GameObject[] enemy = GameObject.FindGameObjectsWithTag("Player");
        List<GameObject> enemyList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].gameObject == gameObject)
            {
                enemyList.RemoveAt(i);
            }
        }
        GameObject fightEnemy = null;
        float playerDistance = Vector3.Distance(enemyList[0].transform.position, transform.position);

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (Vector3.Distance(enemyList[i].transform.position, transform.position) <= playerDistance)
            {
                playerDistance = Vector3.Distance(enemyList[i].transform.position, transform.position);
            }
            if (playerDistance <= detectRange)
            {
                fightEnemy = enemyList[i];
            }
        }

        return fightEnemy;
    }

    void ShootEm (GameObject target)
    {
        BulletMovement bulletScript = bullet.GetComponent<BulletMovement>();
        GameObject clone = Instantiate(bullet, transform.position, transform.rotation);
        bulletScript.target = target.transform;
    }
}
