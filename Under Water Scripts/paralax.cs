using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    
    private float length;
    private float startXPos;
    private float startYPos;

    private Transform cam;

    public float paralaxXScale;
    public float paralaxYScale;

    void Start()
    {
        startXPos = transform.position.x;
        startYPos = -17f;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float rePos = cam.transform.position.x * (1 - paralaxXScale);

        float distanceX = cam.transform.position.x * paralaxXScale;
        float distanceY = cam.transform.position.y * paralaxYScale;

        transform.position = new Vector3(startXPos - distanceX, startYPos - distanceY, transform.position.z);

        if(rePos > startXPos + length) 
        {
            startXPos += length;
        }
        else if(rePos < startXPos - length)
        {
            startXPos -= length;
        }

    }
}
