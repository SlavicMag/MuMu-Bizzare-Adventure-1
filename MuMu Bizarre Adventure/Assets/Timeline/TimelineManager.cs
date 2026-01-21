using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    public bool fix = false;
    public PlayerKontroller player;
    public Animator playerAnim;
    public Animator polaAnim;
    public RuntimeAnimatorController playerKontr;
    public PlayableDirector director;
    public GameObject canvas;
    public bool SaveInEnd;
    public Quaternion rotatione;
    void OnEnable()
    {
        playerKontr = playerAnim.runtimeAnimatorController;
        playerAnim.runtimeAnimatorController = null;
        polaAnim.SetBool("active", true);
        canvas.SetActive(false);
        PlayerKontroller.timline = true;
        while (!player.kulak.activeSelf)
        {
            player.SwitchWeapon();
        }
        rotatione = player.transform.rotation;
        player.BaltikaImage.fillAmount = 0;
        player.catscene = true;
    }

    void Update()
    {
        if (director.state != PlayState.Playing && !fix)
        {
            fix = true;
            playerAnim.runtimeAnimatorController = playerKontr;
            canvas.SetActive(true);
            polaAnim.SetBool("active", false);
            PlayerKontroller.timline = false;
            if (SaveInEnd)
            {
                player.SavePlayer();
            }
            player.transform.rotation = rotatione;
            player.catscene = false;
        }
        if (fix)
        {
            director.gameObject.SetActive(false);
        }
    }
}
