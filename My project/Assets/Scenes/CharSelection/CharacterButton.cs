using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CharacterButton : MonoBehaviour
{
    public int characterIndex; // You set this in the Inspector

    public void SelectCharacter()
    {
        CharacterSelection.selectedIndex = characterIndex;
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SampleScene");
    }

}