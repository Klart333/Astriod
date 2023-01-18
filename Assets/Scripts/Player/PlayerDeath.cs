using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private float deathDistance;

    [SerializeField]
    private Color deathColor;

    [SerializeField]
    private float deathSpeed;

    [SerializeField]
    private float bigScale;

    private SpriteRenderer rend;

    public bool Alive { get; private set; } = true;

    private void Awake()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Mathf.Abs(transform.position.x) > deathDistance || Mathf.Abs(transform.position.y) * 2 > deathDistance)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Astriod>())
        {
            Die();
        }
    }

    private async void Die()
    {
        if (!Alive)
        {
            return;
        }

        Alive = false;

        float t = 0;

        Color startColor = rend.color;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * bigScale;

        while (t <= 1.0f)
        {
            t += Time.deltaTime * deathSpeed;

            rend.color = Color.Lerp(startColor, deathColor, Math.EaseInCubic(t));
            transform.localScale = Vector3.Lerp(startScale, targetScale, Math.EaseInCubic(t));

            await Task.Yield();
        }

        Destroy(gameObject);

        await Task.Delay(400);

        ScreenTransition.Instance.LoadScene(0);
    }
}
