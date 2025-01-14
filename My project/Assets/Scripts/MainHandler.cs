using UnityEngine;
using System.Linq;
using System.Collections;
using System;

public class MainHandler : MonoBehaviour
{
    private Animator animator;
    private GameObject currentArrow;

    private bool onTime;

    private bool missClick;
    private bool miss;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Find the arrow with the least Y position that's still on screen
        currentArrow = ObjectSpawner.spawnedArrows
            .Where(arrow => arrow != null && arrow.transform.position.y > -5f && arrow.transform.position.y < 3.5f)
            .OrderBy(arrow => arrow.transform.position.y)
            .FirstOrDefault();

        if (currentArrow != null)
        {
            onTime = currentArrow.transform.position.y > 0.5f && currentArrow.transform.position.y < 5f;

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Up") {
                    SetAnimationDirection("Up");
                    miss = false;
                    missClick = false; // Reset 'missclick' to false
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection == "Up") {
                    missClick = true;
                    StartCoroutine(ResetMissAfterDelay(.5f));                     
                    SetAnimationDirection("UpMiss");
                    miss = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Down") {
                    SetAnimationDirection("Down");
                    miss = false;
                    missClick = false; // Reset 'missclick' to false
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection == "Down") {
                    missClick = true;
                    StartCoroutine(ResetMissAfterDelay(.5f));  
                    SetAnimationDirection("DownMiss");
                    miss = false;
                }

            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Left") {
                    SetAnimationDirection("Left");
                    miss = false;
                    missClick = false; // Reset 'missclick' to false
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection == "Left") {
                    missClick = true;
                    StartCoroutine(ResetMissAfterDelay(.5f));  
                    SetAnimationDirection("LeftMiss");
                    miss = false;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Right") {
                    SetAnimationDirection("Right");
                    miss = false;
                    missClick = false; // Reset 'missclick' to false
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection == "Right") {
                    missClick = true;
                    StartCoroutine(ResetMissAfterDelay(.5f));  
                    SetAnimationDirection("RightMiss");
                    miss = false;
                }
            }
           
            if (!Input.anyKey && !missClick && !miss)
            {
                ResetAnimations();
            }             
            else if (onTime && !missClick && !Input.anyKey)
            {
                miss = true;
                StartCoroutine(ResetMissAfterDelay(.5f));  
                if (currentArrow.GetComponent<MovingObject>().arrowDirection == "Up") {
                    SetAnimationDirection("UpMiss");
                } else if (currentArrow.GetComponent<MovingObject>().arrowDirection == "Down") {
                    SetAnimationDirection("DownMiss");
                } else if (currentArrow.GetComponent<MovingObject>().arrowDirection == "Left") {
                    SetAnimationDirection("LeftMiss");
                } else if (currentArrow.GetComponent<MovingObject>().arrowDirection == "Right") {
                    SetAnimationDirection("RightMiss");
                }        
            }   
        }
    }
    private IEnumerator ResetMissAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
        miss = false; // Reset 'miss' to false
        missClick = false; // Reset 'missclick' to false
        ResetAnimations();
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
