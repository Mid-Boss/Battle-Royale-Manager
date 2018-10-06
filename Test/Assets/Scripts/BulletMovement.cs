using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {

    public Transform target;
    public int speed = 3;


	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        MoveToTarget(target);
    }

    void MoveToTarget (Transform target)
    {
        Vector3 targetPosition = target.position;
        Vector3 heading = targetPosition - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
