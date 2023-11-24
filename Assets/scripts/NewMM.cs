using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text errorText;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadGame()
    {
        if(PlayerPrefs.HasKey("lastScene"))
        {
            string lastScene = PlayerPrefs.GetString("lastScene");

            SceneManager.LoadScene(lastScene);
        }
        else
        {
            errorText.text = "No saved scene found!";
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
