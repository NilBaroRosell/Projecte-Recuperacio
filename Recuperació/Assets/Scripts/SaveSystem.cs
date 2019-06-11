using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void savePlayer(GameControl gameControl)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.gg";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(gameControl);

        formatter.Serialize(stream, data);
        Debug.Log("DATA SAVED");
        stream.Close();
    }

    public static PlayerData loadPlayer()
    {
        string path = Application.persistentDataPath + "/player.gg";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("NO FILE FOUND");
            return null;
        }
    }

    public static void deleteData()
    {
        string path = Application.persistentDataPath + "/player.gg";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("DATA DELETED");
        }
    }
}
