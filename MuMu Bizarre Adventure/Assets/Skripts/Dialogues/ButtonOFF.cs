using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOFF : MonoBehaviour
{
    public GameObject NPS;

    public void NEActive()
    {
        NPS.SetActive(false);
    }
}
