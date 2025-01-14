using UnityEngine;
using System.Linq;
using System.Collections;
using System;

public class MainHandler : MonoBehaviour
{
    private Animator animator;
    private GameObject currentArrow;

    private bool onTime;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Find the arrow with the least Y position that's still on screen
        currentArrow = ObjectSpawner.spawnedArrows
            .Where(arrow => arrow != null && arrow.transform.position.y > -5f && arrow.transform.position.y < 8f)
            .OrderBy(arrow => arrow.transform.position.y)
            .FirstOrDefault();

        if (currentArrow != null)
        {
            onTime = currentArrow.transform.position.y > 1.5f && currentArrow.transform.position.y < 3.3f;

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Up") {
                    SetAnimationDirection("Up");
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Up") {
                    SetAnimationDirection("UpMiss");
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Down") {
                    SetAnimationDirection("Down");
                } else if (onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Down") {
                    SetAnimationDirection("DownMiss");
                }

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Left") {
                    SetAnimationDirection("Left");
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Left") {
                    SetAnimationDirection("LeftMiss");
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Right") {
                    SetAnimationDirection("Right");
                } else if (onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Right") {
                    SetAnimationDirection("RightMiss");
                }
            }
            else if (!Input.anyKey)
            {
                ResetAnimations();
            }
        }
    }
    private IEnumerator ResetMissAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
    }
    private void ResetAnimations()
    {
        animator.SetBool("UpOn", false);
        animator.SetBool("DownOn", false);
        animator.SetBool("LeftOn", false);
        animator.SetBool("RightOn", false);
        animator.SetBool("UpOff", false);
        animator.SetBool("DownOff", false);
        animator.SetBool("LeftOff", false);
        animator.SetBool("RightOff", false);
    }

    public void SetAnimationDirection(string direction)
    {
        // Reset all animation keys
        ResetAnimations();

        // Set the corresponding direction key
        switch (direction)
        {
            case "Up":
                animator.SetBool("UpOn", true);
                break;
            case "Down":
                animator.SetBool("DownOn", true);
                break;
            case "Left":
                animator.SetBool("LeftOn", true);
                break;
            case "Right":
                animator.SetBool("RightOn", true);
                break;
            case "UpMiss":
                animator.SetBool("UpOff", true);
                break;
            case "DownMiss":
                animator.SetBool("DownOff", true);
                break;
            case "LeftMiss":
                animator.SetBool("LeftOff", true);
                break;
            case "RightMiss":
                animator.SetBool("RightOff", true);
                break;
        }
    }
}
