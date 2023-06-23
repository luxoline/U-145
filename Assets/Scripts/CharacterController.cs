using System;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f, sprintSpeed = 10f, rotationSmoothness = 0.5f, jumpSpeed = 5f, jumpDelay = 0.5f;
    float speed;

    Rigidbody rb;
    Animator animator;

    float moveHorizontal, moveVertical, ctr;
    bool jump;

    private void Start()
    {
        ctr = 0;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Animations();


        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y <= 0.01 && ctr <= 0f)
        {
            ctr = jumpDelay;
            jump = true;
            animator.SetBool("isJumping", true);
        }
        else if (ctr > 0f)
        {
            ctr -= Time.deltaTime;
        }
    }

    private void Animations()
    {
        if (Mathf.Abs(moveHorizontal) > 0.1f || Mathf.Abs(moveVertical) > 0.1f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
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
