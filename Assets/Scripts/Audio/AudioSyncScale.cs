using System.Collections;
using UnityEngine;

public class AudioSyncScale : AudioSyncer
{
    [Header("Scale")]
    public Vector3 beatScale;
    public Vector3 restScale;

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (isBeat)
        {
            return;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, restScale, RestSmoothTime * Time.deltaTime);
    }

    protected override void OnBeat()
    {
        base.OnBeat();

        StopAllCoroutines();
        StartCoroutine(MoveToScale(beatScale));
    }

    private IEnumerator MoveToScale(Vector3 beatScale)
    {
        Vector3 startScale = transform.localScale;

        float t = 0;

        while (t <= 1.0f)
        {
            t += Time.deltaTime / TimeToBeat;

            transform.localScale = Vector3.Lerp(startScale, beatScale, t);

            yield return null;
        }

        isBeat = false;
    }
}
