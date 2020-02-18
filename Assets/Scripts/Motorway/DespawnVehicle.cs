using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DespawnVehicle : MonoBehaviour {
    
    public LaneProperties laneProperties;
    
    void Start() {
        laneProperties = gameObject.GetComponent<LaneProperties>();
    }

    private void FixedUpdate() {
        switch (laneProperties.dir) {
            case LaneProperties.direction.East:
                if (laneProperties.vehicles.eastVehicles[laneProperties.laneIndex] != null) {
                    foreach (var vehicle in laneProperties.vehicles.eastVehicles[laneProperties.laneIndex].ToList()) {
                        if (Vector3.Distance(vehicle.transform.position, laneProperties.deSpawnPos) <
                            laneProperties.distance) {
                            laneProperties.vehiclePool.Enqueue(vehicle);
                            vehicle.GetComponent<VehicleProperties>().canMove = false;
                            vehicle.transform.position =
                                laneProperties.motorwayManager.GetComponent<VehiclePool>().poolPos;
                            laneProperties.vehicles.eastVehicles[laneProperties.laneIndex].Remove(vehicle);
                        }
                    }
                }
                break;
            case LaneProperties.direction.West:
                if (laneProperties.vehicles.westVehicles[laneProperties.laneIndex] != null) {
                    foreach (var vehicle in laneProperties.vehicles.westVehicles[laneProperties.laneIndex].ToList()) {
                        if (Vector3.Distance(vehicle.transform.position, laneProperties.deSpawnPos) <
                            laneProperties.distance) {
                            laneProperties.vehiclePool.Enqueue(vehicle);
                            vehicle.GetComponent<VehicleProperties>().canMove = false;
                            vehicle.transform.position =
                                laneProperties.motorwayManager.GetComponent<VehiclePool>().poolPos;
                            laneProperties.vehicles.westVehicles[laneProperties.laneIndex].Remove(vehicle);
                        }
                    }
                }
                break;
        }
    }
}