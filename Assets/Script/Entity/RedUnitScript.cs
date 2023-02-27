using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RedUnitScript : UnitScript
{
    //The node that we are currently following. Set it at edit time to determine the first node.
    public NodeScript nextNode;
    //A reference to the controller so we can call the "move" function
    public CharacterController controller;
    //The speed at which this unit will move towards nextNode
    public float speed;
    //The minimum distance the unit must be from nextNode to move to the next one
    public float minDistance;



    //The particles that will be created when this object is spawned
    public ParticleSystem spawnParticles;

    // Start is called before the first frame update
    new void Start()
    {
        health = MaxHP;

        Instantiate(spawnParticles, transform.position, Quaternion.identity);

        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //If there's no next node, this unit will not move
        if (nextNode == null)
            return;

        Vector3 movement = 
            (nextNode.transform.position - transform.position).normalized 
            * speed 
            * Time.deltaTime;

        //Otherwise, the unit will move in the direction towards its nextNode reference
        controller.Move(movement);

        //If the distance between this unit and nextNode is less than the minimum distance,
        //we get a new nextNode from the current one, by "asking" it what its own "next node" is.
        if (Vector3.Distance(nextNode.transform.position, transform.position) <= minDistance)
        {
            nextNode = nextNode.GetNext();
        }
    }
}
