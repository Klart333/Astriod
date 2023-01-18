using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Astriod>(out Astriod astriod))
        {
            astriod.Explode();
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Astriod>(out Astriod astriod))
        {
            astriod.Explode();
        }

        Destroy(gameObject);
    }
}
