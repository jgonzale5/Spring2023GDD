using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class BlueUnitScript : UnitScript
{
    //The object this unit will pursue
    public GameObject targetObject;
    //The nav mesh agent that will allow this unit to move and pathfind
    private NavMeshAgent agent;

    //The particles spawned when this object is hit
    public ParticleSystem hitParticle;
    //The enemies that come out of this one when it dies
    public Transform splitEnemies;

    //We overwrite the original damage function so it spawns particles now
    public override void Damage(int dmg)
    {
        Debug.Log("Blue damage");
        health -= dmg;

        Instantiate(hitParticle, transform.position, Quaternion.identity);
    }

    //This overrides the Die function from the unity scrript
    protected override void Die()
    {
        //Instantiate two split enemies in the same position
        Instantiate(splitEnemies, transform.position, Quaternion.identity);
        Instantiate(splitEnemies, transform.position, Quaternion.identity);

        //Destroy the original
        Destroy(this.gameObject);
    }

    private new void Start()
    {
        //Set health
        health = MaxHP;
        //Get nav mesh agent component
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //If there is no agent, stop
        if (agent == null)
            return;

        //Set the destination to the target object
        agent.SetDestination(targetObject.transform.position);

        
    }
}
