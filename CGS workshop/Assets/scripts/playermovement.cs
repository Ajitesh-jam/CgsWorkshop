using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck; // Position to check if grounded
    public float groundDistance = 0.4f;
    public LayerMask groundMask; // Layer mask for the ground

    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isMoving;

    private Vector3 _lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Ground Check
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log("IS grounded : " + _isGrounded);
        // Resetting the default velocity
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 a = transform.right * x + transform.forward * z;
        Vector3 move = a * speed;
        controller.Move(move * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //actually jump
        }

        _velocity.y += gravity * Time.deltaTime; // falling down
        controller.Move(_velocity * Time.deltaTime); // the jump

        if (_lastPosition != gameObject.transform.position && _isGrounded == true)
        {
            _isMoving = true; //later use
        }
        else
        {
            _isMoving = false;
        }

        _lastPosition = gameObject.transform.position;
    }
}