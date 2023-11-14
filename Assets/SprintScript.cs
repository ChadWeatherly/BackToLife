using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintScript : MonoBehaviour
{
    private bool canDash = true;
    private bool isDash = false;
    public float dashSpeed = 10.0f;
    public float dashTime = 1.0f;
    public float dashCooldown = 2.0f;
    public Rigidbody2D rb;

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
        rb.velocity = new Vector2(transform.localScale.x * dashSpeed, transform.localScale.y * dashSpeed);
        yield return new WaitForSeconds(dashTime);
        isDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
