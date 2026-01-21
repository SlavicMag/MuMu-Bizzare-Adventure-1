using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : Sounds
{
    public GunType gunType;
    public int maxAmmo = 120;
    public int currentAmmo = 30;
    public int allAmmo = 0;
    public int A;
    public int poluchAmmo;

    public float offset;
    public GameObject bullet;
    public Transform shotPoint;
    public float reloadTime = 0f;
    public Animator anim;
    public string nameReload;
    public enum GunType{ Player, Enemy};

    private float timeBtwShots;
    public float startTimeBtwShots;
    public  bool isReloading = false;
    public Joystick joystick;
    private float rotZ;
    private Vector3 difference;
    private PlayerKontroller player;
    public float shootingDistance;
    public Transform shootPos;
    public bool blic = false;
    public GameObject objectBlic;

    public float powerShake;
    public float dolgotaShake;
    public float timeShake;

    [SerializeField]
    private Text ammoCount;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerKontroller>();
    }

    void Update()
    {
        if (gunType == GunType.Player)
        {
            if (Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
            {
                rotZ = Mathf.Atan2(joystick.Vertical, joystick.Horizontal) * Mathf.Rad2Deg;
            }

        }
        else if (gunType == GunType.Enemy)
        {
            difference = player.transform.position - transform.position;
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        
        
        if (timeBtwShots <= 0 && !isReloading)
        {
            if (currentAmmo > 0 && gunType == GunType.Enemy)
            {
                if (!PlayerKontroller.tanec)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

                    if (distanceToPlayer <= shootingDistance)
                    {
                        if(anim.GetBool("Pohlebaly") == false) 
                        {
                            Shoot();
                        }
                    }
                    if (blic)
                    {
                        if (distanceToPlayer <= shootingDistance)
                        {
                            objectBlic.SetActive(true);
                        }
                        else 
                        {
                            objectBlic.SetActive(false);
                        }
                    }
                }
            }
            else if(currentAmmo > 0)
            {
                if(gunType == GunType.Player && joystick.Horizontal != 0 || joystick.Vertical != 0)
                {
                    if (!PlayerKontroller.tanec)
                    {
                        if (!PlayerKontroller.seroga)
                        {
                            if(!PlayerKontroller.timline)
                            {
                                Shoot();
                            }
                        }
                    }
                }
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        if (gunType == GunType.Player)
        {
            ammoCount.text = currentAmmo + " / " + allAmmo;
        }

        if(gunType == GunType.Player)
        {
            if (player.isSwitchingWeapon)
            {
                if (isReloading)
                {
                    StopCoroutine(Reload());
                    isReloading = false;
                }
            }
        }
    }
    

    public IEnumerator Reload()
    {
        if (isReloading) yield break;
        isReloading = true;
        anim.SetTrigger(nameReload);
        Debug.Log("Reloading");

        if(player.Uskorenie)
        {
            yield return new WaitForSeconds(reloadTime / 2);
        }
        else
        {
            yield return new WaitForSeconds(reloadTime);
        }

        if (!player.isSwitchingWeapon)
        {
            int reason = A - currentAmmo;
            if (allAmmo >= reason)
            {
                allAmmo = allAmmo - reason;

                currentAmmo = A;
            }
            else
            {
                currentAmmo = currentAmmo + allAmmo;
                allAmmo = 0;
            }
        }
        player.Gobject.SetActive(false);
        isReloading = false;
    }

    public void Shoot()
    {
        PlaySound(0, p1: 1f, p2: 1f);
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        CamController.cameraShake?.Invoke(powerShake, dolgotaShake, timeShake);
        anim.SetTrigger("Shoot");
        anim.SetTrigger("Pay");
        timeBtwShots = startTimeBtwShots;
        currentAmmo--;
        if (gunType == GunType.Player)
        {
            player.isSwitchingWeapon = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (gunType == GunType.Enemy)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(shootPos.position, shootingDistance);
        }
    }

    public void Plus(int patron)
    {
        allAmmo += patron;
    }

    public void AnLoadTimeBTW()
    {
        timeBtwShots = startTimeBtwShots;
    }
}
