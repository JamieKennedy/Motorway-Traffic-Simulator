using System.Collections.Generic;
using UnityEngine;

public class SpawnVehicle : MonoBehaviour {

    public LaneProperties laneProperties;

    // Start is called before the first frame update
    void Start() {
        laneProperties = gameObject.GetComponent<LaneProperties>();
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
        vehicle.transform.position = laneProperties.spawnPos;
        
        switch (laneProperties.dir) {
            case LaneProperties.direction.East:
                vehicle.GetComponent<VehicleProperties>().direction = LaneProperties.direction.East;
                vehicle.GetComponent<VehicleProperties>().canMove = true;
                vehicle.GetComponent<VehicleProperties>().currentLane = laneProperties.laneIndex;
                    laneProperties.vehicles.eastVehicles[laneProperties.laneIndex].Add(vehicle);
                break;
            case LaneProperties.direction.West:
                vehicle.GetComponent<VehicleProperties>().direction = LaneProperties.direction.West;
                vehicle.GetComponent<VehicleProperties>().canMove = true;
                vehicle.GetComponent<VehicleProperties>().currentLane = laneProperties.laneIndex;
                    laneProperties.vehicles.westVehicles[laneProperties.laneIndex].Add(vehicle);
                break;
        }
    }
        
}
