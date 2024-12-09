using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickHandler : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    public void SetAnimationDirection(string direction)
    {
        // Reset all animation keys
        animator.SetBool("isUp", false);
        animator.SetBool("isDown", false);
        animator.SetBool("isLeft", false);
        animator.SetBool("isRight", false);

        // Set the corresponding direction key
        switch (direction)
        {
            case "Up":
                animator.SetBool("isUp", true);
                break;
            case "Down":
                animator.SetBool("isDown", true);
                break;
            case "Left":
                animator.SetBool("isLeft", true);
                break;
            case "Right":
                animator.SetBool("isRight", true);
                break;
        }
    }
}
