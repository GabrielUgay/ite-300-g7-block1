using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float runSpeed;
    private int jumpCount = 0;
    private bool canJump = true;
    private Animator anim;
    public bool isGameOver = false;
    public GameObject GameOverPanel, scoreText;
    public TMP_Text FinalScoreText, HighScoreText;
    public GameObject jumpscareImage;
    public GameObject PauseButton;
    private AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.PlayBackgroundMusic();
        StartCoroutine("IncreaseGameSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            transform.position += Vector3.right * runSpeed * Time.deltaTime;
        }

        if (rb2d.velocity.y == 0) // // Check if player is on the ground (velocity.y == 0 means no vertical movement)
        {
            anim.SetBool("run", true); // activate the player run kapag nasa ground
            audioManager.PlayRunningSound();
        }
        else
        {
            anim.SetBool("run", false); //mawawala yung takbo kapag tumatalon ang player
        }

        if ((Input.GetKeyDown("space") || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && canJump && !isGameOver)
        {
            rb2d.velocity = Vector3.up * 6.7f; // Jump force
            anim.SetTrigger("jump");
            audioManager.StopRunningSound();
            audioManager.PlayJumpSound();
            jumpCount += 1;

            if (jumpCount == 2)
            {
                canJump = false;
            }
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        anim.SetTrigger("death");
        StopCoroutine("IncreaseGameSpeed");
        audioManager.StopRunningSound();
        audioManager.StopBackgroundMusic();
        audioManager.StopAllSoundEffects();
        ScoreSystem scoreSystem = FindObjectOfType<ScoreSystem>();
        scoreSystem.SaveScore();
        StartCoroutine(PlayJumpscareAndShowGameOverPanel());
    }

    // Reset the jump if the player lands on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            canJump = true;
            anim.SetBool("run", true);
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
        if (collision.gameObject.CompareTag("BottomDetector"))
        {
            GameOver();
        }
    }
    // every 10 seconds the game will speed
    IEnumerator IncreaseGameSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);

            if (runSpeed < 8)
            {
                runSpeed += 0.15f;
            }

            if (GameObject.Find("GroundSpawner").GetComponent<ObstaclesSpawner>().obstacleSpawnInterval > 1)
            {
                GameObject.Find("GroundSpawner").GetComponent<ObstaclesSpawner>().obstacleSpawnInterval -= 0.1f;
            }
        }
    }

    IEnumerator PlayJumpscareAndShowGameOverPanel()
    {
        
        yield return new WaitForSeconds(1.5f); // adjust timing sa death animation ng player

        
        jumpscareImage.SetActive(true); // activate the jumpscare image
        Debug.Log("Playing jumpscare sound");
        jumpscareImage.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1.9f);  // duration ng jumpscare

        jumpscareImage.SetActive(false); // // deactivate jumpscare image pagkatapos ipakita

        GameOverPanel.SetActive(true); // activate the game over panel after the jumpscare
        scoreText.SetActive(false);
        PauseButton.SetActive(false);

        FinalScoreText.text = "Score: " + GameObject.Find("ScoreDetector").GetComponent<ScoreSystem>().score;
        HighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    }

}
