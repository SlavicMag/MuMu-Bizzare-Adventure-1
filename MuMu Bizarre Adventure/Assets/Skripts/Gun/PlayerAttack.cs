using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Sounds
{
    public float timeBtwAttack;
    public float startTimeBtwAttack;
    public float GigaTimeBtwAttack;
    public float[] StartsTimeBtwAttack;

    public Transform attackPos;
    public LayerMask enemy;
    public float attackRange;
    public int damage;
    public int Gigadamage;
    public int Stenddamage;
    public Animator anim;
    public Joystick joystick;
    public PlayerKontroller player;
    public Animator EfAnim;

    public float[] traskaGiga;
    public float[] traskaPlayer;
    public float[] traskaStend;

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if(joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                if (PlayerKontroller.seroga == true && !PlayerKontroller.tanec)
                {
                    anim.SetTrigger("attackStend");
                    EfAnim.SetTrigger("attackEf");
                }
                else if (PlayerKontroller.seroga == false && !PlayerKontroller.tanec)
                {
                    if(!PlayerKontroller.GiGa)
                    {
                        if (player.unlockedWeapons[0].activeSelf)
                        {
                            if (joystick.Vertical > 0.7)
                            {
                                anim.SetTrigger("attackUp");
                            }
                            else
                            {
                                anim.SetTrigger("attack");
                            }
                        }
                    }
                    else
                    {
                        anim.SetTrigger("attack");
                    }
                }
            }
            if(PlayerKontroller.GiGa == true)
            {
                timeBtwAttack = GigaTimeBtwAttack;
                if(joystick.Horizontal == 0 & joystick.Vertical == 0)
                {
                    timeBtwAttack = startTimeBtwAttack;
                }
            }
            else
            {
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if(player.Uskorenie)
        {
            startTimeBtwAttack = StartsTimeBtwAttack[0];
            GigaTimeBtwAttack = StartsTimeBtwAttack[1];
        }
        else
        {
            startTimeBtwAttack = StartsTimeBtwAttack[2];
            GigaTimeBtwAttack = StartsTimeBtwAttack[3];
        }
    }
    public void OnAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (PlayerKontroller.GiGa == true && PlayerKontroller.seroga == false)
            {
                enemies[i].GetComponent<Enemy>().TakeDamage(Gigadamage);
                CamController.cameraShake?.Invoke(traskaGiga[0], traskaGiga[1], traskaGiga[2]);
                PlaySound(1, p1: 1f, p2: 1f);
                enemies[i].GetComponent<Enemy>().AnimPoluch();
            }
            else if (PlayerKontroller.GiGa == false && PlayerKontroller.seroga == false)
            {
                enemies[i].GetComponent<Enemy>().TakeDamage(damage);
                CamController.cameraShake?.Invoke(traskaPlayer[0], traskaPlayer[1], traskaPlayer[2]);
                PlaySound(0, random: true, p1: 1f, p2: 1f);
                if(enemies[i].GetComponent<Enemy>().controlenemy != Enemy.ControlEnemy.Boss)
                {
                    enemies[i].GetComponent<Enemy>().AnimPoluch();
                    enemies[i].GetComponent<Enemy>().AnLoadTimeShoot();
                }
            }
            else if (PlayerKontroller.seroga == true && PlayerKontroller.GiGa == false)
            {
                enemies[i].GetComponent<Enemy>().TakeDamage(Stenddamage);
                CamController.cameraShake?.Invoke(traskaStend[0], traskaStend[1], traskaStend[2]);
                PlaySound(0, random: true, p1: 1f, p2: 1f);
                enemies[i].GetComponent<Enemy>().AnimPoluch();
                enemies[i].GetComponent<Enemy>().AnLoadTimeShoot();
            }
        }
        if (PlayerKontroller.seroga == true && PlayerKontroller.GiGa == false)
        {
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            foreach (GameObject bullet in bullets)
            {
                if (Vector2.Distance(bullet.transform.position, attackPos.position) <= attackRange)
                {
                    bullet.GetComponent<Bullet>().DestroyBullet();
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
