using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoulDoors : MonoBehaviour
{
    public Animator soulDoorLeft;
    public Animator soulDoorRight;

    // Start is called before the first frame update
    void Start()
    {
        soulDoorLeft = GetComponent<Animator>();
        soulDoorRight = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        soulDoorLeft.SetBool("Open", true);
        soulDoorRight.SetBool("Open", true);

        Invoke("CloseDoor", 8);
    }

    public void CloseDoor()
    {
        soulDoorLeft.SetBool("Open", false);
        soulDoorRight.SetBool("Open", false);
    }
}
