using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    public int playerCount = 1;
    public int weaponCount = 10;
    int scale = 100;
    Transform weaponLocation;
    public bool gogo = false;
    public GameObject CD;
    float randX;
    float randY;
    float width;
    float height;
    Camera firstCamera;
    public GameObject player;
    public GameObject weapon;
    Vector3[] spawnArray = new[] { new Vector3(0f, 0f), new Vector3(-20f, -20f), new Vector3(20f, 20f),
        new Vector3(-40f, -40f), new Vector3(40f, 40f), new Vector3(-20f, 20f), new Vector3(20f, -20f), new Vector3(-40f, 40f),
        new Vector3(40f, -40f), new Vector3(0f, 20f)};

    // Use this for initialization
    void Start () {
        firstCamera = Camera.main;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = firstCamera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        width = 2.0f * camHalfWidth;
        height = 2.0f * camHalfHeight;
	}
	
	// Update is called once per frame
	void Update () {
		if (gogo)
        {
            CircleCreation();
            WeaponCreation();
            PlayerCreation();
            gogo = false;
        }
	}

    void CircleCreation ()
    {
        randX = Random.Range(0 - width / 2 + scale/2, 0 + width / 2 - scale / 2);
        randY = Random.Range(0 - height / 2 + scale / 2, 0 + height / 2 - scale / 2);
        CD.transform.position = new Vector3(randX, randY, -1);
        Instantiate(CD);
    }

    void PlayerCreation ()
    {
        for (int i = 0; i < playerCount; i++)
        {
            Instantiate(player, spawnArray[i], new Quaternion(0, 0, 0, 0));
        }
    }

    void WeaponCreation ()
    {
        for (int i = 0; i < weaponCount; i++)
        {
            randX = Random.Range(0 - width / 2, 0 + width / 2);
            randY = Random.Range(0 - height / 2, 0 + height / 2);
            weapon.transform.position = new Vector3(randX, randY, -1);
            Instantiate(weapon);
        }
        
    }
}
