using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We inherit the unitscript class so we have access to the health functions and variable(s)
public class PlayerScript : UnitScript
{
    //The character controller attached to the player
    public CharacterController controller;
    //The speed of the player
    public float speed;

    //At start we set the health and find the controller
    new void Start()
    {
        health = MaxHP;

        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //We initiate movementVector as (0,0,0)
        Vector3 movementVector = Vector3.zero;

        //We set the X and Z values to our keyboard
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

        //Move the player
        controller.Move(movementVector * speed * Time.deltaTime);
    }
}
