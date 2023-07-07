using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlightController : MonoBehaviour
{
    private const string FLIGHT_AXIS_NAME = "Horizontal";

    public float RotationSpeed                 = 200f;
    public float Acceleration                  =   1f;
    public float RotationCorrectionStrengthMin =  10f;
    public float RotationCorrectionStrengthMax =  25f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*
         * Apply user input to rotation
         */
        float axis = -Input.GetAxisRaw(FLIGHT_AXIS_NAME);
        transform.Rotate(new Vector3(0, 0, RotationSpeed * axis * Time.deltaTime));

        /*
         * Apply correction to rotation
         */
        float distanceFromDown;
        float sign;

        //Tilting towards left
        if (transform.eulerAngles.z > 180)
        {
            distanceFromDown = 360 - transform.eulerAngles.z;
            sign = 1;
        }

        //Tilting towards right
        else
        {
            distanceFromDown = transform.eulerAngles.z;
            sign = -1;
        }

        transform.Rotate(sign * Mathf.Lerp(RotationCorrectionStrengthMin, RotationCorrectionStrengthMax, distanceFromDown / 180f) * Time.deltaTime);

        /*
         * Apply forward force
         */
        _rigidbody.AddForce(-transform.up * Acceleration);
        _rigidbody.velocity = -transform.up * _rigidbody.velocity.magnitude;
    }
}
