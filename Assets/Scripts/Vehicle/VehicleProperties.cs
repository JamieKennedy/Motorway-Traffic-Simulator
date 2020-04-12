using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class VehicleProperties : MonoBehaviour {
    
    private GameObject motorwayManager;
    private Parameters motorwayParameters;
    
    public float desiredSpeed;
    public float desiredSpeedPerm;
    public int freeAccExponent;
    public float desiredTimeGap;
    public float jamDistance;
    public float maxAccel;
    public float desiredDecel;
    public float politeness;
    public float vehicleWidth;
    public float changeThreshold;
    public float mobil;
    public float laneChangeCoolDown;

    public float currentVel;
    public float currentAccel;
    
    public bool canMove = false;
    public int currentLane;
    public LaneProperties.direction direction;
    public bool hasStopped;
    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        motorwayParameters = motorwayManager.GetComponent<Parameters>();

        vehicleWidth = gameObject.GetComponent<RectTransform>().rect.width * gameObject.transform.localScale.x;

        //currentVel = Random.Range(motorwayParameters.speedLimit * 0.8f, motorwayParameters.speedLimit * 1.2f);
        //currentVel = 0f;
        currentAccel = 0f;
        changeThreshold = 0.5f;
        laneChangeCoolDown = 10f;
        hasStopped = false;
    }

    public void setParameters() {
        SetDesiredSpeed();
        SetFreeExponent();
        SetDesiredTimeGap();
        SetJamDistance();
        SetMaxAccel();
        SetDesiredDecel();
        SetPoliteness();
    }

    private float NormalDist(float mu, float sigma) {
        var u1 = Random.Range(0f, 1f);
        var u2 = Random.Range(0f, 1f);

        var z = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);

        return Convert.ToSingle(z * sigma + mu);
    }

    public void SetDesiredSpeed() {
        desiredSpeed = NormalDist(motorwayParameters.speedLimit, motorwayParameters.speedLimit * 0.1f);
        desiredSpeedPerm = desiredSpeed;
    }

    public void SetFreeExponent() {
        freeAccExponent = 4;
    }

    public void SetDesiredTimeGap() {
        desiredTimeGap = NormalDist(1f, 0.2f);
    }

    public void SetJamDistance() {
        jamDistance = NormalDist(10f, 0.2f) + vehicleWidth / 2;
    }

    public void SetMaxAccel() {
        maxAccel = NormalDist(4f, 0.2f);
    }

    public void SetDesiredDecel() {
        desiredDecel = NormalDist(2f, 0.2f);
    }

    public void SetPoliteness() {
        politeness = NormalDist((motorwayParameters.politeness) / 100,
            ((motorwayParameters.politeness) / 100) * 0.1f);
    }
}