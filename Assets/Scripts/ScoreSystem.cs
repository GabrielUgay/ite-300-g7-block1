using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;
    private const string apiUrl = "http://127.0.0.1:8000/api/leaderboard";

    private PlayerController playerController;
    private int highScore;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateScoreDisplay();
    }

    void Update()
    {
        if (playerController.isGameOver)
        {
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
                SaveScore();
                Debug.Log("New High score is: " + highScore);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            IncrementScore();
        }
    }

    private void IncrementScore()
    {
        score++;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + score;
    }

    public void SaveScore()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        StartCoroutine(SaveScoreCoroutine(playerName));
    }

    private IEnumerator SaveScoreCoroutine(string playerName)
    {
        Debug.Log($"Sending to Laravel - Player Name: {playerName}, Score: {score}");

        PlayerScoreData scoreData = new PlayerScoreData { player_name = playerName, score = score };
        string jsonData = JsonUtility.ToJson(scoreData);

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            www.downloadHandler = new DownloadHandlerBuffer();

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error saving score: " + www.error);
            }
            else
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Server Response: " + responseText);
            }
        }
    }
}

[System.Serializable]
public class PlayerScoreData
{
    public string player_name;
    public int score;
}
