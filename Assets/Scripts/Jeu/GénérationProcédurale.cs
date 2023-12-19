using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GénérationProcédurale : MonoBehaviour
{
    private MeshFilter meshFilter;
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = CreateMesh();
    }

    private Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = CreateVertices();
        mesh.triangles = CreateTriangles();
        mesh.RecalculateNormals();
        return mesh;
    }

    private Vector3[] CreateVertices()
    {
        Vector3[] vertices =
        {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 1),
            new Vector3(1, 1, 1),
            new Vector3(0.5f, 2.5f, 0.5f)
        };
        return vertices;
    }

    private int[] CreateTriangles()
    {
        return new int[]
        {
            2, 1, 0, 
            2, 3, 1,
            0, 4, 5,
            5, 2, 0, 
            5, 3, 2,
            0, 1, 4,
            1, 6, 4,
            5, 7, 3,
            1, 3, 7,
            7, 6, 1,
            4, 8, 5, 
            5, 8, 7,
            6, 8, 4,
            7, 8, 6
        };
    }
}
