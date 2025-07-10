using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    public Animator animator;
    public Transform playerModel;


    public AudioSource footstepSource;
    public AudioClip footstepClip;
    public float footstepInterval = 0.5f;

    float footstepTimer = 0f;
 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        MyInput();
        rb.drag = groundDrag;

        // Calculate animation speed from movement input
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        float speed = flatVelocity.magnitude;

        animator.SetFloat("speed", speed);
        RotateCharacter();
        PlayFootstepSound(speed);

    }

    void PlayFootstepSound(float speed)
    {
        bool isMoving = speed > 0.1f;

        footstepTimer += Time.deltaTime;

        if (isMoving && footstepTimer >= footstepInterval)
        {
            if (footstepClip != null && footstepSource != null)
            {
                footstepSource.PlayOneShot(footstepClip);
                footstepTimer = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);


    }
    private void RotateCharacter()
    {
        // Don't rotate if not moving
        if (moveDirection.magnitude > 0.1f)
        {
            // Get direction on flat ground (ignore Y)
            Vector3 direction = new Vector3(moveDirection.x, 0f, moveDirection.z).normalized;

            // Calculate the rotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smooth rotation
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}
