using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControls : MonoBehaviour {
    public bool breakingDown;
    public bool emergencyBraked;
    private VehicleProperties vehicleProperties;
    // Start is called before the first frame update
    void Start() {
        breakingDown = false;
        vehicleProperties = gameObject.GetComponent<VehicleProperties>();
    }

    // Update is called once per frame
    void Update() {
        SlowDown();
        EmergencyBrake();
    }
    
    private void SlowDown() {
        if (vehicleProperties.desiredSpeed >= 0 && breakingDown) {
            vehicleProperties.hasStopped = true;
            if (vehicleProperties.desiredSpeed - 0.1f < 0) {
                vehicleProperties.desiredSpeed = 0f;
            } else {
                vehicleProperties.desiredSpeed -= 0.1f;
            }
        }
    }

    public void Resume() {
        vehicleProperties.desiredSpeed = vehicleProperties.desiredSpeedPerm;
        vehicleProperties.hasStopped = false;
    }

    private void EmergencyBrake() {
        if (emergencyBraked && vehicleProperties.desiredSpeed >= 0) {
            vehicleProperties.desiredSpeed = 0f;
            vehicleProperties.hasStopped = true;
        }
    }
}