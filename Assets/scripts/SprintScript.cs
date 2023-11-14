using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintScript : MonoBehaviour
{
    private bool canDash = true;
    private bool isDash = false;
    public float dashTime = 1.0f;
    public float dashCooldown = 2.0f;
    public orpheus_script Orp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDash = true;
        float tempSpeed = Orp.moveSpeed;
        Orp.moveSpeed = 10f;
        yield return new WaitForSeconds(dashTime);
        Orp.moveSpeed = tempSpeed;
        isDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
