using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using UnityEngine;
using Color = UnityEngine.Color;

public class Astriod : MonoBehaviour
{
    private static readonly int shPropColor = Shader.PropertyToID("_EmissionColor");

    [Header("Astriod")]
    [SerializeField]
    private Gradient colorVariance;

    [SerializeField]
    private int points = 10;

    [Header("Explosion")]
    [SerializeField]
    private float explosionForce = 50;

    [SerializeField]
    private float expansionScale = 1.2f;

    [SerializeField]
    private float expansionSpeed = 0.3f;

    [SerializeField]
    private Gradient expansionGradient;

    [Header("Audio")]
    [SerializeField]
    private SimpleAudioEvent shatterSound;

    private MaterialPropertyBlock block;
    private new Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();

        float t = UnityEngine.Random.value;
        renderer.GetPropertyBlock(block);
        block.SetColor(shPropColor, colorVariance.Evaluate(t) * 1.1f);
        renderer.SetPropertyBlock(block);
    }

    public async void Explode()
    {
        AudioManager.Instance.PlaySoundEffect(shatterSound);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        GetComponentInChildren<Collider2D>().enabled = false;

        float t = 0;

        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale * expansionScale;

        while (t <= 1.0f)
        {
            t += Time.deltaTime / expansionSpeed;

            transform.localScale = Vector3.Lerp(startScale, targetScale, Math.EaseInCubic(t));

            Color color = expansionGradient.Evaluate(t);

            renderer.GetPropertyBlock(block);
            block.SetColor(shPropColor, color * (t + 1));
            renderer.SetPropertyBlock(block);

            //renderer.material.color = color;

            await Task.Yield();
        }

        List<MeshFilter> pieces = FindObjectOfType<AstriodShatterer>().Shatter(GetComponent<MeshFilter>());
        SpawnPieces(pieces);

        ScoreManager.Instance.IncreaseScore(Mathf.FloorToInt(points / transform.transform.localScale.magnitude), transform.position);

        Destroy(gameObject);
    }

    private void SpawnPieces(List<MeshFilter> pieces)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            Rigidbody2D rb = pieces[i].gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.drag = 0.5f;

            pieces[i].GetComponent<MeshRenderer>().SetPropertyBlock(block);

            Vector3 pos = GetAverageVertexPos(pieces[i].sharedMesh, transform.rotation);
            Vector3 dir = pos.normalized;

            rb.AddForce(dir * explosionForce, ForceMode2D.Impulse);

        }
    }

    private Vector3 GetAverageVertexPos(Mesh mesh, Quaternion rotation)
    {
        Vector3 pos = Vector3.zero;

        for (int i = 0; i < mesh.vertexCount; i++)
        {
            pos += rotation * mesh.vertices[i];
        }

        return pos / (float)mesh.vertexCount;
    }
}
