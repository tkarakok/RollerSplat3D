using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public bool isPainted;
    private Vector3 _position;

    private void Awake()
    {
        _position = transform.position;
        isPainted = false;
    }
}
