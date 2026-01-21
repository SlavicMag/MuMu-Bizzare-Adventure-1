using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float[] position;
    public bool NeProyDech;
    public int score;
    public List<int> ammo;
    public List<int> currentAmmo;
    public List<int> cout;
    public List<bool> spawnerActive;
    public List<bool> ifnetEnemyActive;
    public List<bool> gameobjectActive;
    public float ukurennost;
    public float EmocBaltImage;
    public List<int> i;
    public List<string> unlockedWeapones;
    public int currentDvijJoystikIndex;
    public int currentShootJoystikIndex;
    public float dopHp;

    public PlayerData (PlayerKontroller player, ScoreManager scoreMan, List<Gun> ammos, List<Spawnir> spawnir, List<CatSceneTrigger> ifnetenemy, List<GameObject> gameObjectSave, PauseMenu pauseMenu)
    {
        health = (int)player.health;
        score = ScoreManager.score;
        ukurennost = player.ukurennost;
        EmocBaltImage = player.EmocBaltImage.fillAmount;
        NeProyDech = player.neProyDesh;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        ammo = new List<int>();
        currentAmmo = new List<int>();

        foreach (var gun in ammos)
        {
            ammo.Add(gun.allAmmo);
            currentAmmo.Add(gun.currentAmmo);
        }

        cout = new List<int>();
        spawnerActive = new List<bool>();

        foreach (var spawner in spawnir)
        {
            cout.Add(spawner.cout);
            spawnerActive.Add(spawner.gameObject.activeSelf);
        }

        i = new List<int>();
        ifnetEnemyActive = new List<bool>();

        foreach (var IfnetEnemy in ifnetenemy)
        {
            i.Add(IfnetEnemy.i);
            ifnetEnemyActive.Add(IfnetEnemy.gameObject.activeSelf);
        }

        gameobjectActive = new List<bool>();

        foreach (var gameObjectsave in gameObjectSave)
        {
            gameobjectActive.Add(gameObjectsave.gameObject.activeSelf);
        }

        unlockedWeapones = new List<string>();
        foreach (var weapon in player.unlockedWeapons)
        {
            unlockedWeapones.Add(weapon.name);
        }

        currentDvijJoystikIndex = pauseMenu.currentDvijJoystikIndex;
        currentShootJoystikIndex = pauseMenu.currentShootJoystikIndex;
        dopHp = (int)player.dopHealth;
    }
}
