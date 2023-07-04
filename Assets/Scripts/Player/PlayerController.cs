﻿using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f, sprintSpeed = 10f, rotationSmoothness = 0.5f, jumpSpeed = 5f, jumpDelay = 0.5f;
    float speed;

    Rigidbody rb;
    Animator animator;

    float moveHorizontal, moveVertical, ctr;
    bool jump;
    public bool canWalk, isWalking;

    private void Awake()
    {
        DialogueManager.Instance.playerController = this;
        ctr = 0;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }


    //karakter değişimi yapılırken önce aktif olan karakter disable edilip sonra diğer karakter enable edilmeli
    private void OnEnable()
    {
        canWalk = true;
        DialogueManager.Instance.playerController = this;
        //AudioManager.Instance.playerController = this;
    }
    private void OnDisable()
    {
        canWalk = false;
        DialogueManager.Instance.playerController = null;
        //AudioManager.Instance.playerController = null;
    }

    private void Update()
    {
        if (canWalk){
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }else{
            moveHorizontal = 0;
            moveVertical = 0;
        }
        
        AnimationsAndSounds();
        
        if (Input.GetKey(KeyCode.LeftShift)) speed = sprintSpeed;
        else speed = moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y <= 0.01 && ctr <= 0f){
            ctr = jumpDelay;
            jump = true;
            animator.SetBool("isJumping", true);
        }else if (ctr > 0f)
        {
            ctr -= Time.deltaTime;
        }
    }

    private void AnimationsAndSounds()
    {
        if (canWalk == false)
        {
            Debug.Log("yurumemesi lazim");
            animator.SetBool("isRunning",false);
            animator.SetBool("isJumping", false);
        }
        
        if (Mathf.Abs(moveHorizontal) > 0.1f || Mathf.Abs(moveVertical) > 0.1f)
        {
            animator.SetBool("isRunning", true);
            isWalking = true;
            //if(!AudioManager.Instance.isWalkingSoundPlaying) AudioManager.Instance.PlayWalkSound();
        }
        else
        {
            animator.SetBool("isRunning", false);
            isWalking = false;
            //AudioManager.Instance.StopWalkSound();
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (!canWalk) return;
        Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;

        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);



        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSmoothness);
        }

        if (jump)
        {
            jump = false;
            rb.AddForce(Vector3.up * jumpSpeed * Time.deltaTime, ForceMode.Impulse);
            animator.SetBool("isJumping", false);
        }
    }
}