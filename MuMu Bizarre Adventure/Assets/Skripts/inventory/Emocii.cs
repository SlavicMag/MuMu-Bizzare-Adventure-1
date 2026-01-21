using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emocii : MonoBehaviour
{
    public GameObject Emventory;
    private bool EmventoryOn;

    private void Start()
    {
        EmventoryOn = true;
    }
    public void Chest()
    {
        if (EmventoryOn == false)
        {
            EmventoryOn = true;
            Emventory.SetActive(true);
        }

        else if (EmventoryOn == true)
        {
            EmventoryOn = false;
            Emventory.SetActive(false);
        }
    }
}
