using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DeformationMaillage : MonoBehaviour
{
    [SerializeField] float intervaleEnSecondes = 1;
    [SerializeField] float deformationMultiplier = 3;

    Mesh mesh;
    float elapsedTime = 0;

    int compteur2sec = 0;
    Vector3[] verticesModified;

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        verticesModified = mesh.vertices;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervaleEnSecondes)
        {
            compteur2sec++;

            if (compteur2sec % 2 == 0)
            {
                ModifierVerticesSelonTemps(true);
            }
            else
            {
                ModifierVerticesSelonTemps(false);
            }
            elapsedTime = 0;
            RecalculerMesh();
        }
    }

    void RecalculerMesh()
    {
        mesh.vertices = verticesModified;
        mesh.RecalculateNormals();
    }

    void ModifierVerticesSelonTemps(bool equal0)
    {
        for (int i = 0; i < verticesModified.Length; i++)
        {
            if (i % 2 == 0)
                verticesModified[i] = verticesModified[i] + (equal0 ? Vector3.up : Vector3.left) * Time.deltaTime * deformationMultiplier;
            else
                verticesModified[i] = verticesModified[i] + (equal0 ? Vector3.right : Vector3.down) * Time.deltaTime * deformationMultiplier;
        }

        verticesModified[3] = verticesModified[3] + Vector3.up * Time.deltaTime * deformationMultiplier;
        verticesModified[7] = verticesModified[7] + Vector3.down * Time.deltaTime * deformationMultiplier;
        verticesModified[12] = verticesModified[12] + Vector3.right * Time.deltaTime * deformationMultiplier;
        verticesModified[16] = verticesModified[16] + Vector3.left * Time.deltaTime * deformationMultiplier;
    }
}
