using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateToForward : MonoBehaviour
{
    Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_rb.velocity != Vector2.zero)
            transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.down, _rb.velocity));
    }
}
