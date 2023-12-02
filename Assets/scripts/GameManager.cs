using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int level = 1;

    public bool caught = false;
    public GameObject caughtScreen;

    

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);

        caught = false;
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (caught)
        {
            caughtScreen.SetActive(true);
        }
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        PauseMenu.GameIsPaused = true;
        AudioListener.pause = true;
    }
    public bool isPause()
    {
        return PauseMenu.GameIsPaused;
        
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PauseMenu.GameIsPaused = false;
        AudioListener.pause = false;
    }
}
