using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private ShootingData data;

    private InputActions inputActions;
    private InputAction fire;

    private float cooldown = 0;

    private void OnEnable()
    {
        inputActions = new InputActions();
        fire = inputActions.Player.Fire;
        fire.Enable();
    }

    private void OnDisable()
    {
        fire.Disable();
    }

    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }

        if (fire.IsPressed())
        {
            DoTheShoot();
        }
    }

    private void DoTheShoot()
    {
        cooldown = 1.0f / data.FireRate;

        Vector3 pos = transform.position + transform.up * 0.5f;
        var shot = Instantiate(data.Shot, pos, Quaternion.LookRotation(Vector3.forward, transform.up));

        Rigidbody2D rb = shot.GetComponent<Rigidbody2D>();
        Vector2 force = transform.up * data.BulletSpeed;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
