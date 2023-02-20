using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class UnitControlScript : MonoBehaviour
{
    //How much damage will be done to a unit when its right-clicked
    public int rightClickDamage = 50;

    // Update is called once per frame
    void Update()
    {
        //The ray projected from the position of the mouse on the screen, onto the world
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //If the player is right-clicking
        //AND
        //the ray drawn, with a maximum length of inifnity, hits something,
        //we proceed with the information of the hit
        if (Input.GetMouseButtonDown(1) && 
            Physics.Raycast(ray, out RaycastHit info, Mathf.Infinity))
        {
            if (info.transform.TryGetComponent<UnitScript>(out UnitScript target))
            {
                //We tell the component to receive a rightClickDamage amount of damage
                target.Damage(rightClickDamage);
            }
        }
    }
}
