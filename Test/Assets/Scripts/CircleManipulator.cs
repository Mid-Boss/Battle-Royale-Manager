using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CircleManipulator : MonoBehaviour {

    int scale = 100;
    public float count;
    public float timeToShrink = 30;
    public Vector3 location;
    float randX;
    float randY;
    float width;
    float height;
    Camera firstCamera;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
        CircleScaling(scale);
        firstCamera = Camera.main;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = firstCamera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        width = 2.0f * camHalfWidth;
        height = 2.0f * camHalfHeight;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        count += Time.deltaTime;
        if (count > timeToShrink)
        {
            CircleMovement();
            scale /= 2;
            CircleScaling(scale);
            count = 0;
        }
    }

    void CircleMovement ()
    {
        float circleX = transform.position.x;
        float circleY = transform.position.y;
        float circleRadius = spriteRenderer.sprite.bounds.max.x * transform.localScale.x / 2;
        float a = UnityEngine.Random.Range(0.0f, 1.0f) * 2.0f * (float)Math.PI;
        float r = circleRadius * (float)Math.Sqrt(UnityEngine.Random.Range(0.0f, 1.0f));
        float destX = r * (float)Math.Cos(a) + circleX;
        float destY = r * (float)Math.Sin(a) + circleY;
        Mathf.Clamp(destX, 0 - width / 2, 0 + width / 2);
        Mathf.Clamp(destY, 0 - height / 2, 0 + height / 2);
        transform.position = new Vector3(destX, destY, -1);
    }

    void CircleScaling (int scale)
    {
        this.transform.localScale = new Vector3(scale, scale, 1);
    }
}
