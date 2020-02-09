using System.Collections.Generic;
using UnityEngine;

public class SpawnVehicle : MonoBehaviour {
    
    public enum direction {
        East = -1,
        West = 1
    }

    public direction dir;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private float distance;
    public int laneIndex;

    public bool isFree;
    private GameObject motorwayManager;
    public Vehicles vehicles;

    private Queue<GameObject> vehiclePool;

    private List<GameObject> eastVehicles;
    private List<GameObject> westVehicles;

    // Start is called before the first frame update
    void Start() {
        spawnPos = new Vector3(300f * (int) dir, gameObject.transform.position.y, 0);
    }

    public void setAssignments() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        vehicles = motorwayManager.GetComponent<Vehicles>();
        vehiclePool = motorwayManager.GetComponent<VehiclePool>().vehiclePool;
        isFree = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        switch (dir) {
            case direction.East:
                var tempEast = true;
                if (vehicles.eastVehicles[laneIndex] != null) {
                    foreach (var vehicle in vehicles.eastVehicles[laneIndex]) {
                        if (Vector3.Distance(vehicle.transform.position, spawnPos) < distance) {
                            tempEast = false;
                            break;
                        }
                    }
                }
                
                isFree = tempEast;
                break;
            case direction.West:
                var tempWest = true;
                if (vehicles.westVehicles[laneIndex] != null) {
                    foreach (var vehicle in vehicles.westVehicles[laneIndex]) {
                        if (Vector3.Distance(vehicle.transform.position, spawnPos) < distance) {
                            tempWest = false;
                            break;
                        }
                    }
                }

                isFree = tempWest;
                break;
        }
    }

    public void Spawn() {
        var vehicle = vehiclePool.Dequeue();
        vehicle.transform.position = spawnPos;
        
        switch (dir) {
            case direction.East:
                vehicle.GetComponent<VehicleMovement>().desiredSpeed =
                    vehicle.GetComponent<VehicleMovement>().desiredSpeed;
                vehicle.GetComponent<VehicleMovement>().canMove = true;
                vehicles.eastVehicles[laneIndex].Add(vehicle);
                break;
            case direction.West:
                vehicle.GetComponent<VehicleMovement>().desiredSpeed =
                    vehicle.GetComponent<VehicleMovement>().desiredSpeed * -1;
                vehicle.GetComponent<VehicleMovement>().canMove = true;
                vehicles.westVehicles[laneIndex].Add(vehicle);
                break;
        }
    }
        
}
