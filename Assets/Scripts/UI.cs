using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class UI : MonoBehaviour
{
    [Header("ints")]
    [SerializeField] public int score;
    [SerializeField] public int highScore;
    [SerializeField] public int difficulty;
    [Header("referables")]
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject specialFood;
    [SerializeField] public GameObject food2;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI highScoreText;

    public void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Snake");
    }
    public void CloseGame()
    {
        Application.Quit();
    }

    public void Update()
    {
        /*score = player.GetComponent<Player>()._segments.Count;*/
        if (score>=highScore)
        {
            highScore = score;
        }
        scoreText.text ="Score: " + score.ToString();
        highScoreText.text = "HighScore: " + highScore.ToString();

        if (score>=5)
        {
            food2.SetActive(true);
            if (score>10)
            {
                specialFood.SetActive(true);
            }
        }
    }
}
