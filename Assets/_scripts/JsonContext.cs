using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonContext : DataContext
{
    private void Awake()
    {
        fileName = Application.persistentDataPath + "/gamedata.json";
    }
    public override void Save()
    {
        var jsonData = JsonUtility.ToJson(_data);
        var fileStream = new StreamWriter(fileName);
        fileStream.Write(jsonData);
        fileStream.Close();
    }

    public override void Load()
    {
        if (!File.Exists(fileName)) return;
        var fileStream = new StreamReader(fileName);
        var jsonData = fileStream.ReadToEnd();
        fileStream.Close();
        JsonUtility.FromJsonOverwrite(jsonData, _data);
    }
}
