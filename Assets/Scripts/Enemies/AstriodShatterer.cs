using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstriodShatterer : MonoBehaviour
{ 
    [SerializeField]
    private MeshFilter shardPrefab;

    [Header("Stats")]
    [SerializeField]
    private int minPieces = 3;

    [SerializeField]
    private int maxPieces = 6;

    public List<MeshFilter> Shatter(MeshFilter meshFilter)
    {
        int pieces = UnityEngine.Random.Range(minPieces, maxPieces + 1);
        Mesh mesh = meshFilter.sharedMesh;
        int corners = mesh.vertices.Length;

        List<Mesh> meshes = new List<Mesh>();

        float anglePerPiece = (2.0f * Mathf.PI) / (float)pieces;
        int lastPieceIndex = 1;

        for (int g = 1; g < pieces + 1; g++)
        {
            List<Vector3> vertices = new List<Vector3>();
            vertices.Add(Vector3.zero);
            for (int i = lastPieceIndex; i < mesh.vertices.Length + 1; i++)
            {
                if (i < mesh.vertices.Length)
                {
                    vertices.Add(mesh.vertices[i]);
                }

                float angle = ((float)i / (float)corners) * 2 * Mathf.PI;

                if (angle >= anglePerPiece * g)
                {
                    if (i == mesh.vertices.Length)
                    {
                        vertices.Add(mesh.vertices[1]);
                    }
                    else if (i < mesh.vertices.Length - 1)
                    {
                        vertices.Add(mesh.vertices[i + 1]);
                    }

                    meshes.Add(GenerateMesh(vertices));
                    lastPieceIndex = i + 1;
                    break;
                }
            }
        }

        List<MeshFilter> shatters = new List<MeshFilter>();
        for (int i = 0; i < meshes.Count; i++)
        {
            MeshFilter gm = Instantiate(shardPrefab, meshFilter.transform.position, meshFilter.transform.rotation);
            gm.mesh = meshes[i];
            gm.transform.localScale = meshFilter.transform.localScale;

            shatters.Add(gm);
        }

        return shatters;
    }

    private Mesh GenerateMesh(List<Vector3> vertices)
    {
        MeshData meshData = new MeshData(vertices.ToArray());

        for (int i = 1; i < meshData.vertices.Length - 1; i++)
        {
            meshData.AddTriangle(0, i + 1, i);
        }

        Mesh mesh = meshData.CreateMesh();
        mesh.hideFlags = HideFlags.DontSave;
        return mesh;
    }
}
