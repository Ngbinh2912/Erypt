using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;

    private Rigidbody2D rb;

    public SpriteRenderer characterSR;

    Animator animator;

    public Vector3 moveInput; //luu tru input di chuyen

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //di chuyen
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        transform.position += moveInput * speed * Time.deltaTime;

        //set speed cho animator
        animator.SetFloat("Speed", moveInput.sqrMagnitude);

        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            Dash();
        }

        //quay dau
        if (moveInput.x != 0)
        {
            //CmnLoiNhieuVai
            characterSR.flipX = moveInput.x < 0;
            //if (moveInput.x > 0)
            //{
            //  characterSR.transform.localScale = new Vector3(1, 1, 0);
            //}
            //else if (moveInput.x < 0)
            //{
            //  characterSR.transform.localScale = new Vector3(-1, 1, 0);
            //}
        }
    }
    private void Dash()
    {
        isDashing = true;

        animator.SetTrigger("Dash");

        rb.linearVelocity = moveInput * dashSpeed;

        Invoke(nameof(EndDash), dashDuration);
    }
    private void EndDash()
    {
        isDashing = false;
        rb.linearVelocity = Vector2.zero;
    }
}
