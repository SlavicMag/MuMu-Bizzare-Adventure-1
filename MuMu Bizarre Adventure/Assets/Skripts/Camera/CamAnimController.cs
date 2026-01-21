using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAnimController : MonoBehaviour
{
    private CamController cam;
    private Transform player;

    private void Start()
    {
        cam = FindObjectOfType<CamController>();
        player = GameObject.Find("Player").transform;
    }

    public void changeCameraSize(float size)
    {
        CamController.changeCameraSizeEvent?.Invoke(size);
    }

    public void focusOnObject(int _focus)
    {
        bool focus = _focus > 0;
        Transform followObject = focus ? transform : player;

        CamController.changeFollowTargetEvent?.Invoke(followObject);

    }

    public void changeTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void ShakeCamera(float strength, float time, float fadetime)
    {
        CamController.cameraShake?.Invoke(strength, time, fadetime);
    }
}
