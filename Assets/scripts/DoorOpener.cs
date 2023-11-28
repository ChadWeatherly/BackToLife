using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public GotKey key;
    //public GateButton button;

    private void Update()
    {
        //if (button.gateOpen)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Orpheus")
        {
            if (key.gotKey)
            {
                Destroy(gameObject);
            }
        }
    }
}
