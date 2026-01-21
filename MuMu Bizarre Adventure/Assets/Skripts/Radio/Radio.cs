using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{
    public AudioClip[] pesni;
    private AudioSource audioSource;
    private int currentSongIndex;
    public Slider volumeSlider;
    public PlayerKontroller player;
    public bool pause;
    private PlayerData playerData;

    private void Start()
    {
        playerData = SaveSystem.LoadPlayer();
        audioSource = GetComponent<AudioSource>();
        currentSongIndex = 0;
        audioSource.clip = pesni[currentSongIndex];
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    private void Update()
    {
        if(player.health > 0)
        {
            if (!audioSource.isPlaying && !pause)
            {
                PlayNextSong();
            }
        }
    }

    public void TogglePause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            pause = true;
        }
        else 
        {
            audioSource.UnPause();
            pause = false;
        }
    }

    public void PlayNextSong()
    {
        currentSongIndex = (currentSongIndex + 1) % pesni.Length;
        audioSource.clip = pesni[currentSongIndex];
        audioSource.Play();
    }

    public void PlayDextSong()
    {
        if (currentSongIndex > 0)
        {
            currentSongIndex = (currentSongIndex - 1) % pesni.Length;
            audioSource.clip = pesni[currentSongIndex];
            audioSource.Play();
        }
    }

    public void PlayNumberSong(int number)
    {
        currentSongIndex = (number) % pesni.Length;
        audioSource.clip = pesni[currentSongIndex];
        audioSource.Play();
    }

    public void ChangeVolume()
    {
        audioSource.volume = volumeSlider.value;
    }

    public void SwithVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void Unpause()
    {
        audioSource.UnPause();
    }

    public void DoVolume() 
    {
        PlayerKontroller.volumeDo = audioSource.volume;
    }
}
