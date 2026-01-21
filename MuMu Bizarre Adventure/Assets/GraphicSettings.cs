using UnityEngine;

public class GraphicSettings : MonoBehaviour
{
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
