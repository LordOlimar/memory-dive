using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameObject targetObject;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speed;
    float horizontalInput;
    bool enabled;


    void rotate()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput == -1)
            /*transform.rotation = Quaternnion.Euler(x, y, (z + 10));*/
        _rotation = Vector3.up;
        else if (horizontalInput == 1)
       /*transform.rotation = Quaternnion.Euler(x, y, (z - 10));*/
            _rotation = Vector3.down;
            else _rotation = Vector3.zero;

            transform.Rotate(_rotation * _speed * Time.deltaTime);
    }
    void Update()
    {
        if (enabled)
            rotate();
    }
}
