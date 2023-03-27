using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveLoad
{
    [System.Serializable]
    public class SaveFile
    {
        //The words on A, B, and C
        public string[] words = new string[0];
        //The value of the slider
        public float sliderVal;
    }

    public static SaveFile selectedSave = new SaveFile();
    public static string saveFileName = "player.sav";

    public static void Save()
    {
        //The path of the savefile is generated
        string path = Application.persistentDataPath + "/" + saveFileName;
        Debug.Log(path);

        FileStream dataStream;

        //If the file exists, we open it. Otherwise, we create it.
        if (File.Exists(path))
        {
            dataStream = new FileStream(path, FileMode.Open);
        }
        else
        {
            dataStream = new FileStream(path, FileMode.Create);
        }

        //We make a formatter able to serialize our variable
        BinaryFormatter formatter = new BinaryFormatter();

        //We serialize the selected save variable, using the data stream we set up
        formatter.Serialize(dataStream, selectedSave);

        dataStream.Close();
    }

    public static void Load()
    {
        //The path of the savefile is generated
        string path = Application.persistentDataPath + "/" + saveFileName;
        Debug.Log(path);

        FileStream dataStream;

        //If the file exists, we open it. Otherwise, we create it.
        if (File.Exists(path))
        {
            dataStream = new FileStream(path, FileMode.Open);
        }
        else
        {
            Debug.Log("Save file does not exist. New file has been created.");
            dataStream = new FileStream(path, FileMode.Create);

            //We would want to do something here so the file has content to deserialize, or skip the deserialization when the file was just created
        }

        //We make a formatter able to serialize our variable
        BinaryFormatter formatter = new BinaryFormatter();

        //We serialize the selected save variable, using the data stream we set up
        selectedSave = (SaveFile)formatter.Deserialize(dataStream);

        dataStream.Close();
    }
}
