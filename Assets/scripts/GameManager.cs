using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int level = 1;
    public bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        //print(isPaused);
    }
}
