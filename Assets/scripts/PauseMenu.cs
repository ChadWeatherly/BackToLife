using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){
                
                Resume();
            } else
            {
                
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Game is resumed");
        AudioListener.pause = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("Game is paused");
        AudioListener.pause = true;
    }
    public void QuitGame(){
        SceneManager.LoadScene("Menuscreen");
    }
}
