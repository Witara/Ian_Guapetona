using UnityEngine;
using System.Linq;
using System.Collections;
using TMPro;
using System;
using Unity.VisualScripting;

public class MainHandler : MonoBehaviour
{
    private Animator animator;
    private GameObject currentArrow;
    public GameObject enemy;
    public GameObject player;
    public TextMeshProUGUI pointsText; // For TMP
    private bool pointUp = false;
    private bool pointDown = false;
    private int pointsPlayer = 0;
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
                    pointUp = true;
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Up") {
                    SetAnimationDirection("UpMiss");
                    // StartCoroutine(DelayThenTagEnemy());
                    pointDown = true;
                }
            } 
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Down") {
                    SetAnimationDirection("Down");
                    StartCoroutine(CheckArrowPosition());
                    pointUp = true;
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Down") {
                    SetAnimationDirection("DownMiss");
                    // StartCoroutine(DelayThenTagEnemy());
                    pointDown = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Left") {
                    SetAnimationDirection("Left");
                    StartCoroutine(CheckArrowPosition());
                    pointUp = true;
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Left") {
                    SetAnimationDirection("LeftMiss");
                    // StartCoroutine(DelayThenTagEnemy());
                    pointDown = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (onTime && currentArrow.GetComponent<MovingObject>().arrowDirection == "Right") {
                    SetAnimationDirection("Right");
                    StartCoroutine(CheckArrowPosition());
                    pointUp = true;
                } else if (!onTime || currentArrow.GetComponent<MovingObject>().arrowDirection != "Right") {
                    SetAnimationDirection("RightMiss");
                    // StartCoroutine(DelayThenTagEnemy());
                    pointDown = true;
                }
            }
            if (!Input.anyKey)
            {
                ResetAnimations();
            }

        }
        if (pointUp)
        {
            pointUp = false;
            pointsPlayer++;
            Debug.Log("Points Player: " + pointsPlayer);
        } else if (pointDown)
        {
            pointDown = false;
            pointsPlayer--;
            Debug.Log("Points Player: " + pointsPlayer);
        }
        pointsText.text = "Points: " + pointsPlayer;
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

    
    IEnumerator DelayThenTagEnemy()
    {
        yield return new WaitForSeconds(2f);
        enemy.tag = "Singing";
        player.tag = "OnStage";

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
