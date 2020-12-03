using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.CompilerServices;

[System.Serializable]
public class GameData
{
    //public type data;

    public GameData(GameManager manager)
    {
        //data = manager.data;
    }
}


public static class SaveLoad
{
    public static void Save(GameManager manager)
    {
        string path = Application.persistentDataPath + "/gameSave.dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(path, FileMode.Create);
       
        GameData data = new GameData(manager);

        bf.Serialize(file, data);
        file.Close();
    }

    public static GameData Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gameSave.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameSave.dat",FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void Delete()
    {
        File.Delete(Application.persistentDataPath + "/gameSave.dat");
    }
}