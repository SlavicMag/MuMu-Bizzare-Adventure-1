using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variant : MonoBehaviour
{
    public float lifetime;

    void Start()
    {
        Invoke("DestroyVariant", lifetime);
    }

    public void DestroyVariant()
    {
        Destroy(gameObject);
    }
}
