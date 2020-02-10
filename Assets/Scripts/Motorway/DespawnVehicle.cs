using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnVehicle : MonoBehaviour {
    
    public LaneProperties laneProperties;
    
    // Start is called before the first frame update
    void Start() {
        laneProperties = gameObject.GetComponent<LaneProperties>();
    }

    private void FixedUpdate() {
        switch (laneProperties.dir) {
            case LaneProperties.direction.East:
                if (laneProperties.vehicles.eastVehicles[laneProperties.laneIndex] != null) {
                    foreach (var vehicle in laneProperties.vehicles.eastVehicles[laneProperties.laneIndex]) {
                        if (Vector3.Distance(vehicle.transform.position, laneProperties.deSpawnPos) <
                            laneProperties.distance) {
                            laneProperties.vehiclePool.Enqueue(vehicle);
                            vehicle.GetComponent<VehicleMovement>().canMove = false;
                            vehicle.transform.position =
                                laneProperties.motorwayManager.GetComponent<VehiclePool>().poolPos;
                        }
                    }
                }
                break;
            case LaneProperties.direction.West:
                if (laneProperties.vehicles.westVehicles[laneProperties.laneIndex] != null) {
                    foreach (var vehicle in laneProperties.vehicles.westVehicles[laneProperties.laneIndex]) {
                        if (Vector3.Distance(vehicle.transform.position, laneProperties.deSpawnPos) <
                            laneProperties.distance) {
                            laneProperties.vehiclePool.Enqueue(vehicle);
                            vehicle.GetComponent<VehicleMovement>().canMove = false;
                            vehicle.transform.position =
                                laneProperties.motorwayManager.GetComponent<VehiclePool>().poolPos;
                        }
                    }
                }
                break;
        }
    }
}