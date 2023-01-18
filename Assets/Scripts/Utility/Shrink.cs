using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 1.0f;

    public int Points { get; set; }

    private void Start()
    {
        Shrinking();
    }

    private async void Shrinking()
    {
        await Task.Yield();
        float t = 0;

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;

        while (t <= 1.0f)
        {
            t += Time.deltaTime / lifeTime;

            if (transform == null)
            {
                return;
            }
            transform.localScale = Vector3.Lerp(startScale, targetScale, Math.EaseInCubic(t));

            await Task.Yield();
        }

        if (Points > 0)
        {
            ScoreManager.Instance.IncreaseScore(Points, transform.position);
        }

        Destroy(gameObject);
    }


}
