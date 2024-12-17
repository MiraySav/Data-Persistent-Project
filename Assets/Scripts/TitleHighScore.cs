using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleHighScore : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        if(NameManager.instance != null)
        {
        textMesh.text = "Best Score: " + NameManager.instance.playerName + ": " + NameManager.instance.highScore;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
