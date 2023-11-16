using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioMixer SFXMixer;
    [SerializeField] private AudioMixer MusicMixer;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider MusicSlider;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
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
        optionMenuUI.SetActive(false);
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
        float sfx = SFXSlider.value;
        SFXMixer.SetFloat("SFX",Mathf.Log10(sfx)*20);
    }
    public void SetMusicVolume(){
        float music = MusicSlider.value;
        MusicMixer.SetFloat("Music",Mathf.Log10(music)*20);
    }
}
