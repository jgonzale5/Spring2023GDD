using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class WeaponClass
{
    //The name of this weapon
    public string name;
    //The prefab that will be spawned when this weapon is selected
    public Transform weaponPrefab;
    //The game object with the position where this weapon will spawn
    public GameObject weaponPivot;
    //The key that will select this weapon
    public KeyCode weaponKey;

    //A reference to the iteration of this weapon currently spawned
    private GameObject spawnedWeapon;

    public bool KeyPress(KeyCode input)
    {
        //return input == weaponKey;

        bool result = (input == weaponKey);

        return result;
    }

    public void Select()
    {
        Debug.Log("Selected " + name);

        spawnedWeapon = MonoBehaviour.Instantiate(weaponPrefab, 
            weaponPivot.transform.position, 
            Quaternion.identity, 
            weaponPivot.transform).gameObject;
    }

    public void Deselect()
    {
        Debug.Log("Deselected " + name);

        if (spawnedWeapon != null)
            MonoBehaviour.Destroy(spawnedWeapon);
    }
}
