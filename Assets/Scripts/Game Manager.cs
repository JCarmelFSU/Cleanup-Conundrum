using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> garbage;
    private float spawnRate = 2.5f;
    private int p1Score;
    private int p2Score;
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public float timeLeft = 60.0f;
    // Start is called before the first frame update
    void Start()
    {
       // Cursor.visible = false;
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, garbage.Count);
            Instantiate(garbage[index]);

        }
    }

    public void UpdateP1Score(int scoreToAdd)
    {
        p1Score += scoreToAdd;
        p1ScoreText.text = "Score " + p1Score;
    }

    public void UpdateP2Score(int scoreToAdd)
    {
        p2Score += scoreToAdd;
        p2ScoreText.text = "Score " + p2Score;
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        p1Score = 0;
        p2Score = 0;
        UpdateP1Score(0);
        UpdateP2Score(0);
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + timeLeft.ToString("F2");
        if (timeLeft < 0)
        {
            GameOver();
        }

        
    }
}
