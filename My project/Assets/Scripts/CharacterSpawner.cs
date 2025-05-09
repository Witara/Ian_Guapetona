using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public Transform characterHolder; // The holder of character models
    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip[] songs; // Array to hold different songs for each index

    void Start()
    {
        int selectedIndex = CharacterSelection.selectedIndex;

        // Loop through the character models and activate the selected one
        for (int i = 0; i < characterHolder.childCount; i++)
        {
            characterHolder.GetChild(i).gameObject.SetActive(i == selectedIndex);
            // characterHolder.GetChild(i).gameObject.tag = "OnStage"; // Set the tag for the selected character
        }

        // Check if the selected index is valid and play the corresponding song
        if (selectedIndex >= 0 && selectedIndex < songs.Length)
        {
            audioSource.clip = songs[selectedIndex]; // Set the selected song
            audioSource.Play(); // Play the song
        }
        else
        {
            Debug.LogWarning("Invalid selected index or song not assigned.");
        }
    }
}
