using UnityEngine;
using Cinemachine;

public class ScaleCaveraController : MonoBehaviour
{
    public CinemachineVirtualCamera[] virtualCameras;
    public float targetAspectRatio = 16f / 9f;
    public float targetOrthographicSize = 5f;
    public Color backGroundColor;

    void Start()
    {
        AdjustCameras();
        SmenaColor();
    }

    void AdjustCameras()
    {
        float currentAspectRatio = (float)Screen.width / (float)Screen.height;

        if (currentAspectRatio > targetAspectRatio)
        {
            float aspectRatioFactor = currentAspectRatio / targetAspectRatio;

            foreach (var virtualCamera in virtualCameras)
            {
                virtualCamera.m_Lens.OrthographicSize = targetOrthographicSize / aspectRatioFactor;
            }
        }
        else
        {
            foreach (var virtualCamera in virtualCameras)
            {
                virtualCamera.m_Lens.OrthographicSize = targetOrthographicSize;
            }
        }
    }

    public void SmenaColor()
    {
        Camera camera = Camera.main;
        if(camera != null)
        {
            camera.backgroundColor = backGroundColor;
        }
    }
}
