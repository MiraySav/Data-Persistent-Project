using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private bool isStuck = false;
    private Transform paddle;


    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isStuck && Input.GetKeyDown(KeyCode.Space))
        {
            isStuck = false;
            transform.SetParent(null);
            GetComponent<Rigidbody>().isKinematic = false; 
        }
    }
    public void StickToPlatform(Transform newPlatform)
    {
        isStuck = true;
        paddle = newPlatform;
        transform.SetParent(paddle); 
        GetComponent<Rigidbody>().isKinematic = true;
    }
    private void OnCollisionExit(Collision other)
    {
        var velocity = m_Rigidbody.velocity;
        
        //after a collision we accelerate a bit
        velocity += velocity.normalized * 0.01f;
        
        //check if we are not going totally vertically as this would lead to being stuck, we add a little vertical force
        if (Vector3.Dot(velocity.normalized, Vector3.up) < 0.1f)
        {
            velocity += velocity.y > 0 ? Vector3.up * 0.5f : Vector3.down * 0.5f;
        }

        //max velocity
        if (velocity.magnitude > 3.0f)
        {
            velocity = velocity.normalized * 3.0f;
        }

        m_Rigidbody.velocity = velocity;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Ball hit the paddle!");
    //    }
    //}
}
