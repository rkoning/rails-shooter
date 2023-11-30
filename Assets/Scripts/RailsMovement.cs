using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class RailsMovement : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private float horizontalInput;
    private float verticalInput;
    private bool boostInput;
    private bool brakeInput;
    private bool fireInput;
    private bool secondaryInput;

    public float Speed = 5f;

    public float maxRollDegrees = 30f;
    public float maxPitchDegrees = 30f;

    public Transform body;
    private float bodyRollAngle = 0f;
    public float rollSpeed = 12f;
    private float bodyPitchAngle = 0f;
    public float pitchSpeed = 12f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CaptureInputs();
        // SetBodyAngle();
    }

    void FixedUpdate() {
        Move();
        ClampPosition();
    }

    private void Move() {
        Vector3 delta = new Vector3(horizontalInput, verticalInput, 0f) * Speed * Time.fixedDeltaTime;
        
        transform.position += delta;
    }

    private void ClampPosition() {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }

    private void CaptureInputs() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        boostInput = Input.GetButtonDown("Boost");
        brakeInput = Input.GetButtonDown("Brake");

        fireInput = Input.GetButtonDown("Fire");
        secondaryInput = Input.GetButtonDown("Secondary");
    }

    private void SetBodyAngle() {
        if (!body)
            return;

        bodyRollAngle = NextBodyAngle(horizontalInput, bodyRollAngle, Time.deltaTime * rollSpeed, maxRollDegrees);
        bodyPitchAngle = NextBodyAngle(verticalInput, bodyPitchAngle, Time.deltaTime * pitchSpeed, maxPitchDegrees);

        body.rotation = Quaternion.Euler(bodyPitchAngle, 0, bodyRollAngle);
    }

    private float NextBodyAngle(float inputValue, float angle, float speed, float max) {
        if (Mathf.Abs(inputValue) > 0.1f) {
            angle -= inputValue * speed;
        } else {
            angle = ReduceToZero(angle, speed);
        }
        return Mathf.Clamp(angle, -max, max);
    } 

    private float ReduceToZero(float value, float delta) {
        if (value > 0) {
            return Mathf.Clamp(value - delta, 0, value);
        } else {
            return Mathf.Clamp(value + delta, -value, 0);
        }
    }
}
