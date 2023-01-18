using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AstriodSpawner : MonoBehaviour
{
    [SerializeField]
    private Astriod asteroidPrefab;

    [SerializeField]
    private SpawnerData data;

    [Header("Area")]
    [SerializeField]
    private Vector2 minSpawnArea;

    [SerializeField]
    private Vector2 maxSpawnArea;

    private AstriodGenerator generator;

    private float timer = 0;
    private float gameTimer = 0;

    private void Start()
    {
        generator = FindObjectOfType<AstriodGenerator>();
    }

    private void Update()
    {
        gameTimer += Time.deltaTime;
        timer += Time.deltaTime;

        if (timer > 1.0f / GetSpawnRate())
        {
            timer = 0;
            Spawn();
        }
    }

    private float GetSpawnRate()
    {
        return data.SpawnRate + data.RateIncrease * gameTimer / 10.0f;
    }

    private void Spawn()
    {
        Vector3 pos = GetPos();
        Astriod astriod = Instantiate(asteroidPrefab, pos, Quaternion.identity);
        float size = UnityEngine.Random.Range(data.MinSize, data.MaxSize);
        astriod.transform.localScale = Vector3.one * size;

        Mesh mesh = generator.Generate();
        astriod.GetComponent<MeshFilter>().mesh = mesh;

        Vector3 dir = -pos.normalized;
        Vector3 direction = Quaternion.AngleAxis(UnityEngine.Random.Range(-data.Angle, data.Angle) / 2.0f, Vector3.forward) * dir;

        float force = GetForce() / (size + 0.4f);
        astriod.GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);

        PolygonCollider2D col = astriod.AddComponent<PolygonCollider2D>();

        Vector2[] path = new Vector2[mesh.vertexCount - 1];
        for (int i = 1; i < mesh.vertexCount; i++)
        {
            path[i - 1] = mesh.vertices[i];
        }
        col.SetPath(0, path);
    }

    private float GetForce()
    {
        return UnityEngine.Random.Range(data.MinForce * (1.0f + data.ForceIncrease * gameTimer / 10.0f), data.MaxForce * (1.0f + data.ForceIncrease * gameTimer / 10.0f));
    }

    private Vector3 GetPos()
    {
        int section = UnityEngine.Random.Range(0, 4);

        switch (section)
        {
            case 0:
                return new Vector2(UnityEngine.Random.Range(-minSpawnArea.x, minSpawnArea.x), UnityEngine.Random.Range(minSpawnArea.y, maxSpawnArea.y)) / 2.0f;
            case 1:
                return new Vector2(UnityEngine.Random.Range(-minSpawnArea.x, minSpawnArea.x), UnityEngine.Random.Range(-maxSpawnArea.y, -minSpawnArea.y)) / 2.0f;
            case 2:
                return new Vector2(UnityEngine.Random.Range(-maxSpawnArea.x, -minSpawnArea.x), UnityEngine.Random.Range(-maxSpawnArea.y, maxSpawnArea.y)) / 2.0f;
            case 3:
                return new Vector2(UnityEngine.Random.Range(maxSpawnArea.x, minSpawnArea.x), UnityEngine.Random.Range(-maxSpawnArea.y, maxSpawnArea.y)) / 2.0f;
            default:
                break;
        }

        return Vector3.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, minSpawnArea);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, maxSpawnArea);
    }
}
