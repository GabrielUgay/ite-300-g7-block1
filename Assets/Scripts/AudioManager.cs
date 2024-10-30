using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Sources ------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("------ Audio Clips ------")]
    public AudioClip backgroundMusic;
    public AudioClip runningSound;
    public AudioClip jumpSound;

    [Header("------ Volume Settings ------")]
    [SerializeField] private Slider BGMusicSlider;
    [SerializeField] private Slider SFXSlider;

    void Start()
    {
        // Load saved volume settings
        float savedBGMVolume = PlayerPrefs.GetFloat("Music", 0.5f);
        float savedSFXVolume = PlayerPrefs.GetFloat("SFX", 0.5f);

        // Set the initial volume for the audio sources
        musicSource.volume = savedBGMVolume;
        sfxSource.volume = savedSFXVolume;

        // Set the slider values
        BGMusicSlider.value = savedBGMVolume;
        SFXSlider.value = savedSFXVolume;

        // Play background music and set it to loop
        PlayBackgroundMusic();

        // Add listener to sliders to update volume
        BGMusicSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void PlayBackgroundMusic()
    {
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void SetBGMVolume(float volume)
    {
        musicSource.volume = volume; // Adjust the background music volume
        PlayerPrefs.SetFloat("Music", volume); // Save the BGM volume
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume; // Adjust the sound effects volume
        PlayerPrefs.SetFloat("SFX", volume); // Save the SFX volume
    }

    // Method to play running sound
    public void PlayRunningSound()
    {
        if (sfxSource.clip != runningSound || !sfxSource.isPlaying) // Prevent overlapping running sound
        {
            sfxSource.clip = runningSound;
            sfxSource.Play();
        }
    }

    // Method to stop running sound
    public void StopRunningSound()
    {
        if (sfxSource.clip == runningSound && sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }

    // Method to play jump sound
    public void PlayJumpSound()
    {
        sfxSource.PlayOneShot(jumpSound);  // Play jump sound once
    }

    public void StopBackgroundMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    // to stop all sound effects
    public void StopAllSoundEffects()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }
}
