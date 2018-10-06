using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
     
	}

    public void PlayerMove (Vector3 direction, int speed)
    {
        this.transform.Translate(direction * speed);
    }
}
