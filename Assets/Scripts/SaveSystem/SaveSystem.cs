using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    static string path = Application.persistentDataPath + "VisuelltLarande.savefile";
  
    public static SaveData LoadSave()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        SaveData data;
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
        }
        else
        {
            Debug.Log("Creating new savefile");
            FileStream stream = new FileStream(path, FileMode.Create);
            data = new SaveData();
            formatter.Serialize(stream, data);
            stream.Close();

        } 
        return data;
    }

    public static void Save(SaveData data)
    {   
        foreach(string a in data.enabledObjects){
            Debug.Log(a);
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
        foreach (string a in SaveSystem.LoadSave().enabledObjects)
        {
            Debug.Log(a);
        }
    }


    public static void AddCredits(int amount)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        SaveData data = (SaveData)formatter.Deserialize(stream);
        stream.Close();
        data.credits += amount;
        stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void AddBuyItem(string ID)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        SaveData data = (SaveData)formatter.Deserialize(stream);
        stream.Close();
        data.enabledObjects.Add(ID);
        stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();
    }

}
