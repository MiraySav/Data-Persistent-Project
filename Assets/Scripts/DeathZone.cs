using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        if (!(balls.Length > 1))
        {
        Manager.GameOver();
        }
        Destroy(other.gameObject);
    }
}