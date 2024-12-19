using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;
    //private bool isStuck = false;
    //private Transform paddle;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    public void Update()
    {
        float input = Input.GetAxis("Horizontal");

        Vector3 pos = transform.position;
        pos.x += input * Speed * Time.deltaTime;

        if (pos.x > MaxMovement)
            pos.x = MaxMovement;
        else if (pos.x < -MaxMovement)
            pos.x = -MaxMovement;

        transform.position = pos;


        //if (isStuck && Input.GetKeyDown(KeyCode.Space))
        //{
        //    isStuck = false;
        //    transform.SetParent(null); // Detach from platform
        //    GetComponent<Rigidbody>().isKinematic = false; // Enable physics
        //}
    }
    //public void StickToPlatform(Transform newPlatform) { 
    //    isStuck = true;
    //    paddle = newPlatform;
    //    transform.SetParent(paddle); // Attach to platform
    //    GetComponent<Rigidbody>().isKinematic = true; // Disable physics
    //}
}
