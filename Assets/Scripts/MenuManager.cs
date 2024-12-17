using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    // Start is called before the first frame update
    private void Start()
    {
        if (NameManager.instance != null && nameInputField != null)
        {
            nameInputField.text = NameManager.instance.lastPlayerName;
        }
        
    }
    public void OnEndEdit(string e)
    {
        if (NameManager.instance != null)
        {
            NameManager.instance.lastPlayerName = e;
        }
    }

    public void StartNew()
    {
        NameManager.instance.SaveLastNameScore();
        SceneManager.LoadScene(1); 
    }

    public void Exit()
    { 
        NameManager.instance.SaveLastNameScore();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
