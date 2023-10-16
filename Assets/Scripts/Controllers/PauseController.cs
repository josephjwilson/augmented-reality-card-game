using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public GameObject optionMenuUI;
    public GameObject optionButton;

    public void OptionButton()
    {
        optionButton.SetActive(false);
        optionMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeButton()
    {
        optionButton.SetActive(true);
        optionMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void SurrenderButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
