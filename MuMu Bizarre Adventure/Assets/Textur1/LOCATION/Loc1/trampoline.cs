using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampoline : Sounds
{
    [Header("Упругость батута")]
    [Space]
    [SerializeField] private float _foarce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            collision.rigidbody.AddForce(Vector2.up * _foarce, ForceMode2D.Impulse);
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(prijok());
            }
        }
    }

    public IEnumerator prijok()
    {
        PlaySound(0, p1: 1f, p2: 1f);
        yield return new WaitForSeconds(0.5f);
    }
}
