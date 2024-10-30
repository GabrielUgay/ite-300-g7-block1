using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public GameObject playPanel;
    public GameObject mainMenu;
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI errorMessage;
    public GameObject notificationPanel;
    public TextMeshProUGUI notificationText;
    public GameObject optionsPanel;
    public GameObject helpPanel;

    public AudioSource mainMenuMusicSource;
    public AudioSource notificationAudioSource;

    private string apiUrl = "http://127.0.0.1:8000/api/leaderboard";

    // FOR PLAY BUTTON TO PROCEED IN THE PLAY PANEL
    public void OnPlayButtonClick()
    {
        mainMenu.SetActive(false);
        playPanel.SetActive(true);
    }

    // HANDLE IN OPTION BUTTON
    public void OnOptionsButtonClick()
    {
        mainMenu.SetActive(false);
        optionsPanel.SetActive(true);
    }

    // FOR BACK BUTTON OF OPTION PANEL THAT WILL GO BACK TO MAIN MENU 
    public void OnBackFromOptionsButtonClick()
    {
        optionsPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    // Method to show the Help Panel from the Options Panel
    public void ShowHelpPanel()
    {
        helpPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    // FOR BACK BUTTON OF HELP PANEL THAT WILL GO BACK TO OPTION PANEL
    public void OnBackFromHelpButtonClick()
    {
        helpPanel.SetActive(false);    // Deactivates the Help Panel
        optionsPanel.SetActive(true);  // Reactivates the Options Panel
    }

    // HANDLE THE START BUTTON
    public void OnStartButtonClick()
    {
        string playerName = playerNameInput.text.Trim();

        // Validate player name input 
        if (string.IsNullOrEmpty(playerName))
        {
            errorMessage.text = "Please enter a name.";
            return;
        }

        if (!char.IsUpper(playerName[0]))
        {
            errorMessage.text = "First letter must be uppercase.";
            return;
        }

        if (System.Text.RegularExpressions.Regex.IsMatch(playerName, @"[^a-zA-Z]"))
        {
            errorMessage.text = "No special characters allowed.";
            return;
        }
        errorMessage.text = "";

        // Stop the main menu music when the player starts the game
        if (mainMenuMusicSource.isPlaying)
        {
            mainMenuMusicSource.Stop();
        }

        // Save player name and display the notification
        StartCoroutine(SavePlayerNameAndShowNotification(playerName));
    }

    public void ClosePlayPanel()
    {
        playPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    private IEnumerator SavePlayerNameAndShowNotification(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);

        string jsonData = JsonUtility.ToJson(new { player_name = playerName, score = 0 });

        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, jsonData))
        {
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage.text = $"Error saving name: {www.error}";
                Debug.LogError($"Error saving name: {www.error}");
            }
            else
            {
                playPanel.SetActive(false);
                ShowNotification("Someone is approaching you...");

                yield return new WaitForSeconds(5);

                notificationAudioSource.Stop();

                SceneManager.LoadScene("GameScene");
            }
        }
    }

    // Show notification message and play sound
    private void ShowNotification(string message)
    {
        notificationText.text = message;
        notificationPanel.SetActive(true);

        // Play the notification sound
        if (!notificationAudioSource.isPlaying)
        {
            notificationAudioSource.Play();
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
