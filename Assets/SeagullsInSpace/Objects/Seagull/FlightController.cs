using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlightController : MonoBehaviour
{
    private const string FLIGHT_AXIS_NAME = "Horizontal";

    public float RotationSpeed                    =  200f;
    public float Acceleration                     =    1f;
    public float RotationCorrectionStrengthMin    =   10f;
    public float RotationCorrectionStrengthMax    =   25f;
    public float RotationCorrectionStrengthTumble =  100f;
    public float TumbleThreshold                  =    0.5f;
    public float TumbleTime                       =    1f;

    private Rigidbody2D _rigidbody;

    private float TumbleTimer = 0;

    public bool IsTumbling
    {
        get
        {
            return TumbleTimer > 0;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*
         * Apply user input to rotation
         */
        if (!IsTumbling)
        {
            float axis = Input.GetAxisRaw(FLIGHT_AXIS_NAME);
            transform.Rotate(new Vector3(0, 0, RotationSpeed * axis * Time.deltaTime));
        }

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

        if (IsTumbling)
            transform.Rotate(sign * RotationCorrectionStrengthTumble * Time.deltaTime);
        else
            transform.Rotate(sign * Mathf.Lerp(RotationCorrectionStrengthMin, RotationCorrectionStrengthMax, distanceFromDown / 180f) * Time.deltaTime);

        /*
         * Apply forward force
         */
        _rigidbody.AddForce(-transform.up * Acceleration);
        _rigidbody.velocity = -transform.up * _rigidbody.velocity.magnitude;

        if (!IsTumbling)
        {
            //Player needs to tumble
            if (_rigidbody.velocity.magnitude < TumbleThreshold)
                TumbleTimer = TumbleTime;
        }

        //Player is tumbling. Tick timer
        else
        {
            TumbleTimer -= Time.deltaTime;
        }
    }
}
