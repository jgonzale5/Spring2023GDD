using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveDemo : MonoBehaviour
{
    //What method will this script use to save information
    public enum saveMode { PlayerPrefs, SerializedBinaries};
    public saveMode SaveMode;

    //A reference to each input field
    public TMP_InputField A;
    public TMP_InputField B;
    public TMP_InputField C;
    //A reference to the slider
    public Slider D;

    private void Awake()
    {
        Load();
    }

    public void Save()
    {
        //Debug messages to confirm that it's working
        Debug.Log("Saving...");
        Debug.Log(string.Format("A: {0} \n B: {1} \n C: {2} \n D: {3}", A.text, B.text, C.text, D.value));

        if (SaveMode == saveMode.PlayerPrefs)
        {
            //Set the value of A, B, and C to the ones assigned under the input field
            PlayerPrefs.SetString("A", A.text);
            PlayerPrefs.SetString("B", B.text);
            PlayerPrefs.SetString("C", C.text);
            //Set the value of D to be the one in the slider
            PlayerPrefs.SetFloat("D", D.value);
            //Save the values
            PlayerPrefs.Save();
        }
        else if (SaveMode == saveMode.SerializedBinaries)
        {
            //We update the values in the selected save
            SaveLoad.selectedSave.words = new string[3];
            SaveLoad.selectedSave.words[0] = A.text;
            SaveLoad.selectedSave.words[1] = B.text;
            SaveLoad.selectedSave.words[2] = C.text;
            SaveLoad.selectedSave.sliderVal = D.value;

            //We save the selected save into memory
            SaveLoad.Save();
        }
    }

    public void Load()
    {
        if (SaveMode == saveMode.PlayerPrefs)
        {
            //Get the value of A, B, and C and update the fields on screen accordingly
            A.text = PlayerPrefs.GetString("A");
            B.text = PlayerPrefs.GetString("B");
            C.text = PlayerPrefs.GetString("C");

            //Get the value of D and update the slider on screen accordingly
            D.value = PlayerPrefs.GetFloat("D");
        }
        else if (SaveMode == saveMode.SerializedBinaries)
        {
            SaveLoad.Load();

            if (SaveLoad.selectedSave.words.Length >= 3)
            {
                A.text = SaveLoad.selectedSave.words[0];
                B.text = SaveLoad.selectedSave.words[1];
                C.text = SaveLoad.selectedSave.words[2];
            }

            D.value = SaveLoad.selectedSave.sliderVal;
        }
    }
}
