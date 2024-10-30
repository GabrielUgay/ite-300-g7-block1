using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{

    private ScoreSystem scoreSystem;

    void Start()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>();
    }
    // Called when the Replay button is clicked
    public void ReplayGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called when the Main Menu button is clicked
    public void GoToMainMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }

    // Called when the Leaderboard button is clicked
    public void GoToLeaderboard()
    {

    }
}