using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickHandler : MonoBehaviour
{
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the D key is pressed
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRight", true);
            animator.SetBool("isLeft", false);
            animator.SetBool("isDown", false);
            animator.SetBool("isUp", false);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("isLeft", true);
            animator.SetBool("isDown", false);
            animator.SetBool("isUp", false);
            animator.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isDown", true);
            animator.SetBool("isLeft", false);
            animator.SetBool("isUp", false);
            animator.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isUp", true);
            animator.SetBool("isDown", false);
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);
        }
        else 
        {
            animator.SetBool("isLeft", false);
            animator.SetBool("isDown", false);
            animator.SetBool("isUp", false);
            animator.SetBool("isRight", false);
            animator.SetBool("isIdle", true);
        }
    }
}
