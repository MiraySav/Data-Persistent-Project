using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class PowerupSpawner : MonoBehaviour
{
    public GameObject blueExtraBallsPowerup;
    public GameObject pinkStickyBallsPowerup;
    public MainManager mainManager;
    public Ball ball;
    public Paddle paddle;
    public float effectTime = 5f;
    public float powerupSpeed = 0.3f;
    private float spawnRangeX = 1.6f;
    private float spawnRangeY = 5f;
    private float nextSpawnTime;
    bool hasPowerup = false;
    // Start is called before the first frame update
    void Start() 
    {
        nextSpawnTime = Time.deltaTime + Random.Range(2f, 8f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.deltaTime >= nextSpawnTime)
        {

            GameObject powerUpToSpawn = Random.value > 0.5f ? blueExtraBallsPowerup : pinkStickyBallsPowerup;

            Instantiate(powerUpToSpawn,GenerateSpawnPosition(), powerUpToSpawn.transform.rotation);
            nextSpawnTime = Time.deltaTime + Random.Range(5f, 15f);
        }
       
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(spawnPosX, spawnRangeY, 0) * powerupSpeed;
        return spawnPos;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.CompareTag("Player") && hasPowerup)
        {
            for (int i = 0; i < 2; i++) // Create 2 additional balls
            {
                Instantiate(blueExtraBallsPowerup, other.transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Destroy the power-up
        }
        else if (other.CompareTag("Player") && !hasPowerup)
        {
            Ball ball = FindObjectOfType<Ball>();
            Paddle paddle = FindObjectOfType<Paddle>();

            if (ball != null && paddle != null)
            {
                ball.StickToPlatform(paddle.transform);
            }

            Destroy(gameObject);
        }
    }

}
