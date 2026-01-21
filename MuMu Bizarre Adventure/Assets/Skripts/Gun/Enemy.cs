using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Sounds
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public ControlEnemy controlenemy;
    public enum ControlEnemy { Boss, PrEnemy, Strelok };
    public int health;
    public float Minspeed;
    public float speed;
    public float Normspeed;
    public float Stopspeed;
    public bool Stop = false;
    public GameObject deathEffect;
    public GameObject BloodEffect;
    public int damage;
    private PlayerKontroller player;
    public Animator anim;
    public string[] attackAnim;
    private bool isPlayerInRange;
    private ScoreManager sm;
    private Rigidbody2D bossRigidbody;
    public Transform attackPos;
    public float attackRange;
    public GameObject aptekaEffect;
    [Header("���������")]
    private Transform playerTr;
    public float jumpForce;
    public float jumpHeightThreshold;
    private Rigidbody2D rb;
    public bool jump;
    [Header("����������")]
    public Transform zonePos;
    public float zoneRange;
    public LayerMask izbegLayer;
    private Vector3 initialPosition;
    private Transform target;
    public GameObject tochka;
    private bool taktic = false;
    public GameObject patrony;
    public float dropChance;
    public bool dropItem;
    public int scoreE;
    public bool antiOttalkivanie;
    public int antiForce;
    public float timePohleblu;
    public Gun gun;
    public bool strelaet;
    public string[] poluchenieAnim;
    public bool nestrelat;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerKontroller>();
        sm = FindObjectOfType<ScoreManager>();
        bossRigidbody = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (jump)
        {
            if(playerTr.position.y > transform.position.y + jumpHeightThreshold)
            {
                Jump();
            }
        }
        if (playerTr.position.y < transform.position.y + 3f)
        {
            Physics2D.IgnoreLayerCollision(7, 10, true);
            Invoke("IgnoreLayerOff", 0.5f);
        }
        if (Stop == true)
        {
            speed = Stopspeed;
        }
        if (PlayerKontroller.tanec)
        {
            if (controlenemy != ControlEnemy.Boss)
            {
                anim.SetBool("tanes", true);
                speed = Minspeed;
            }
        }
        else if (!PlayerKontroller.tanec)
        {
            anim.SetBool("tanes", false);
            if (Stop == false)
            {
                speed = Normspeed;
            }
        }
        if (health <= 0)
        {
            DeathEnemy();
            sm.Kill(scoreE);
        }
        if(player.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (taktic)
        {
            if (controlenemy == ControlEnemy.Strelok)
            {
                transform.position = Vector2.MoveTowards(transform.position, tochka.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (strelaet)
        {
            if (nestrelat)
            {
                gun.AnLoadTimeBTW();
            }
        }
    }
    
    public void TakeDamage(int damage)
    {
        PlaySound(0, random: true, p1: 1f, p2: 1f);
        health -= damage;
        Instantiate(BloodEffect, transform.position, Quaternion.identity);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (controlenemy == ControlEnemy.Strelok)
        {
            if (other.CompareTag("Player"))
            {
                taktic = true;
            }
        }

        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (timeBtwAttack <= 0)
            {
                if (controlenemy != ControlEnemy.Boss)
                {
                    anim.SetTrigger("enemyAttackk");
                }
                else
                {
                    int randomIndex = Random.Range(0, attackAnim.Length);
                    string randomAttack = attackAnim[randomIndex];
                    anim.SetTrigger(randomAttack);
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }

        if (other.CompareTag("Player"))
        {
            taktic = false;
        }
    }
    public void OnEnemyAttack()
    {
        if(isPlayerInRange)
        {
            if (PlayerKontroller.GiGa == false)
            {
                CamController.cameraShake?.Invoke(5f, 0.2f, 0.2f);
                player.PlayerTakeDamage(damage);
                PlaySound(0, p1: 1f, p2: 1f);
            }
        }
        timeBtwAttack = startTimeBtwAttack;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (controlenemy == ControlEnemy.Boss)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange);
        }

        if (controlenemy == ControlEnemy.Boss)
        {
            if (PlayerKontroller.GiGa == false)
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    bossRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (controlenemy == ControlEnemy.Boss)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                bossRigidbody.constraints = RigidbodyConstraints2D.None;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);

        if (controlenemy == ControlEnemy.Strelok)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(zonePos.position, zoneRange);
        }
    }

    public IEnumerator Pohlebaly()
    {
        if (anim.GetBool("Pohlebaly")) yield break;
        Stop = true;
        anim.SetBool("Pohlebaly", true);
        yield return new WaitForSeconds(timePohleblu);
        anim.SetBool("Pohlebaly", false);
        Stop = false;
    }

    public void DeathEnemy()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        if (dropItem)
        {
            if (Random.value < dropChance)
            {
                Instantiate(patrony, transform.position, Quaternion.identity);
            }
        }
    }

    public void PlusHealth(int apteka)
    {
        health += apteka;
        Instantiate(aptekaEffect, transform.position, Quaternion.identity);
    }

    public void PlayZvuk(int sound)
    {
        PlaySound(sound, p1: 1f, p2: 1f);
    }

    public void Jump()
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
             rb.linearVelocity = Vector2.up * jumpForce;
        }
    }

    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(7, 10, false);
    }

    public void MetodPohlebly()
    {
        StartCoroutine(Pohlebaly());
    }

    public void AnLoadTimeShoot()
    {
        if (strelaet)
        {
            gun.AnLoadTimeBTW();
        }
    }

    public void AnimPoluch()
    {
        int randomIndex = Random.Range(0, poluchenieAnim.Length);
        string randomPoluchil = poluchenieAnim[randomIndex];
        anim.SetTrigger(randomPoluchil);
    }

    public IEnumerator timeSlawZaj(float time)
    {
        anim.SetTrigger("��1");
        nestrelat = true;
        Stop = true;
        yield return new WaitForSeconds(time);
        Stop = true;
        nestrelat = true;
        DeathEnemy();
    }

    public void MetodTimeSlawZaj(float timeZ)
    {
        StartCoroutine(timeSlawZaj(timeZ));
    }
}
