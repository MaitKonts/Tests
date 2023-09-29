using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    float yVelocity = 0f;
    [Range(5f, 25f)]
    public float gravity = 15f;
    [Range(5f, 15f)]
    public float walkSpeed = 7f;
    [Range(5f, 15f)]
    public float runSpeed = 12f;
    [Range(5f, 15f)]
    public float jumpSpeed = 10f;

    private Vector3 move;

    Transform cameraTransform;
    float pitch = 0f;
    [Range(1f, 90f)]
    public float maxPitch = 85f;
    [Range(-1f, -90f)]
    public float minPitch = -85f;
    [Range(0.5f, 5f)]
    public float mouseSensitivity = 2f;

    CharacterController cc;
    private Animator anim;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        float xInput = Input.GetAxis("Mouse X") * mouseSensitivity;
        float yInput = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(0, xInput, 0);
        pitch -= yInput;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        Quaternion rot = Quaternion.Euler(pitch, 0, 0);
        cameraTransform.localRotation = rot;
    }

    void Move()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Vector3.ClampMagnitude(input, 1f);
        move = transform.TransformVector(input) * speed;

        if (cc.isGrounded)
        {
            yVelocity = -gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = jumpSpeed;
                anim.SetTrigger("Jump");
            }
        }

        yVelocity -= gravity * Time.deltaTime;
        move.y = yVelocity;

        if (input != Vector3.zero)
        {
            anim.SetFloat("Speed", speed);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }

        cc.Move(move * Time.deltaTime);
    }
}
