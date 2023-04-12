using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDemoScript : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("punch");
        }

        animator.SetFloat("X", Input.GetAxis("Horizontal"));

        animator.SetFloat("Y", Input.GetAxis("Vertical"));
    }
}
