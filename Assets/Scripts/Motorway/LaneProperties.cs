using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneProperties : MonoBehaviour {
    
    public enum direction {
        East = 1,
        West = -1
    }

    public direction dir;
    public Vector3 spawnPos;
    public Vector3 deSpawnPos;
    public float distance;
    public int laneIndex;

    public bool isFree;
    public GameObject motorwayManager;
    public Vehicles vehicles;

    public Queue<GameObject> vehiclePool;

    private float yPos;

    public void Start() {
        yPos = gameObject.transform.position.y;
        spawnPos = new Vector3(300f * (int) dir * -1, yPos, 0);
        deSpawnPos = new Vector3(300f * (int) dir, yPos, 0);
    }

    public void setAssignments() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        vehicles = motorwayManager.GetComponent<Vehicles>();
        vehiclePool = motorwayManager.GetComponent<VehiclePool>().vehiclePool;
        isFree = true;
        
    }
}