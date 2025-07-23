using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speed;
    float horizontalInput;
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput == -1) _rotation = Vector3.up;
        else if (horizontalInput == 1) _rotation = Vector3.down;
        else _rotation = Vector3.zero;
        
        transform.Rotate(_rotation * _speed * Time.deltaTime);
    }
}
