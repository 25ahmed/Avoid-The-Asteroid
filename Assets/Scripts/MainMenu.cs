using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject HowToPlayDisplay;
    [SerializeField] private GameObject MenuDisplay;

    private void Start()
    {
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus) { return; }

        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        highScoreText.text = $"High Score: {highScore}";
        //"High Score" + highScore.ToString() is also correct, this is just a different way.
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void HowToPlayButton()
    {
        MenuDisplay.gameObject.SetActive(false);
        HowToPlayDisplay.gameObject.SetActive(true);
    }

    public void OkayButton()
    {
        HowToPlayDisplay.gameObject.SetActive(false);
        MenuDisplay.gameObject.SetActive(true);
    }
}
