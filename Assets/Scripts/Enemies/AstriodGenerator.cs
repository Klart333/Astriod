using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

public class AstriodGenerator : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private int corners;

    [SerializeField]
    private float randomness = 0.2f;

    public Mesh Generate()
    {
        List<Vector3> vertices = new List<Vector3>();
        vertices.Add(Vector3.zero);

        for (int i = 0; i < corners; i++)
        {
            float angle = ((float)i / (float)corners) * 2 * Mathf.PI;

            Vector2 random = new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)) * randomness;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) + (Vector3)random;

            vertices.Add(pos);
        }

        Mesh mesh = GenerateMesh(vertices);
        return mesh;
    }

    private Mesh GenerateMesh(List<Vector3> vertices)
    {
        MeshData meshData = new MeshData(vertices.ToArray());

        for (int i = 1; i < meshData.vertices.Length; i++)
        {
            if (i == meshData.vertices.Length - 1)
            {
                meshData.AddTriangle(0, 1, i);
            }
            else
            {
                meshData.AddTriangle(0, i + 1, i);
            }
        }

        Mesh mesh = meshData.CreateMesh();
        mesh.hideFlags = HideFlags.DontSave;
        return mesh;
    }
}
