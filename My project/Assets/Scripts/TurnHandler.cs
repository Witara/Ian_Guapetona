using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    private List<GameObject> turnOrder = new List<GameObject>();
    private int currentTurnIndex = 0;
    private string playerTag = "Player";
    private string onStageTag = "OnStage";
    private string singingTag = "Singing";
    public float turnDelay = 10; // Time between turns
    private Coroutine turnCoroutine = null;

    void Start()
    {
        InitializeTurnOrder();

        if (turnCoroutine == null)
        {
            turnCoroutine = StartCoroutine(AutoNextTurn());
        }
    }

    void InitializeTurnOrder()
    {
        turnOrder.Clear();
        
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            turnOrder.Add(player);
        }
        
        GameObject[] onStageObjects = GameObject.FindGameObjectsWithTag(onStageTag);
        foreach (GameObject obj in onStageObjects)
        {
            turnOrder.Add(obj);
        }
        
        if (turnOrder.Count > 0)
        {
            SetCurrentTurn(0);
        }
    }

    IEnumerator AutoNextTurn()
    {
        while (true)
        {
            yield return new WaitForSeconds(turnDelay);
            NextTurn();
        }
    }

    void NextTurn()
    {
        if (turnOrder.Count == 0) return;
        
        ResetTag(currentTurnIndex);
        
        currentTurnIndex = (currentTurnIndex + 1) % turnOrder.Count;
        
        SetCurrentTurn(currentTurnIndex);
    }

    void SetCurrentTurn(int index)
    {
        if (index >= 0 && index < turnOrder.Count)
        {
            turnOrder[index].tag = singingTag;
        }
    }

    void ResetTag(int index)
    {
        if (index >= 0 && index < turnOrder.Count)
        {
            if (turnOrder[index].CompareTag(singingTag))
            {
                turnOrder[index].tag = (index == 0) ? playerTag : onStageTag;
            }
        }
    }
}