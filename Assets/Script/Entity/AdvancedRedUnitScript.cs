using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedRedUnitScript : RedUnitScript
{
    public enum State { Patrol, Pause, Chase };
    public State state;

    [Header("Pause State")]
    //How long this unit should stop for when pausing
    public float pauseLength;
    //The amount of time that this unit has currently paused for
    private float pauseTimer = 0;

    [Header("Player")]
    //How close the player must be before the enemy can see them
    public float visionRange = 5;
    //A reference to the player
    private UnitScript player;
    //The layers this enemy cannot see through
    public LayerMask visionLayers;

    new void Start()
    {
        //We set the health to the maxHP
        health = MaxHP;

        //We instantiate the particles
        Instantiate(spawnParticles, transform.position, Quaternion.identity);

        //The controller is found on this object
        controller = GetComponent<CharacterController>();

        //The player is found on the scene by looking for the player tag, then getting the unit script in it
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<UnitScript>();
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.DrawLine(transform.position, player.transform.position, Color.red, 1);

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Pause:
                Pause();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }

    void Patrol()
    {
        //The moment the player can be seen, we start chasing them
        if (CanSeePlayer())
        {
            state = State.Chase;
            return;
        }

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

            //When you get to the node, also change to a pause state
            state = State.Pause;
        }
    }

    void Pause()
    {
        //The moment this enemy sees the player, it will reset the timer and go after them
        if (CanSeePlayer())
        {
            state = State.Chase;
            pauseTimer = 0;
            return;
        }

        //If more time has passed than there this unit is supposed to wait for
        if (pauseTimer >= pauseLength)
        {
            //Reset the timer
            pauseTimer = 0;
            //Set the state to patrol
            state = State.Patrol;
            return;
        }

        //Add the delta time to the timer to keep track of time
        pauseTimer += Time.deltaTime;
    }

    void Chase()
    {
        CanSeePlayer();
    }

    bool CanSeePlayer()
    {
        bool inRange = Vector3.Distance(transform.position, player.transform.position) <= visionRange;

        if (inRange)
        {

            if (Physics.Raycast(
                transform.position,
                player.transform.position - transform.position,
                out RaycastHit info,
                visionRange,
                visionLayers))
            {

                if (info.transform.tag == ("Player"))
                {
                    Debug.DrawLine(transform.position, player.transform.position, Color.green);
                    return true;
                }
                else
                {
                    Debug.DrawLine(transform.position, player.transform.position, Color.red);
                    return false;
                }
            }
            else
            {
                Debug.DrawLine(transform.position, player.transform.position, Color.red);
                return false;
            }
        }
        else
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.blue);
            return false;
        }
    }
}
