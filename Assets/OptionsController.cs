using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public AudioSource mainMenuMusicSource;
    public Slider musicVolumeSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        mainMenuMusicSource.volume = savedVolume;
        musicVolumeSlider.value = savedVolume;

        // Add listener to the slider to update the volume in real-time
        musicVolumeSlider.onValueChanged.AddListener(SetMainMenuMusicVolume);
    }

    public void SetMainMenuMusicVolume(float volume)
    {
        mainMenuMusicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}
