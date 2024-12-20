//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private bool isStuck = false;
    private Transform paddle;
    public MainManager manager;
    private bool shouldStickAgain;


    void Start()
    {
        shouldStickAgain = true;
        m_Rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isStuck && Input.GetKeyDown(KeyCode.Space))
        {
            isStuck = false;
            shouldStickAgain = false;
            Invoke("SetShouldStickAgainToTrue", 2f);
            transform.SetParent(null);
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    
    public void StickToPlatform(Transform newPlatform)
    {
            isStuck = true;
            paddle = newPlatform;
            transform.SetParent(paddle);
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.isKinematic = true;
    }

    public void SpawnMultipleBalls(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newBall = Instantiate(gameObject, transform.position, Quaternion.identity);
            Rigidbody newBallRb = newBall.GetComponent<Rigidbody>();

            if (newBallRb != null)
            {
                Vector3 randomDirection = m_Rigidbody.velocity + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                randomDirection.Normalize();
                newBallRb.velocity = randomDirection * m_Rigidbody.velocity.magnitude;
            }
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
          bool isPaddleSticky =  collision.gameObject.GetComponent<Paddle>().hasStickyPower;
            if (isPaddleSticky && shouldStickAgain)
            {
                StickToPlatform(collision.transform);
                manager.isSticky = true;
            }
        }
    }

    void SetShouldStickAgainToTrue()
    {
        shouldStickAgain = true;
    }
}
