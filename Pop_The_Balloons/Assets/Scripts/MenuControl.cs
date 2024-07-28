using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{

    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderMusic;

    AudioSource musicSound;

    Music music;


    private void Awake()
    {
        music = Music.Instance;
        musicSound = music.GetComponent<AudioSource>();
        sliderMusic.value = SoundLevels.musicLevel;
        sliderSFX.value = SoundLevels.sfxLevel;
    }

    public void ChangeMusicLevel()
    {
        musicSound.volume = sliderMusic.value;
        SoundLevels.musicLevel = sliderMusic.value;
    }
    public void ChangeSFXLevel()
    {
        SoundLevels.sfxLevel = sliderSFX.value;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
