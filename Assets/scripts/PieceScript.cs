using System;
using UnityEngine;

public class PieceScript : MonoBehaviour
{

    void Start()
    {
        transform.position = new Vector3(
            UnityEngine.Random.Range(1f, 8f),
            UnityEngine.Random.Range(26f,30f),
            -0.01950645f
        );
    }

}