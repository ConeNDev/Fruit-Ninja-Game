using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Score Elements")]
    public int score;
    public int highscore;
    public Text scoreText;
    public Text highscoreText;


    [Header("GameOver")]
    public GameObject gameOverPanel;
    public Text gameOverPanelHighScoreText;
    public Text gameOverPanelScoreText;

    [Header("Sounds")]
    public AudioClip[] sliceSounds;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();
        gameOverPanel.SetActive(false);
        GetHighScore();
    }

    private void GetHighScore()
    {
        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreText.text = "Best: " + highscore;
    }

    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();

        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore",score);
            highscoreText.text = "Best: "+score.ToString();
            highscore=score;
        }
    }

    public void OnBombHit()
    {

        Time.timeScale = 0;//po defaultu je 1 i kada je 0 tada se igra zaustavlja
        gameOverPanelScoreText.text = "Score: "+score.ToString();
        gameOverPanelHighScoreText.text = "Best: " + highscore.ToString();
        gameOverPanel.SetActive(true);// kada udari u bombu poziva game over panel
        Debug.Log("Bomb hit");
    }

    public void RestartGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameOverPanel.SetActive(false);

        //i hocu da sve objekte koji su interactable da ih unistim
        //interactable su oni objekti kojima je tag interactable dodeljen(mi smo
        //napravili taj tag)
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Interactable"))
        {
            Destroy(g); 
        }
        Time.timeScale = 1;

    }

    public void PlayRandomSliceSound()
    {
        AudioClip randomSound = sliceSounds[Random.Range(0, sliceSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }
}
