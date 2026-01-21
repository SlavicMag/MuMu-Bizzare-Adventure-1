using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    public GameObject bulletEffect;
    public bool flechka = false;
    public bool ottalkivanie = false;
    public float knockbackForce;
    [SerializeField] bool enemyBullet;

    public Transform attackPos;
    public float attackRange;

    private void Start()
    {
        Invoke("DestroyBullet", lifetime);
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy") && enemyBullet == false)
            {
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
                if (flechka)
                {
                    hitInfo.collider.GetComponent<Enemy>().PlaySound(1, random: true, p1: 1f, p2: 1f);
                    HandleEnemyHit();
                }
                if (ottalkivanie)
                {
                    Rigidbody2D enemyRigidbody = hitInfo.collider.GetComponent<Rigidbody2D>();
                    if (!hitInfo.collider.GetComponent<Enemy>().antiOttalkivanie)
                    {
                        if (enemyRigidbody != null)
                        {
                            Vector2 knockbackDirection = (enemyRigidbody.position - GetComponent<Rigidbody2D>().position).normalized;
                            enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
                        }
                    }
                    else
                    {
                        int antiforce = hitInfo.collider.GetComponent<Enemy>().antiForce;
                        Vector2 knockbackDirection = (enemyRigidbody.position - GetComponent<Rigidbody2D>().position).normalized;
                        enemyRigidbody.AddForce(knockbackDirection * antiforce, ForceMode2D.Impulse);
                    }
                }
            }
            if (hitInfo.collider.CompareTag("Player") && enemyBullet)
            {
                if (PlayerKontroller.GiGa == false)
                {
                    hitInfo.collider.GetComponent<PlayerKontroller>().PlayerTakeDamage(damage);
                }
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.right * speed * Time.deltaTime);

       
    }

    public void DestroyBullet()
    {
        Instantiate(bulletEffect, transform.position, Quaternion.identity);
        if (flechka)
        {
            HandleEnemyHit();
        }
        Destroy(gameObject);
    }

    private void HandleEnemyHit()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsSolid);
        foreach (var col in enemiesInRange)
        {
            if (col.CompareTag("Enemy"))
            {
                col.GetComponent<Enemy>().MetodPohlebly();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
