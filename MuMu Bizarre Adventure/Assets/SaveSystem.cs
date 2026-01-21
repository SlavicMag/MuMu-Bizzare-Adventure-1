using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class SaveSystem 
{
     
    public static void SavePlayer (PlayerKontroller player, ScoreManager scoreM, List<Gun> ammmos, List<Spawnir> spawnir, List<CatSceneTrigger> ifnetenemy, List<GameObject> gameObjectSave, PauseMenu pauseMenu)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, scoreM, ammmos, spawnir, ifnetenemy, gameObjectSave, pauseMenu);


        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
