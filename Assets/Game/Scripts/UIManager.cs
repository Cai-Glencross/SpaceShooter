using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public Sprite[] lives;
    [SerializeField]
    public Image livesImageDisplay;
    [SerializeField]
    public GameObject startScreen;

    public Text scoreText;

    public int score = 0;


    public void UpdateLives(int numLives)
    {
        livesImageDisplay.sprite = lives[numLives];
    }

    public void UpdateScore(int scoreIncrement)
    {
        score += scoreIncrement;
        scoreText.text = "Score: " + score;

    }

    public void ShowTitleScreen()
    {
        startScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        startScreen.SetActive(false);
        score = 0;
        UpdateScore(0);
    }


}
