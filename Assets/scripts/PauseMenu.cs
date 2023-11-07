using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioMixer mainVolume;
    [SerializeField] private Slider VolumeSlider;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private void Start(){
        SetVolume();
    }
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
    public void SetVolume(){
        float volume = VolumeSlider.value;
        mainVolume.SetFloat("SFX",Mathf.Log10(volume)*20);
    }
}
