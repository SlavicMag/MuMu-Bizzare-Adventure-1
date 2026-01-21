using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject nastroykiMenuUI;
    public AudioSource[] audioSources;
    public Animator[] animators;

    [Header("Disclaymer")]
    public GameObject disclaymer;
    public GameObject fpsText;
    public int maxfps;
    public PlayerKontroller player;
    public PlayerAttack attacka;

    public PlayableDirector timelineDirector;
    public GameObject timelineManager;
    private PlayerData playerData;
    public AudioSource audioStopStart;
    public Vector3 initialValue;

    public Joystick[] joysticksDvij;
    public int currentDvijJoystikIndex;
    public GameObject[] knopkiDvij;

    public Joystick[] joysticksShoot;
    public int currentShootJoystikIndex;
    public GameObject[] knopkiShoot;

    private void Start()
    {
        playerData = SaveSystem.LoadPlayer();
        if(playerData != null)
        {
            if (playerData.NeProyDech)
            {
                fpsText.SetActive(false);
                disclaymer.SetActive(true);
                Time.timeScale = 0;
                audioStopStart.Pause();
            }
            else
            {
                player.LoadPlayer();
                audioStopStart.UnPause();
                ZagryzkaJostiks();
            }
        }
        else
        {
            fpsText.SetActive(false);
            disclaymer.SetActive(true);
            Time.timeScale = 0;
            audioStopStart.Pause();
        }
        FpsControl(maxfps);
    }
    public void Resume ()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        foreach (var audioSource in audioSources)
        {
            audioSource.UnPause();
        }
        foreach (var animator in animators)
        {
            animator.enabled = true;
        }
    }

    public void Pause ()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        foreach(var audioSource in audioSources)
        {
            audioSource.Pause();
        }
        foreach (var animator in animators)
        {
            animator.enabled = false;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Qutting game...");
        Application.Quit();
    }

    public void PrinimauUsloviya()
    {
        fpsText.SetActive(true);
        player.neProyDesh = false;
        disclaymer.SetActive(false);
        Time.timeScale = 1f;

        player.transform.position = initialValue;
        player.StartCoroutine(player.Zatemnil(1.01f));
        player.SavePlayer();
    }

    public void VhodWNastroyki()
    {
        pauseMenuUI.SetActive(false);
        nastroykiMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void VihodIsNastroyki()
    {
        pauseMenuUI.SetActive(true);
        nastroykiMenuUI.SetActive(false);
        Time.timeScale = 0f;
    }

    public void SwithJoystik()
    {
        currentDvijJoystikIndex++;
        if(currentDvijJoystikIndex >= joysticksDvij.Length)
        {
            currentDvijJoystikIndex = 0;
        }
        ViborJoustik(currentDvijJoystikIndex);
    }

    public void ViborJoustik(int index)
    {
        for (int i = 0; i < joysticksDvij.Length; i++)
        {
            joysticksDvij[i].gameObject.SetActive(i == index);
            player.joystick = joysticksDvij[index];
            knopkiDvij[i].SetActive(i == index);
        }
    }

    public void SwithShootJoystik()
    {
        currentShootJoystikIndex++;
        if (currentShootJoystikIndex >= joysticksShoot.Length)
        {
            currentShootJoystikIndex = 0;
        }
        ViborShootJoustik(currentShootJoystikIndex);
    }

    public void ViborShootJoustik(int indexS)
    {
        for (int i = 0; i < joysticksShoot.Length; i++)
        {
            joysticksShoot[i].gameObject.SetActive(i == indexS);
            player.jostikAttack = joysticksShoot[indexS];
            attacka.joystick = joysticksShoot[indexS];
            Gun[] guns = GameObject.FindObjectsOfType<Gun>(true);
            foreach (var gun in guns)
            {
                if (gun.gunType == Gun.GunType.Player)
                {
                    gun.joystick = joysticksShoot[indexS];
                    Debug.Log("¿ıı‡ı‡ı ƒ‡‡‡‡‡ Œ‰‡‡‡");
                }
            }
            knopkiShoot[i].SetActive(i == indexS);
        }
    }

    public void ZagryzkaJostiks()
    {
        ViborJoustik(playerData.currentDvijJoystikIndex);
        ViborShootJoustik(playerData.currentShootJoystikIndex);
        currentDvijJoystikIndex = playerData.currentDvijJoystikIndex;
        currentShootJoystikIndex = playerData.currentShootJoystikIndex;
    }

    public void FpsControl(int fps)
    {
        Application.targetFrameRate = fps;
    }

    public void NachaloOneTimline()
    {
        timelineManager.SetActive(true);
        timelineDirector.Play();
        audioStopStart.UnPause();
    }
}
