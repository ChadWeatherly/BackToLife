using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int level = 1;
    public bool isPaused = false;
    public bool caught = false;
    private GameObject ui;
    public GameObject caughtScreen;

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        isPaused = false;
        caught = false;
        ui = GameObject.Find("UI");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (caught)
        {
            caughtScreen.SetActive(true);
        }
    }
}
