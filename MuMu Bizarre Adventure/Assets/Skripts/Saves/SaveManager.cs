using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SavePlayerData(Vector3 position)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlData data = new PlData(position);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public PlData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.dat";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlData data = formatter.Deserialize(stream) as PlData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}

[System.Serializable]
public class PlData
{
    public float x;
    public float y;
    public float z;

    public PlData(Vector3 position)
    {
        x = position.x;
        y = position.y;
        z = position.z;
    }
}


