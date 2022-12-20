using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    public const string HighScoreKey = "HighScore";

    private bool shouldCount = true;
    private float score;

    // Update is called once per frame
    void Update()
    {
        if (!shouldCount) { return; }

        score += Time.deltaTime * scoreMultiplier;

        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }

    public void StartTimer()
    {
        shouldCount = true;
    }

    public int EndTimer()
    {
        shouldCount = false;

        scoreText.text = string.Empty;

        return Mathf.FloorToInt(score);
    }
}
