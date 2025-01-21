using UnityEngine;
using System.Linq;
using System.Collections;
using System;
using Unity.VisualScripting;

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
        currentArrow = ObjectSpawner.spawnedArrows
            .Where(arrow => arrow != null && arrow.transform.position.y > -5f && arrow.transform.position.y < 5.5f)
            .OrderBy(arrow => arrow.transform.position.y)
            .FirstOrDefault();

        if (currentArrow != null)
        {
            onTime = currentArrow.transform.position.y > -1.5f && currentArrow.transform.position.y < 5.5f;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Up") {
                    SetAnimationDirection("Up");
                    StartCoroutine(CheckArrowPosition());
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Up") {
                    SetAnimationDirection("UpMiss");
                }
            } 
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Down") {
                    SetAnimationDirection("Down");
                    StartCoroutine(CheckArrowPosition());
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Down") {
                    SetAnimationDirection("DownMiss");
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Left") {
                    SetAnimationDirection("Left");
                    StartCoroutine(CheckArrowPosition());
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Left") {
                    SetAnimationDirection("LeftMiss");
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Right") {
                    SetAnimationDirection("Right");
                    StartCoroutine(CheckArrowPosition());
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Right") {
                    SetAnimationDirection("RightMiss");
                }
            }
            if (!Input.anyKey)
            {
                ResetAnimations();
            }

        }
    }
    private IEnumerator ResetMissAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time
    }

    IEnumerator CheckArrowPosition()
    {
        while (currentArrow != null)
        {
            if (currentArrow.transform.position.y >= 2.25f)
            {
                Destroy(currentArrow);
                Debug.Log("Arrow destroyed after reaching 2f.");
                yield break;  // Exit the coroutine
            }
            yield return null;  // Wait until the next frame
        }
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
        ResetAnimations();
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
