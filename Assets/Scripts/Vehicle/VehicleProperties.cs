using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleProperties : MonoBehaviour {
    
    private GameObject motorwayManager;
    private Parameters motorwayParameters;
    
    public float desiredSpeed;
    public int freeAccExponent;
    public float desiredTimeGap;
    public float jamDistance;
    public float maxAccel;
    public float desiredDecel;
    public float politeness;

    public float currentVel;
    public float currentAccel;
    
    public bool canMove = false;
    public int currentLane;
    public LaneProperties.direction direction;
    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        motorwayParameters = motorwayManager.GetComponent<Parameters>();
        
        setParameters();

        currentVel = Random.Range(motorwayParameters.speedLimit * 0.8f, motorwayParameters.speedLimit * 1.2f);
        currentAccel = 0f;
    }

    private void setParameters() {
        desiredSpeed = Random.Range(motorwayParameters.speedLimit * 0.8f, motorwayParameters.speedLimit * 1.2f);

        freeAccExponent = 4;

        desiredTimeGap = Random.Range(1.5f * 0.8f, 1.5f * 1.2f);

        jamDistance = Random.Range(2f * 0.8f, 2f * 1.2f);

        maxAccel = Random.Range(1.4f * 0.8f, 1.4f * 1.2f);

        desiredDecel = Random.Range(2f * 0.8f, 2f * 1.2f);

        politeness = Random.Range(((motorwayParameters.politeness * 2) / 100) * 0.8f,
            ((motorwayParameters.politeness * 2) / 100) * 1.2f);
    }
}