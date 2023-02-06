using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    #region Variables
    public enum enemyStates { WalkingL = 0, WalkingR = 1, Idle = 2, Combat = 3 };

    [Header("Animations")]
    //A reference to the animator that controls which animation to play
    public Animator animator;
    //The name of the parameter that controls which state to play
    public string stateParameterName = "State";

    [Header("States")]
    public enemyStates state;

    [Header("Walk")]
    public float walkingTime = 1f;
    private float walkedTime = 0f;

    [Range(0, 5)]
    //The speed at which the enemy will move
    public float speed;

    [Header("Idle")]
    //The state the enemy will go to after unfrozen
    private enemyStates cachedState;

    //The controller that will handle basic movement collisions for us
    private CharacterController controller;

    [Header("Combat")]
    //How far the enemy must be from the player to see them
    public float range = 1f;

    //The sparks that spawn when the sword hits down
    public ParticleSystem sparks;

    //Will tell us if we're in range of the target
    private bool inRange = false;
    
    //The target that this enemy is attacking
    private Transform target = null;
    
    #endregion 
    
    // Start is called before the first frame update
    void Start()
    {
        //Get the character controller component on this game object
        controller = GetComponent<CharacterController>();

        GameObject temp;
        temp = GameObject.FindGameObjectWithTag("Player");
        //if ((temp = GameObject.FindGameObjectWithTag("Player")) != null)
        if (temp != null)
        {
            target = temp.transform;
        }

        /*
        if (TryGetComponent<CharacterController>(out controller))
        {
            controller.radius = 2f;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        CheckRange();

        if (!inRange)
            Freeze();

        switch (state)
        {
            case enemyStates.WalkingL:
                WalkingL();
                break;
            case enemyStates.WalkingR:
                WalkingR();
                break;
            case enemyStates.Idle:
                Idle();
                break;
            case enemyStates.Combat:
                Combat();
                break;
        }

        UpdateAnimation();
    }

    void WalkingL()
    {
        //The next three are equivalent
        //if (!inRange)
        //if (inRange != true)
        //if (inRange == false)
        //The next two are equivalent
        //if (inRange == true)
        if (inRange)
        {
            state = enemyStates.Combat;
            walkedTime = 0;
            return;
        }

        walkedTime += Time.deltaTime;

        controller.Move(Vector3.left * speed * Time.deltaTime);

        if (walkedTime >= walkingTime)
        {
            state = enemyStates.WalkingR;
            walkedTime = 0;
        }
        cachedState = state;
    }

    void WalkingR()
    {
        if (inRange)
        {
            state = enemyStates.Combat;
            walkedTime = 0;
            return;
        }

        walkedTime += Time.deltaTime;

        controller.Move(Vector3.right * speed * Time.deltaTime);

        if (walkedTime >= walkingTime)
        {
            state = enemyStates.WalkingL;
            walkedTime = 0;
        }
        cachedState = state;
    }

    void Idle()
    {
        if (inRange)
        {
            state = enemyStates.Combat;
            walkedTime = 0;
            return;
        }


    }

    void Combat()
    {
        //If we're not in range, we go back to the last state that was put in the cache
        if (!inRange)
        {
            state = cachedState;
            return;
        }


    }

    void CheckRange()
    {
        //If there is no target, the enemy cannot be in range
        if (target == null)
        {
            inRange = false;
            return;
        }

        //We subtract the position of the enemy from that of the target, it will give us a vector with direction and magnitude.
        //The magnitude is the distance between both points.
        //If the magnitude is less than the range, this enemy is in range.
        //if ( (target.position - this.transform.position).magnitude <= range)
        //if (Vector3.Distance(target.position, this.transform.position) <= range)
        //{
        //    inRange = true;
        //}
        //else
        //    inRange = false;

        inRange = (Vector3.Distance(target.position, this.transform.position) <= range);
    }

    void Freeze()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 2f);

        //Ray is the direction of the ray, mostly
        //Hit will store the results of the raycast, including what it hit, where it hit it, the normal of the 
        //surface, etc
        //The last one is the max distance which in this case is infinity
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.transform == this.transform)
            {
                //We store the last state so we can restore it later, as long as said state isnt idle
                if (state != enemyStates.Idle)
                    cachedState = state;

                state = enemyStates.Idle;
                return;
            }
        }

        state = cachedState;
    }

    void UpdateAnimation()
    {
        animator.SetInteger(stateParameterName, (int)state);
    }

    void PlaySparks()
    {
        Debug.Log("Pschoo");
        if (sparks != null)
        {
            sparks.Play();
        }
    }
}
