using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSDisplay : MonoBehaviour
{
    public Text fpsText; 
    private float deltaTime = 0.0f;
    private float[] fpsArray;
    private int fpsIndex;
    private float totalFPS;
    public int sampleSize = 60; 
    private float averageFPS;

    public float updateInterval = 0.5f; 
    private float nextUpdateTime = 0f; 

    void Start()
    {
        fpsArray = new float[sampleSize];
        fpsIndex = 0;
        totalFPS = 0f;
        nextUpdateTime = Time.time + updateInterval; 
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float currentFPS = 1.0f / deltaTime;

            totalFPS -= fpsArray[fpsIndex];
            fpsArray[fpsIndex] = currentFPS;
            totalFPS += currentFPS;
            fpsIndex = (fpsIndex + 1) % sampleSize;
            averageFPS = totalFPS / sampleSize;
        }

        if (Time.time >= nextUpdateTime)
        {
            fpsText.text = string.Format("FPS: {0:0} | Sredniy FPS: {1:0}", Time.timeScale > 0 ? (1.0f / deltaTime) : 0, averageFPS);
            nextUpdateTime = Time.time + updateInterval; 
        }
    }
}
