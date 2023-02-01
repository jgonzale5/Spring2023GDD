using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    #region Variables
    public enum enemyStates { WalkingL = 0, WalkingR = 1, Idle = 2 };

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
    #endregion 

    // Start is called before the first frame update
    void Start()
    {
        //Get the character controller component on this game object
        controller = GetComponent<CharacterController>();

        /*
        if (TryGetComponent<CharacterController>(out controller))
        {
            controller.radius = 2f;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
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
        }

        UpdateAnimation();
    }

    void WalkingL()
    {
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
}
