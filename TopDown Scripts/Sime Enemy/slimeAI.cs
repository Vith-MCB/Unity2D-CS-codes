using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeAI : MonoBehaviour
{
    private Rigidbody2D slimeRb;
    private Collider2D slimeCollider;

    void Start()
    {
        slimeRb = GetComponent<Rigidbody2D>();
        slimeCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            Debug.Log("Player hit");
        }
    }
}
