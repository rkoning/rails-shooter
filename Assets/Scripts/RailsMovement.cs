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

    public Vector3 bodyTurnOffsetTarget;

    public Transform body;
    public float turnSpeed = 12f;
    public Weapon primary;
    public Weapon secondary;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CaptureInputs();
        SetBodyAngle();
        TriggerWeapons();
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

    private void TriggerWeapons() {
        if (fireInput) {
            primary.Fire();
        }

        if (secondaryInput) {
            secondary.Fire();
        }
    }

    private void SetBodyAngle() {
        if (!body)
            return;

        Vector3 targetPoint = transform.position 
            + transform.rotation 
            * new Vector3(
                horizontalInput * bodyTurnOffsetTarget.x, 
                verticalInput * bodyTurnOffsetTarget.y, 
                bodyTurnOffsetTarget.z
            );

        var forward = targetPoint - transform.position;
        var rot = Quaternion.LookRotation(forward);
        body.rotation = Quaternion.Lerp(body.rotation, rot, turnSpeed * Time.deltaTime);
    }

    private float ReduceToZero(float value, float delta) {
        if (value > 0) {
            return Mathf.Clamp(value - delta, 0, value);
        } else if (value < 0) {
            return Mathf.Clamp(value + delta, -value, 0);
        }
        return 0;
    }
}
