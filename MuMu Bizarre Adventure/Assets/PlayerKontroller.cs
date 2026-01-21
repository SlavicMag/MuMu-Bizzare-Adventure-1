using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerKontroller : Sounds
{
    [SerializeField]
    [Header("Controls")]
    public float dlinaTrasky;
    public Joystick joystick;

    public float Sspeed;
    public float Pspeed;
    public float speed;
    public float UskSpeed;
    public float Tspeed;
    public float jumpForce;
    public float timeJump;
    public float startTimeJump;
    public float moveInput;
    public Image BaltikaImage;
    public bool Uskorenie = false;
    public GameObject sled;
    public float cooldownBalt;
    public float perBalt;
    public Image EmocBaltImage;
    public bool ReloadBalt = false;
    [Header("Healths")]
    public float health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public float dopHealth;
    public int dopNumOfHearts;
    public Image[] dopHearts;
    public Sprite dopfullHeart;

    public float point;
    public int numOfPoint;
    public Image[] pointImage;
    public Sprite fullPoint;
    public Sprite emptyPoint;
    public Sprite closePoint;
    [Header("Effects and Panels")]
    public GameObject panel;
    public GameObject potionEffect;
    public GameObject BaltikaEffect;
    public GameObject TrebobonEffect;
    public GameObject emptyulta;
    public GameObject[] ulta;
    public GameObject stend;
    
    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    [Header("Kusha")]
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Animator anim;
    public static bool tanec;
    public Image tanecImage;
    public Sprite tanecTrue;
    public Sprite tanecFalse;
    [Header("Weapons")]
    public List<GameObject> unlockedWeapons;
    public GameObject[] allWeapons;
    public Image weaponIcon;
    public Gun[] guns;
    private ScoreManager scoreM;
    public List<Gun> amms;  
    public List<Spawnir> spawnir;
    public List<CatSceneTrigger> ifnetenemy;
    public List<GameObject> gameobjectsave;
    public float ZajimZona;
    public Transform SlawyanPos;
    public Gun[] gaan;
    public bool isSwitchingWeapon;
    private ScoreManager sm;
    public Animator uianim;

    public Text gameOverText;
    public string[] gameOverMessages;
    public GameObject BloodEffect;

    [Header("Stando")]
    public static bool seroga = true;
    public Image stendImage;
    public float cooldownS;
    public float cooldownG;
    public GameObject stendEffect;
    public static bool GiGa = false;
    public GameObject Gigachad;
    public GameObject telo;
    public Image GigaImage;
    private Radio radio;
    public static float volumeDo;
    private PauseMenu pauseMenu;
    public GameObject Gobject;

    public float ukurennost;
    public bool ukuren;
    public float cooldownUkur;
    public Image sugarSlider;
    public Image redSlider;
    public GameObject colorPanel;
    public float scrollSpeed;
    public float panelOpacity;
    private float timeSinceLastColorChange = 0;
    private float timeMuzik = 0;
    public static bool timline;
    public bool neProyDesh;
    private bool noJump;
    public GameObject kulak;

    public GameObject[] taymers;

    public Joystick jostikAttack;

    public List<GameObject> spawnedObjects;

    public Animator zatemnenie;

    public List<GameObject> objectPrefabs;
    public Vector3 spawnPosition;
    public bool catscene;
    public bool trenbolon = true;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        radio = FindObjectOfType<Radio>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sm = FindObjectOfType<ScoreManager>();

        joystick.gameObject.SetActive(true);
    }


    private void FixedUpdate()
    {
        if (point > numOfPoint)
        {
            point = numOfPoint;
        }
        if (health <= 0)
        {
            panel.SetActive(true);
            radio.Pause();
            pauseMenu.Pause();
            Time.timeScale = 0f;
            int randomIndex = Random.Range(0, gameOverMessages.Length);
            gameOverText.text = gameOverMessages[randomIndex];
        }
        if(health > 0)
        {
            panel.SetActive(false);
            if (!neProyDesh)
            {
                Time.timeScale = 1f;
            }
        }
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        for (int i = 0; i < pointImage.Length; i++)
        {
            if (!trenbolon && i == 1)
            {
                pointImage[i].sprite = closePoint;
            }
            else
            {
                if (i < Mathf.RoundToInt(point))
                {
                    pointImage[i].sprite = fullPoint;
                }
                else
                {
                    pointImage[i].sprite = emptyPoint;
                }
            }

            if (i < numOfPoint)
            {
                pointImage[i].enabled = true;
            }
            else
            {
                pointImage[i].enabled = false;
            }
        }

        if (dopHealth > dopNumOfHearts)
        {
            dopHealth = dopNumOfHearts;
        }
        for (int i = 0; i < dopHearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(dopHealth))
            {
                dopHearts[i].gameObject.SetActive(true);
            }
            else
            {
                dopHearts[i].gameObject.SetActive(false);
            }
            if (i < dopNumOfHearts)
            {
                dopHearts[i].enabled = true;
            }
            else
            {
                dopHearts[i].enabled = false;
            }
        }
        moveInput = joystick.Horizontal;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if(facingRight == true && moveInput < 0)
        {
            Flip();
        }
        if(moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    private void Update()
    {
        if (lockLunge)
        {
            if(Uskorenie)
            {
                lungeImage.fillAmount += Time.deltaTime * 2;
            }
            else
            {
                lungeImage.fillAmount += Time.deltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Lunge();
        }
        if (seroga)
        {
            stendImage.fillAmount -= 1 / cooldownS * Time.deltaTime;
            if (stendImage.fillAmount <= 0)
            {
                stendImage.fillAmount = 1;
                seroga = false;
                stend.SetActive(false);
                stendImage.gameObject.SetActive(false);
                Instantiate(stendEffect, transform.position, Quaternion.identity);
                StopMusic();
                taymers[0].SetActive(false);
            }
        }
        if (Uskorenie)
        {
            lungeImpulse = 14000;
            SpeeedSounds(2);
            BaltikaImage.fillAmount -= 0.7f / cooldownBalt * Time.deltaTime;
            if (BaltikaImage.fillAmount <= 0)
            {
                BaltikaImage.fillAmount = 1;
                Uskorenie = false;
                sled.SetActive(false);
                speed = Sspeed;
                anim.speed = 1;
                SpeeedSounds(1);
                BaltikaImage.gameObject.SetActive(false);
                Instantiate(BaltikaEffect, transform.position, Quaternion.identity);
                lungeImpulse = 8000;
                taymers[2].SetActive(false);
            }
        }
        if (GiGa)
        {
            GigaImage.fillAmount -= 1 / cooldownG * Time.deltaTime;
            if (GigaImage.fillAmount <= 0)
            {
                speed = Sspeed;
                GigaImage.fillAmount = 1;
                GiGa = false;
                Gigachad.SetActive(false);
                telo.SetActive(true);
                GigaImage.gameObject.SetActive(false);
                anim.SetBool("GiGa", false);
                Instantiate(TrebobonEffect, transform.position, Quaternion.identity);
                StopMusic();
                taymers[1].SetActive(false);
            }
        }

        switch (point)
        {
            case 0:
                emptyulta.SetActive(true);
                ulta[0].SetActive(false);
                ulta[1].SetActive(false);
                ulta[2].SetActive(false);
                break;

            case 1:
                emptyulta.SetActive(false);
                ulta[0].SetActive(true);
                ulta[1].SetActive(false);
                ulta[2].SetActive(false);
                break;

            case 2:
                emptyulta.SetActive(false);
                ulta[0].SetActive(false);
                ulta[1].SetActive(true);
                ulta[2].SetActive(false);
                break;
            case 3:
                emptyulta.SetActive(false);
                ulta[0].SetActive(false);
                ulta[1].SetActive(false);
                ulta[2].SetActive(true);
                break;
        }

        if (ScoreManager.score >= 500)
        {
            point = 1;
            if (ScoreManager.score >= 1500)
            {
                if (trenbolon)
                {
                    point = 3;
                }
            }
        }
        else if (ScoreManager.score < 500)
        {
            point = 0;
        }
        
        float verticalMove = joystick.Vertical;
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (isGrounded == true && verticalMove >= .4f)
        {
            YesLunge = true;
            rb.linearVelocity = Vector2.up * jumpForce;
            if (!noJump)
            {
                anim.SetTrigger("takeOf");
            }
            if (timeJump <= 0)
            {
                if(catscene == false)
                {
                    PlayMusik(15);
                }
                timeJump = startTimeJump;
            }
        }

        if (verticalMove <= -.5f)
        {
            Physics2D.IgnoreLayerCollision(9, 10, true);
        }
        else
        {
            IgnoreLayerOff();
        }

        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
            LungeUp = true;
        }
        else
        {
            if (!noJump)
            {
                anim.SetBool("isJumping", true);
            }
            else
            {
                anim.SetBool("isJumping", false);
            }
        }

        if (jostikAttack.Horizontal != 0 || jostikAttack.Vertical != 0)
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }

        if (ReloadBalt)
        {
            EmocBaltImage.fillAmount += 1 / perBalt * Time.deltaTime;
            if (EmocBaltImage.fillAmount >= 1)
            {
                ReloadBalt = false;
                speed = Sspeed;
            }
        }

        sugarSlider.fillAmount = ukurennost * 1 / 24;
        redSlider.fillAmount = ukurennost * 1 / 18;
        if (ukurennost > 0)
        {
            sugarSlider.gameObject.SetActive(true);
            redSlider.gameObject.SetActive(true);
            ukurennost -= Time.deltaTime * 1.2f;
        }
        else
        {
            sugarSlider.gameObject.SetActive(false);
            redSlider.gameObject.SetActive(false);
        }

        if (ukurennost >= 24)
        {
            ukuren = true;
        }

        if (ukuren)
        {
            if (timeMuzik <= 0)
            {
                PlayMusik(7);
                timeMuzik = 7.78f;
            }
            else
            {
                if(Uskorenie)
                {
                    timeMuzik -= Time.deltaTime * 2;
                }
                else
                {
                    timeMuzik -= Time.deltaTime;
                }
            }
            colorPanel.SetActive(true);
            timeSinceLastColorChange += Time.deltaTime;
            if (timeSinceLastColorChange >= scrollSpeed)
            {
                ScrollColors();
                timeSinceLastColorChange = 0f;
            }
            rb.freezeRotation = false;
            speed = 10;
            ukurennost -= Time.deltaTime;
            anim.Play("GlassesRun");
            VolumeRadio(0.5f);
            if (ukurennost <= 0)
            {
                StopMusic();
                timeMuzik = 0;
                colorPanel.SetActive(false);
                rb.freezeRotation = true;
                rb.rotation = 0;
                speed = Sspeed;
                ukuren = false;
                VolumeRadio(1);
            }
        }

        if (timeJump > 0)
        {
            timeJump -= Time.deltaTime;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Potion"))
        {
            health += 1;
            sm.Kill(75);
            gaan[1].Plus(gaan[1].poluchAmmo);
            gaan[2].Plus(gaan[2].poluchAmmo);
            Instantiate(potionEffect, other.transform.position, Quaternion.identity);
            PlayMusik(20);
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Weapon"))
        {
            for (int i = 0; i < allWeapons.Length; i++)
            {
                if(other.name == allWeapons[i].name)
                {
                    if (!unlockedWeapons.Contains(allWeapons[i]))
                    {
                        unlockedWeapons.Add(allWeapons[i]);
                    }
                }
            }
            PlayMusik(20);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("������������������"))
        {
            gaan[0].Plus(gaan[0].poluchAmmo);
            PlayMusik(20);
            Destroy(other.gameObject);
        }

        if(other.GetComponent<Pickup>())
        {
            PlayMusik(20);
        }
    }


    public int lungeImpulse = 100000;
    public int lungeUPImpulse;
    public bool YesLunge;
    public Image lungeImage;
    public bool LungeUp = false;

    public void Lunge()
    {
        if (!lockLunge)
        {
            lockLunge = true;
            if(Uskorenie)
            {
                Invoke("LungeLock", 0.5f);
            }
            else
            {
                Invoke("LungeLock", 1);
            }

            anim.StopPlayback();
            anim.Play("lunge");

            rb.linearVelocity = new Vector2(0, 0);

            if (!facingRight) 
            { 
                rb.AddForce(Vector2.left * lungeImpulse);
                rb.AddForce(Vector2.up * jumpForce);
            }
            else 
            {
                rb.AddForce(Vector2.right * lungeImpulse);
                rb.AddForce(Vector2.up * jumpForce);
            }
            float verticalMove = joystick.Vertical;
            if (verticalMove <= -.5f)
            {
                rb.AddForce(Vector2.down * lungeImpulse / 8);
                rb.AddForce(Vector2.down * jumpForce);
            }
            else if(verticalMove >= .4f && LungeUp)
            {
                rb.AddForce(Vector2.up * lungeImpulse / 10);
                LungeUp = false;
            }

                lungeImage.fillAmount = 0;
            isSwitchingWeapon = true;
        }
    }
    private bool lockLunge = false;
    internal int score;

    public void LungeLock()
    {
        lockLunge = false;
    }

    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this, scoreM, amms, spawnir, ifnetenemy, gameobjectsave, pauseMenu);
    }

    public void LoadPlayer ()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        neProyDesh = data.NeProyDech;

        if (health <= 0)
        {
            Time.timeScale = 1f;
            radio.Unpause();
            pauseMenu.Resume();
        }

        health = data.health;
        ScoreManager.score = data.score;
        ukurennost = data.ukurennost;
        BaltikaImage.fillAmount = 0;
        stendImage.fillAmount = 0;
        GigaImage.fillAmount = 0;
        EmocBaltImage.fillAmount = data.EmocBaltImage;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        for (int i = 0; i < amms.Count; i++)
        {
            if (i < data.ammo.Count)
            {
                amms[i].allAmmo = data.ammo[i];
                amms[i].currentAmmo = data.currentAmmo[i];
            }
        }

        for (int i = 0; i < spawnir.Count; i++)
        {
            if (i < data.cout.Count)
            {
                spawnir[i].cout = data.cout[i];
                spawnir[i].gameObject.SetActive(data.spawnerActive[i]);
            }
        }

        for (int i = 0; i < gameobjectsave.Count; i++)
        {
            gameobjectsave[i].gameObject.SetActive(data.gameobjectActive[i]);
        }

        for (int i = 0; i < ifnetenemy.Count; i++)
        {
            ifnetenemy[i].i = data.i[i];
            ifnetenemy[i].gameObject.SetActive(data.ifnetEnemyActive[i]);
        }

        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            GameObject.Destroy(enemy.gameObject);
        }

        DropItems[] droipItems = GameObject.FindObjectsOfType<DropItems>();
        foreach (DropItems dropItem in droipItems)
        {
            GameObject.Destroy(dropItem.gameObject);
        }

        SpriteBlood[] spriteBloods = GameObject.FindObjectsOfType<SpriteBlood>();
        foreach (SpriteBlood spriteBlood in spriteBloods)
        {
            GameObject.Destroy(spriteBlood.gameObject);
        }

        ParticleSystem[] particles = GameObject.FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem particle in particles)
        {
            GameObject.Destroy(particle.gameObject);
        }

        Gun[] gun = GameObject.FindObjectsOfType<Gun>();
        foreach (Gun gune in gun)
        {
            gune.AnLoadTimeBTW();
        }

        Bullet[] bullet = GameObject.FindObjectsOfType<Bullet>();
        foreach (Bullet bulle in bullet)
        {
            GameObject.Destroy(bulle.gameObject);
        }

        while (!kulak.activeSelf)
        {
            SwitchWeapon();
        }

        unlockedWeapons.Clear();
        foreach (var weapon in data.unlockedWeapones)
        {
            for (int i = 0; i < allWeapons.Length; i++)
            {
                if (weapon == allWeapons[i].name)
                {
                    unlockedWeapons.Add(allWeapons[i]);
                }
            }
        }

        StartCoroutine(Zatemnil(1.01f));
        dopHealth = data.dopHp;

        foreach (var prefab in objectPrefabs)
        {
            string objectName = prefab.name;
            GameObject existingObject = GameObject.Find(objectName);
            if (!data.unlockedWeapones.Contains(objectName))
            {
                if (existingObject == null)
                {
                    GameObject newObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
                    newObject.name = objectName;
                }
            }
            else
            {
                if (existingObject != null)
                {
                    Destroy(existingObject);
                }
            }
        }
    }

    public void SwitchWeapon()
    {
        for (int i = 0; i < unlockedWeapons.Count; i++)
        {
            if(unlockedWeapons[i].activeInHierarchy)
            {
                unlockedWeapons[i].SetActive(false);
                if(i != 0)
                {
                    unlockedWeapons[i - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[i - 1].GetComponent<IconWeapon>().icon;
                }
                else
                {
                    unlockedWeapons[unlockedWeapons.Count - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[unlockedWeapons.Count - 1].GetComponent<IconWeapon>().icon;
                }
                weaponIcon.SetNativeSize();
                isSwitchingWeapon = true;
                break;
            }
        }
    }

    void IgnoreLayerOff()
    {
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    public void ReloadGun()
    {
        foreach (Gun g in guns)
        {
            if (g != null && g.gameObject.activeSelf && !g.isReloading)
            {
                if(g.allAmmo > 0 && !tanec)
                {
                    StartCoroutine(NoJump(g.reloadTime));
                    StartCoroutine(g.Reload());
                }
            }
        }
    }

    public void SlavanskyZajim()
    {
        if(!GiGa)
        {
            StartCoroutine(NoJump(1.1f));
            uianim.Play("������� ������");
            ScoreManager.score -= 500;
            anim.SetTrigger("��1");
            PlaySound(0, p1: 1f, p2: 1f);
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, ZajimZona);

            for (int i = 0; i < enemies.Length; i++)
            {
                Enemy enemy = enemies[i].GetComponent<Enemy>();
                if (enemy != null)
                {
                    if(enemy.controlenemy != Enemy.ControlEnemy.Boss)
                    {
                        if (Uskorenie)
                        {
                            enemy.anim.speed = 2;
                        }
                        enemy.MetodTimeSlawZaj(1);
                    }
                }
            }
            isSwitchingWeapon = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(SlawyanPos.position, ZajimZona);
    }

    public void Stand()
    {
        if (!GiGa)
        {
            seroga = true;
            uianim.Play("����� ����������");
            Instantiate(stendEffect, transform.position, Quaternion.identity);
            ScoreManager.score -= 1000;
            stend.SetActive(true);
            stendImage.gameObject.SetActive(true);
            stendImage.fillAmount = 1;
            PlayMusik(18);
            PlayMusik(19);
            taymers[0].SetActive(true);
            isSwitchingWeapon = true;
            dopHealth = 9;
            health = 9;
        }
    }

    public void WERY()
    {
        if (!seroga)
        {
            GiGa = true;
            uianim.Play("���� ������");
            ScoreManager.score -= 1500;
            speed = 10;
            Gigachad.SetActive(true);
            telo.SetActive(false);
            GigaImage.gameObject.SetActive(true);
            GigaImage.fillAmount = 1;
            anim.SetBool("GiGa", true);
            PlaySound(25, volume: 3.5f, p1: 1f, p2: 1f);
            taymers[1].SetActive(true);
            isSwitchingWeapon = true;
        }
    }

    public void Trasenie()
    {
        CamController.cameraShake?.Invoke(7f, 0.8f, 0.8f);
    }

    public void Trasenie1()
    {
        CamController.cameraShake?.Invoke(1f, 0.5f, 0.5f);
    }

    public void TrasenieRegulir(float power)
    {
        CamController.cameraShake?.Invoke(power, dlinaTrasky, dlinaTrasky);
    }

    public void Tanec()
    {
        if (!tanec)
        {
            anim.SetBool("tanec", true);
            speed = Pspeed;
            tanec = true;
            tanecImage.sprite = tanecFalse;
            isSwitchingWeapon = true;

        }
        else
        {
            anim.SetBool("tanec", false);
            speed = Sspeed;
            tanec = false;
            tanecImage.sprite = tanecTrue;
        }
    }

    public void Kurenie()
    {
        if (!ukuren)
        {
            anim.SetTrigger("Kurenie");
            StartCoroutine(NoJump(1.2f));
        }
    }

    public void HealthPlus()
    {
        health += 2;
        ukurennost += 6;
        Instantiate(potionEffect, transform.position, Quaternion.identity);
    }

    public void BaltikaUskorenie()
    {
        if (ReloadBalt == false)
        {
            anim.SetTrigger("Baltika");
            StartCoroutine(NoJump(3.2f));
        }
    }

    public void SpeedPlus()
    {
        Uskorenie = true;
        ReloadBalt = true;
        speed = UskSpeed;
        anim.speed = 2;
        sled.SetActive(true);
        BaltikaImage.gameObject.SetActive(true);
        BaltikaImage.fillAmount = 1;
        EmocBaltImage.fillAmount = 0;
        Instantiate(BaltikaEffect, transform.position, Quaternion.identity);
        taymers[2].SetActive(true);
        dopHealth += 6;
    }

    public void PlayMusik(int musik)
    {
        PlaySound(musik, p1: 1f, p2: 1f);
    }

    public void Paise()
    {
        radio.DoVolume();
        radio.SwithVolume(0.1f);
    }

    public void VolumeRadio(float volume)
    {
        radio.SwithVolume(volume);
    }

    public void UnPaise()
    {
        radio.SwithVolume(volumeDo);
    }

    public void changeCameraSize(float size)
    {
        CamController.changeCameraSizeEvent?.Invoke(size);
    }

    public void PlaySongNumber(int number)
    {
        radio.PlayNumberSong(number);
    }

    public void GobjectTrue(int active)
    {
        if (active == 1)
        {
            Gobject.SetActive(true);
        }
        else if (active == 0)
        {
            Gobject.SetActive(false);
        }
    }

    private void ScrollColors()
    {
        Graphic[] graphics = colorPanel.GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in graphics)
        {
            graphic.color = new Color(Random.value, Random.value, Random.value, panelOpacity);
        }
    }

    public void isSwithWeaponN()
    {
        isSwitchingWeapon = false;
    }

    public void PlayerTakeDamage(int damages)
    {
        if(dopHealth > 0)
        {
            dopHealth -= damages;
            Instantiate(BloodEffect, transform.position, Quaternion.identity);
            PlaySound(0, random: true, p1: 1f, p2: 1f);
            if (dopHealth < 0)
            {
                float excessDamage = Mathf.Abs(dopHealth);
                dopHealth = 0;
                health -= excessDamage;
            }
        }
        else
        {
            health -= damages;
            Instantiate(BloodEffect, transform.position, Quaternion.identity);
            PlaySound(0, random: true, p1: 1f, p2: 1f);
        }
    }

    public void StartRadio()
    {
        radio.pause = false;
    }

    public IEnumerator NoJump(float time)
    {
        noJump = true;
        yield return new WaitForSeconds(time);
        noJump = false;
    }

    public IEnumerator Zatemnil(float time)
    {
        zatemnenie.gameObject.SetActive(true);
        zatemnenie.SetTrigger("fade");
        yield return new WaitForSeconds(time);
        zatemnenie.gameObject.SetActive(false);
    }
}

