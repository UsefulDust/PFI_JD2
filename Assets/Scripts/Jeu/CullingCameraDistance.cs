using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingCameraDistance : MonoBehaviour
{
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        float[] distances = new float[32];
        distances[10] = 20;
        camera.layerCullDistances = distances;
    }
}
