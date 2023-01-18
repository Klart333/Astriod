using System.Collections;
using UnityEngine;

public class AudioSyncSpriteHeight : AudioSyncer
{
    [Header("Scale")]
    public float beatHeight = 5;
    public float restHeight = 1;

    [Header("Options")]
    [SerializeField]
    private bool height = true;

    [SerializeField]
    private bool color = false;

    [Header("Color")]
    [SerializeField]
    private Color restColor;

    [SerializeField]
    private Color beatColor;

    private Vector2 size;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        size = spriteRenderer.size;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (isBeat)
        {
            return;
        }

        if (height)
        {
            size.y = Mathf.Lerp(size.y, restHeight, RestSmoothTime * Time.deltaTime);
        }
        else
        {
            size.x = Mathf.Lerp(size.x, restHeight, RestSmoothTime * Time.deltaTime);
        }

        spriteRenderer.size = size;

        if (color)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, restColor, RestSmoothTime * Time.deltaTime);
        }
    }

    protected override void OnBeat()
    {
        base.OnBeat();

        StopAllCoroutines();
        StartCoroutine(MoveToScale());
    }

    private IEnumerator MoveToScale()
    {
        float startHeight = size.y;
        Color startColor = spriteRenderer.color;

        if (!height)
        {
            startHeight = size.x;
        }

        float t = 0;

        while (t <= 1.0f)
        {
            t += Time.deltaTime / TimeToBeat;

            if (height)
            {
                size.y = Mathf.Lerp(startHeight, beatHeight, t);
            }
            else
            {
                size.x = Mathf.Lerp(startHeight, beatHeight, t);
            }
            spriteRenderer.size = size;

            if (color)
            {
                spriteRenderer.color = Color.Lerp(startColor, beatColor, t);
            }

            yield return null;
        }

        isBeat = false;
    }
}
