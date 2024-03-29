using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

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
    private bool dodgeInput;
    public float DodgeRange;
    public float DodgeCooldown;
    private float lastDodge;

    public float Speed = 5f;

    public Vector3 bodyTurnOffsetTarget;
    public float boostTurnScaling;

    public Transform ship;
    private Rigidbody shipRB;
    public Transform body;
    public float turnSpeed = 12f;
    public Weapon primary;
    public Weapon secondary;

    public Rail CurrentRail;
    public float RailTurnSpeed = 5f;

    public float RailSpeed = 5f;
    public float BoostSpeed = 10f;
    public float BrakeSpeed = 2f;
    private bool isBoosting = false;

    public float FuelCap = 100f;
    public float FuelDrainPerSecond = 25f;
    public float FuelRechargePerSecond = 20f;
    public float FuelRechargeDelay = 1.5f;
    private float lastFuelExpenditure = float.MinValue;
    public float CurrentFuel { get; private set; }

    public bool RadiusMode = false;
    private float currentRadius = 15f;
    public float MinRadius = 10f;
    public float MaxRadius = 20f;

    public static RailsMovement Player;

    public ParticleSystem BoostParticles;
    
    public void KillShip() {
        ship.gameObject.SetActive(false);
    }

    public void SpawnShip() {
        ship.gameObject.SetActive(true);
        ship.transform.localPosition = Vector3.zero;
    }

    public void SpawnShipAfter(float seconds) {
        StartCoroutine(WaitThenSpawnShip(seconds));
    }

    private IEnumerator WaitThenSpawnShip(float seconds) {
        yield return new WaitForSeconds(seconds);
        SpawnShip();
    }

    // Start is called before the first frame update
    void Start()
    {
        Player = this;
        shipRB = ship.GetComponent<Rigidbody>();
        CurrentFuel = FuelCap;
        if (CurrentRail == null) {
            Debug.LogWarning($"No Rail assigned to: {name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CaptureInputs();
        if (!RadiusMode)
            SetBodyAngle();
        TriggerWeapons();
    }

    void FixedUpdate() {
        RailMove();
        if (RadiusMode) {
            MoveShipOnRadius();
        } else {
            MoveShip();
            ClampBodyPosition();
        }
    }

    private void RailMove() {
        float dt = Time.fixedDeltaTime;
        float speed = RailSpeed;
        isBoosting = boostInput && CurrentFuel > 0;
        
        if (isBoosting) {
            if (!BoostParticles.isPlaying)
                BoostParticles.Play();
        } else {
            if (BoostParticles.isPlaying)
                BoostParticles.Stop();
        }

        if (isBoosting) {
            speed = BoostSpeed;
            CurrentFuel -= FuelDrainPerSecond * dt;
            lastFuelExpenditure = Time.fixedTime;
            isBoosting = true;
        }
        else if (brakeInput && CurrentFuel > 0) {
            speed = BrakeSpeed;
            CurrentFuel -= FuelDrainPerSecond * dt;
            lastFuelExpenditure = Time.fixedTime;
        }
        else if (lastFuelExpenditure + FuelRechargeDelay < Time.fixedTime) {
            CurrentFuel += FuelRechargePerSecond * dt;
        }

        Vector3 direction = (CurrentRail.GetNextRailPoint(transform) - transform.position).normalized;
        transform.position += direction * speed * dt;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), RailTurnSpeed * Time.fixedDeltaTime);
    }

    private void MoveShipOnRadius() {
        // Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        Vector3 center = transform.position;

        Vector3 diff = ship.localPosition;
        currentRadius = diff.magnitude;

        float currentAngleRadians = Mathf.Atan2(diff.y, diff.x);

        float dRadians = currentAngleRadians - horizontalInput * Time.deltaTime * Speed * Mathf.PI / 180;

        currentRadius += verticalInput * Time.deltaTime * Speed;
        currentRadius = Mathf.Clamp(currentRadius, MinRadius, MaxRadius);
        Vector3 localPosition = currentRadius * new Vector3(Mathf.Cos(dRadians), Mathf.Sin(dRadians), 0f);
        // if (dodgeInput && Time.fixedTime > lastDodge + DodgeCooldown) {
        //     lastDodge = Time.fixedTime;
        //     position += transform.rotation * direction * DodgeRange;
        // } else {
        // Vector3 delta = transform.rotation * direction * Speed * Time.fixedDeltaTime;
        // position += delta;
        // }
        localPosition = transform.rotation * localPosition;
        ship.position = center + localPosition;
        ship.rotation = Quaternion.LookRotation(transform.forward, localPosition - center);
    }

    private void MoveShip() {
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f).normalized;
        Vector3 position = ship.position;
        if (dodgeInput && Time.fixedTime > lastDodge + DodgeCooldown) {
            lastDodge = Time.fixedTime;
            position += transform.rotation * direction * DodgeRange;
        } else {
            Vector3 delta = transform.rotation * direction * Speed * Time.fixedDeltaTime;
            position += delta;
        }
        ship.position = position;
    }

    private void ClampBodyPosition() {
        Vector3 pos = ship.position;
        pos.x = Mathf.Clamp(pos.x, transform.position.x + minX, transform.position.x + maxX);
        pos.y = Mathf.Clamp(pos.y, transform.position.y + minY, transform.position.y + maxY);
        ship.position = pos;
    }

    private void CaptureInputs() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        boostInput = Input.GetButton("Boost");
        brakeInput = Input.GetButton("Brake");
        dodgeInput = Input.GetButton("Dodge");

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

        Vector3 offset = new Vector3(
            horizontalInput * bodyTurnOffsetTarget.x,
            verticalInput * bodyTurnOffsetTarget.y,
            bodyTurnOffsetTarget.z * (isBoosting ? boostTurnScaling : 1f)
        ); 
        Vector3 targetPoint = transform.position 
            + transform.rotation
            * offset;

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
