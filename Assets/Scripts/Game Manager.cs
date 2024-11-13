using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> garbage;
    public float spawnRate = 2.5f;
    private int p1Score;
    private int p2Score;
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI winnerText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject titleScreen;
    public float timeLeft = 90.0f;
    private float spawnMaxX = 0;
    private float spawnMinX = -4.5f;
    private float spawnMaxY = 4;
    private float spawnMinY = 0.5f;
    private GameObject[] trashCount;
    public int spawnDifficulty;
    private bool restartButtonActive = false;
    // Start is called before the first frame update
    void Start()
    {
       Cursor.visible = false;
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 spawnPos = new Vector3(Random.Range(spawnMinX, spawnMaxX), Random.Range(spawnMinY, spawnMaxY), 0);
            int index = Random.Range(0, garbage.Count);
            Instantiate(garbage[index], spawnPos, garbage[index].transform.rotation);

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
        if (spawnRate == 2.5) 
        {
            spawnDifficulty = 1;
        }
        else if (spawnRate == (2.5 / 2))
        {
            spawnDifficulty = 2;
        }
        else if (spawnRate == (2.5 / 3)) 
        {
            spawnDifficulty = 3;
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        if (p1Score < p2Score)
        {
            winnerText.gameObject.SetActive(true);
            winnerText.text = "Player 2 wins!";
        }
        else if (p1Score > p2Score)
        {
            winnerText.gameObject.SetActive(true);
            winnerText.text = "Player 1 wins!";
        }
        else if (p1Score == p2Score)
        {
            winnerText.gameObject.SetActive(true);
            winnerText.text = "Tie!";
        }
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        restartButtonActive = true;
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

        if (isGameActive == true) 
        {
            trashCount = GameObject.FindGameObjectsWithTag("Trash");
            if (trashCount.Length > 15)
            {
                GameOver();
            }
            
            timeLeft -= Time.deltaTime;
            timerText.text = "Time: " + timeLeft.ToString("F2");

            }
            if (timeLeft < 0)
            {
                GameOver();
            }
            else if (timeLeft <= 75)
            {
                if (spawnDifficulty == 1)
                {
                    spawnRate = 1.28f;
                }
                else if (spawnDifficulty == 2)
                {
                    spawnRate = 0.8f;
                }
                else if (spawnDifficulty == 3)
                {
                    spawnRate = 0.5f;
                }
            }
            else if (timeLeft <= 80)
            {
                if (spawnDifficulty == 1)
                {
                    spawnRate = 1.6f;
                }
                else if (spawnDifficulty == 2)
                {
                    spawnRate = 0.9f;
                }
                else if (spawnDifficulty == 3)
                {
                    spawnRate = 0.65f;
                }
            }
            else if (timeLeft <= 85)
            {
                if (spawnDifficulty == 1)
                {
                    spawnRate = 2;
                }
                else if (spawnDifficulty == 2)
                {
                    spawnRate = 1;
                }
                else if (spawnDifficulty == 3)
                {
                    spawnRate = 0.75f;
                }
            

            
        }

        if (restartButtonActive == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
            {
                RestartGame();
            }
        }
         
    }
}
