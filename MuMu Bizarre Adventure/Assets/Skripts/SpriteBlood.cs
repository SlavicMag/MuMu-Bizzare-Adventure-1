using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlood : MonoBehaviour
{
    public float fadeSpeed = 0.5f;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color currentColor = spriteRenderer.color;
        currentColor.a = 5f;
        spriteRenderer.color = currentColor;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        while (spriteRenderer.color.a > 0)
        {
            Color newColor = spriteRenderer.color;
            newColor.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.color = newColor;
            yield return null;
        }
        Destroy(gameObject);
    }
}
