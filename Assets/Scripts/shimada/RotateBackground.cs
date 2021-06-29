using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBackground : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, _rotateSpeed);
    }
}
