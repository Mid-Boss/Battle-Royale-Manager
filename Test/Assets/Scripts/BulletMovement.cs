using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public Transform target;
    public GameObject shooterObj;
    public int speed = 10;
    Rigidbody rb;
    Vector3 targetPos;
    float travelDistanceTime = 3.0f;
    float travelDistance = 0.0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPos = target.position;
        MoveToTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
        travelDistance += Time.deltaTime;
        if (travelDistance >= travelDistanceTime)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void MoveToTarget(Transform target)
    {
        Vector3 targetPosition = targetPos;
        Vector3 heading = targetPosition - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (shooterObj != collision.gameObject)
            {
                GameObject.Destroy(collision.gameObject);
                GameObject.Destroy(gameObject);
            }
        }
    }
}
