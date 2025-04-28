using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] CharacterController controller;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    [Header("Settings")]
    [SerializeField] float speed = 3f;
    [SerializeField] float gravity = -9.81f * 2;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float groundDistance = 0.1f;

    Vector3 velocity;

    [SerializeField] bool isGrounded;

    private void Update()
    {
        //burasý yere deðip deðmediðimizi kontrol ediyor eðer yere deðmiyorsak sürekli daha hýzlý yere düþmesini saðlayacak
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = MathF.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }
}
