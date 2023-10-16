using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneLoader : MonoBehaviour
{
    string usernameInput;
    public GameObject inputField;

    //Audio Mixer
    public AudioMixer audioMixer;

    public void PlayGame()
    {
        if (PlayerData.username != null)
            SceneManager.LoadScene("Game");
        else
            Debug.Log("Please set a username");
    }

    public void SetName()
    {
        usernameInput = inputField.GetComponent<Text>().text;
        PlayerData.username = usernameInput;

        Debug.Log(usernameInput);
    }


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetDifficulty(int difficultyIndex)
    {
        PlayerData.difficultyIndex = difficultyIndex;
    }
}