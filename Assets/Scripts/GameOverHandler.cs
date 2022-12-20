using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueButton;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpawner asteroidSpawner;

    public bool isContinue = false;
    public bool isPlayAgain = false;
    public bool isReturnToMenu = false;


    public void EndGame()
    {
        asteroidSpawner.enabled = false;

        int finalScore = scoreSystem.EndTimer();
        gameOverText.text = $"Your Score: {finalScore}";

        gameOverDisplay.gameObject.SetActive(true);

        AdManager.Instance.LoadAd();
    }

    public void PlayAgain()
    {
        
        isPlayAgain = true;
        isContinue = false;
        isReturnToMenu = false;
        
        AdManager.Instance.ShowAd(this);
       // SceneManager.LoadScene(1);
    }

    public void ContinueButton()
    {
        
        isContinue = true;
        isPlayAgain = false;
        isReturnToMenu = false;
        
        AdManager.Instance.ShowAd(this);

        continueButton.interactable = false;
    }

    public void ReturnToMenu()
    {
        
        isReturnToMenu = true;
        isPlayAgain = false;
        isContinue = false;
        
        AdManager.Instance.ShowAd(this);

        SceneManager.LoadScene(0);
    }

    public void ContinueGame()
    {
        scoreSystem.StartTimer();

        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        asteroidSpawner.enabled = true;

        gameOverDisplay.gameObject.SetActive(false);
    }
}
