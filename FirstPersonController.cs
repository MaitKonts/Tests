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
    public float walkSpeed = 1f;
    [Range(2f, 10f)]
    public float runSpeed = 2f;
    [Range(1f, 10f)]
    public float jumpSpeed = 10f;

    private Vector3 move;
    public Animator playerAnimator;
    Transform cameraTransform;
    float pitch = 0f;
    [Range(1f, 90f)]
    public float maxPitch = 85f;
    [Range(-1f, -90f)]
    public float minPitch = -85f;
    [Range(0.5f, 5f)]
    public float mouseSensitivity = 2f;
    public Transform characterModel; // Reference to the character model object
    CharacterController cc;
    private Animator anim;

    public float attackRange = 2f; // The range of the attack
    public int attackDamage = 20; // The damage dealt by the attack

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
        anim = GetComponentInChildren<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Look();
        Move();
        // Check for attack input (e.g., left mouse button)
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            playerAnimator.SetTrigger("Attack"); // Trigger the attack animation
        }
    }

    void Look()
    {
        float xInput = Input.GetAxis("Mouse X") * mouseSensitivity;
        float yInput = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the entire player GameObject for yaw (left and right)
        transform.Rotate(0, xInput, 0);

        // Adjust the camera's pitch (up and down)
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

    void Attack()
    {
        // Perform a raycast from the player's position in the forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            Pig pig = hit.collider.GetComponent<Pig>();
            if (pig != null)
            {
                pig.TakeDamage(attackDamage, transform.position); // Pass the attacker's position
            }
        }
    }
}