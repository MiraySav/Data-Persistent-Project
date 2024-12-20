using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    public Brick BrickPrefab;
    public int LineCount = 6;
    public bool isSticky;
    public Rigidbody Ballrb;
    public Ball ball;
    public GameObject paddle;

    public Text ScoreText;
    public Text playerNameText;
    public GameObject GameOverText;
    public Text highScoreText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;
    private bool isMultipleBallsActive;


    // Start is called before the first frame update
    void Start()
    {
        isMultipleBallsActive = false;
        isSticky = false;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            int indexOfPowerup = Random.Range(0, perLine);

            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
                if (x == indexOfPowerup)
                {

                    brick.GetComponent<Brick>().powerup = SharedFiles.Powerups.sticky;
                    //brick.GetComponent<Brick>().powerup = SharedFiles.Powerups.threeBalls;

                }

            }
        }

        UpdateHighScore();
    }
    public void OnThreeBallsActivate()
    {
        ball.SpawnMultipleBalls(2); 
        StartCoroutine(ThreeBallsTimer());
    }

    IEnumerator ThreeBallsTimer()
    {
        isMultipleBallsActive = true ;
        yield return new WaitForSeconds(2);
        isMultipleBallsActive = false ;
    }

    private void Update()
    {
        if (!m_Started || isSticky)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSticky = false;
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ballrb.transform.SetParent(null);
                Ballrb.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }


    }
    public void OnStick()
    {
        isSticky = true;

    }
    void UpdateHighScore()
    {
        if (NameManager.instance != null )
        {
            if(m_Points > NameManager.instance.highScore)
            {
            NameManager.instance.highScore = m_Points;
            NameManager.instance.playerName = NameManager.instance.lastPlayerName;
            NameManager.instance.SaveHighScore();
            }
            highScoreText.text = "Best Score: " + NameManager.instance.playerName + ": " + NameManager.instance.highScore;
        } else
        {
            highScoreText.text = "Best Score: 0";
        }
    }
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        if (isMultipleBallsActive == true)
        {
            m_GameOver = false;
        }
        else if (isMultipleBallsActive == false) 
        { m_GameOver = true; }
        UpdateHighScore();
        GameOverText.SetActive(true);
    }
}
