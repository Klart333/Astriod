using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float rotationSpeed = 10;

    [Header("Particles")]
    [SerializeField]
    private ParticleSystem rocketParticles;

    private PlayerDeath playerDeath;
    private InputActions inputActions;
    private InputAction move;

    private Rigidbody2D rb;

    private void OnEnable()
    {
        playerDeath = GetComponent<PlayerDeath>();
        rb = GetComponent<Rigidbody2D>();

        inputActions = new InputActions();
        move = inputActions.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        if (!playerDeath.Alive)
        {
            if (rocketParticles.isPlaying)
            {
                rocketParticles.Stop();
            }
            return;
        }

        Vector2 movement = move.ReadValue<Vector2>();

        Move(movement);
        Rotate(movement);
    }

    private void Rotate(Vector2 movement)
    {
        float angle = -movement.x * rotationSpeed * Time.deltaTime;
        rb.AddTorque(angle);
    }

    private void Move(Vector2 movement)
    {
        float gas = Mathf.Clamp01(movement.y);
        Vector3 force = transform.up * gas * speed * 50 * Time.deltaTime;
        rb.AddForce(force);

        if (movement.y > 0)
        {
            if (!rocketParticles.isPlaying)
            {
                rocketParticles.Play();
            }
        }
        else
        {
            if (rocketParticles.isPlaying)
            {
                rocketParticles.Stop();
            }
        }
    }
}
