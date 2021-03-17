using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int credits;
    public List<string> enabledObjects;

    public SaveData()
    {
        credits = 0;
        enabledObjects = new List<string>();
    }

    public override string ToString()
    {
        return "Credits: " + credits;
    }
}
