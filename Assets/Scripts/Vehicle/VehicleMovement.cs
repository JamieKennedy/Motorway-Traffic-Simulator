using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour {
    

    private GameObject motorwayManager;
    private Parameters motorwayParameters;

    private VehicleProperties vehicleProperties;
    
    // Start is called before the first frame update
    void Start() {
        vehicleProperties = gameObject.GetComponent<VehicleProperties>();
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        motorwayParameters = motorwayManager.GetComponent<Parameters>();
        vehicleProperties.desiredSpeed =
            Random.Range(motorwayParameters.speedLimit - 10, motorwayParameters.speedLimit + 10);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (vehicleProperties.canMove) {
            gameObject.transform.position += new Vector3(vehicleProperties.desiredSpeed * 0.02f * (int) vehicleProperties.direction, 0 , 0);
        }
    }
}