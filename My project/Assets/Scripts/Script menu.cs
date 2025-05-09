using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Scriptmenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jugar()
    {
        EventSystem.current.SetSelectedGameObject(null); // Deselect any focused UI
        SceneManager.LoadScene("CharacterSelection"); // Load the character selection scene
    }

    // Update is called once per frame
    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
}
