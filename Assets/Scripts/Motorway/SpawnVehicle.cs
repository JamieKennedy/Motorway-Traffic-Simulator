﻿using System.Collections.Generic;
using UnityEngine;

public class SpawnVehicle : MonoBehaviour {

    public LaneProperties laneProperties;
    private GameObject closestVehicle;
    private VehicleProperties vehicleProperties;
    private GameObject motorwayManager;
    private Parameters motorwayParameters;
    private MotorwayStats motorwayStats;
    private ChangeLanes changeLanes;
    private MotorwayTiming motorwayTiming;

    // Start is called before the first frame update
    void Start() {
        laneProperties = gameObject.GetComponent<LaneProperties>();
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        motorwayStats = motorwayManager.GetComponent<MotorwayStats>();
        motorwayParameters = motorwayManager.GetComponent<Parameters>();
        motorwayTiming = motorwayManager.GetComponent<MotorwayTiming>();
    }

    

    // Update is called once per frame
    void FixedUpdate() {
        switch (laneProperties.dir) {
            case LaneProperties.direction.East:
                var tempEast = true;
                if (laneProperties.vehicles.eastVehicles[laneProperties.laneIndex] != null) {
                    foreach (var vehicle in laneProperties.vehicles.eastVehicles[laneProperties.laneIndex]) {
                        if (Vector3.Distance(vehicle.transform.position, laneProperties.spawnPos) < laneProperties.distance) {
                            tempEast = false;
                            break;
                        }
                    }
                }
                
                laneProperties.isFree = tempEast;
                break;
            case LaneProperties.direction.West:
                var tempWest = true;
                if (laneProperties.vehicles.westVehicles[laneProperties.laneIndex] != null) {
                    foreach (var vehicle in laneProperties.vehicles.westVehicles[laneProperties.laneIndex]) {
                        if (Vector3.Distance(vehicle.transform.position, laneProperties.spawnPos) < laneProperties.distance) {
                            tempWest = false;
                            break;
                        }
                    }
                }

                laneProperties.isFree = tempWest;
                break;
        }
    }

    public void Spawn() {
        var vehicle = laneProperties.vehiclePool.Dequeue();
        vehicleProperties = vehicle.GetComponent<VehicleProperties>();
        vehicleProperties.setParameters();
        vehicle.transform.position = laneProperties.spawnPos;
        
        switch (laneProperties.dir) {
            case LaneProperties.direction.East:
                vehicle.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                motorwayStats.eastboundArrivalCount += 1;
                motorwayStats.eastVehicleArrivals.Add(motorwayTiming.elapsedTime);
                vehicleProperties.direction = LaneProperties.direction.East;
                vehicleProperties.canMove = true;
                vehicleProperties.currentLane = laneProperties.laneIndex;
                vehicleProperties.currentVel = startVel(vehicleProperties.direction, vehicleProperties.currentLane);
                laneProperties.vehicles.eastVehicles[laneProperties.laneIndex].Add(vehicle);
                break;
            case LaneProperties.direction.West:
                vehicle.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                motorwayStats.westboundArrivalCount += 1;
                motorwayStats.westVehicleArrivals.Add(motorwayTiming.elapsedTime);
                vehicleProperties.direction = LaneProperties.direction.West;
                vehicleProperties.canMove = true;
                vehicleProperties.currentLane = laneProperties.laneIndex;
                vehicleProperties.currentVel = startVel(vehicleProperties.direction, vehicleProperties.currentLane);
                laneProperties.vehicles.westVehicles[laneProperties.laneIndex].Add(vehicle);
                break;
        }
    }

    private float startVel(LaneProperties.direction dir, int laneIndex) {
        switch (dir) {
            case LaneProperties.direction.East:
                if (laneProperties.vehicles.eastVehicles[laneIndex].Count > 0) {
                    closestVehicle = laneProperties.vehicles.eastVehicles[laneIndex][0];
                    for (int i = 1; i < laneProperties.vehicles.eastVehicles[laneIndex].Count; i++) {
                        if (laneProperties.vehicles.eastVehicles[laneIndex][i].transform.position.x <
                            closestVehicle.transform.position.x) {
                            closestVehicle = laneProperties.vehicles.eastVehicles[laneIndex][i];
                        }
                    }
                } else {
                    return Random.Range(motorwayParameters.speedLimit * 0.8f, motorwayParameters.speedLimit * 1.2f);
                }
                break;
            
            case LaneProperties.direction.West:
                if (laneProperties.vehicles.westVehicles[laneIndex].Count > 0) {
                    closestVehicle = laneProperties.vehicles.westVehicles[laneIndex][0];
                    for (int i = 1; i < laneProperties.vehicles.westVehicles[laneIndex].Count; i++) {
                        if (laneProperties.vehicles.westVehicles[laneIndex][i].transform.position.x <
                            closestVehicle.transform.position.x) {
                            closestVehicle = laneProperties.vehicles.westVehicles[laneIndex][i];
                        }
                    }
                } else {
                    return Random.Range(motorwayParameters.speedLimit * 0.8f, motorwayParameters.speedLimit * 1.2f);
                }
                break;
        }

        return closestVehicle.GetComponent<VehicleProperties>().currentVel;
    }
        
}
