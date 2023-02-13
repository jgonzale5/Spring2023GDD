using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionScript : MonoBehaviour
{
    //public WeaponClass defaultWeapon;

    //An array of weapon class objects so we can have multiple weapons to select
    public WeaponClass[] weapons = new WeaponClass[0];

    //Keeps track of what weapon from the weapons array is currently selected
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Select the first weapon, that will be our "default" weapon
        if (weapons.Length > 0)
            weapons[0].Select();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSelection();
    }

    //This function determines what weapon should be selected based on the player input
    void WeaponSelection()
    {
        //For each weapon object in the weapon array
        //foreach (var temp in weapons)
        //We change to a for loop so it's a little easier to keep track of our selected weapon
        for (int i = 0; i < weapons.Length; i++)
        {
            //A temporary weaponclass variable used inside the loop. It would be better practice to define it
            //before the loop starts, and assign the value in the loop.
            WeaponClass temp = weapons[i];

            //If the player is currently pressing the weapon assigned to this weapon
            if (i != selectedWeapon && Input.GetKeyDown(temp.weaponKey))
            {
                //Select this weapon
                temp.Select();
                //Deselect the currently selected weapon
                weapons[selectedWeapon].Deselect();

                //The selected weapon is updated to the one assigned to temp
                selectedWeapon = i;
                return;
            }
        }
    }
}
