using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI playerHighScoreText;
    public string inputName;
    public void ReadStringInput(string inputName)
    {
        this.inputName = inputName;
        DataManager.Instance.playerName = inputName;
    }

    void Start()
    {
        playerHighScoreText.text = $"Best score : {DataManager.Instance.playerHighScoreName} : {DataManager.Instance.playerHighScore}";
    }

    // ----------- START & EXIT ----------
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
