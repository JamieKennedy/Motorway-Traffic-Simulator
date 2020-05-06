using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighbours : MonoBehaviour {

    private VehicleProperties vehicleProperties;
    private Lanes lanes;
    private GameObject motorwayManager;
    private Parameters parameters;
    private Vehicles vehicles;

    private List<GameObject> allVehicles;
    private List<GameObject> possibleVehicles;
    private GameObject closestVehicle;

    private float delay = 0.2f;
    
    public GameObject[] neighbours = new GameObject[6];
    // 0 North East
    // 1 East
    // 2 South East
    // 3 South West
    // 4 West
    // 5 North West
    
    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        parameters = motorwayManager.GetComponent<Parameters>();
        vehicleProperties = gameObject.GetComponent<VehicleProperties>();
        lanes = motorwayManager.GetComponent<Lanes>();
        vehicles = motorwayManager.GetComponent<Vehicles>();

        StartCoroutine(DoEveryX());
    }

    private IEnumerator DoEveryX() {
        while (true) {
            yield return new WaitForSeconds(delay);
            GetNeighbours();
        }
    }

    private void GetNeighbours() {
        if (vehicleProperties.canMove) {
            switch (vehicleProperties.direction) {
                case LaneProperties.direction.East:
                    if (vehicleProperties.currentLane == 0) {
                        neighbours[0] = getNorthEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[1] = getEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[2] = null; // SE null
                        neighbours[3] = null; // SW null
                        neighbours[4] = getWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[5] = getNorthWest(vehicleProperties.direction, vehicleProperties.currentLane);

                    } else if (vehicleProperties.currentLane == lanes.eastLanes.Length - 1) {
                        neighbours[0] = null; // NE null
                        neighbours[1] = getEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[2] = getSouthEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[3] = getSouthWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[4] = getWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[5] = null; // NW null
                    } else {
                        neighbours[0] = getNorthEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[1] = getEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[2] = getSouthEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[3] = getSouthWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[4] = getWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[5] = getNorthWest(vehicleProperties.direction, vehicleProperties.currentLane);
                    }
                    
                    break;
            
                case LaneProperties.direction.West:
                    if (vehicleProperties.currentLane == 0) {
                        neighbours[0] = null; // NE null
                        neighbours[1] = getEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[2] = getSouthEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[3] = getSouthWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[4] = getWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[5] = null; // NW null
                    } else if (vehicleProperties.currentLane == lanes.westLanes.Length - 1) {
                        neighbours[0] = getNorthEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[1] = getEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[2] = null; // SE null
                        neighbours[3] = null; // SW null
                        neighbours[4] = getWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[5] = getNorthWest(vehicleProperties.direction, vehicleProperties.currentLane);
                    } else {
                        neighbours[0] = getNorthEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[1] = getEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[2] = getSouthEast(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[3] = getSouthWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[4] = getWest(vehicleProperties.direction, vehicleProperties.currentLane);
                        neighbours[5] = getNorthWest(vehicleProperties.direction, vehicleProperties.currentLane);
                    }
                    
                    break;
            }
        } else {
            for (int i = 0; i < 6; i++) {
                neighbours[i] = null;
            }
        }
    }

    private GameObject getNorthEast(LaneProperties.direction dir, int lane) {
        closestVehicle = null;
        possibleVehicles = new List<GameObject>();

        // Gets list of all vehicles
        if (parameters.lanesNum > 1) {
            switch (dir) {
                case LaneProperties.direction.East:
                    allVehicles = vehicles.eastVehicles[lane + 1];
                    break;
                case LaneProperties.direction.West:
                    allVehicles = vehicles.westVehicles[lane - 1];
                    break;
            }
            
            // Finds all vehicles with x >= to itself
            foreach (var vehicle in allVehicles) {
                if (vehicle.transform.position.x >= gameObject.transform.position.x) {
                    possibleVehicles.Add(vehicle);
                }
            }

            // Finds the closest of the vehicles with x >= than itself
            if (possibleVehicles.Count > 0) {
                closestVehicle = possibleVehicles[0];
                for(var i = 1; i < possibleVehicles.Count; i++) {
                    if (possibleVehicles[i].transform.position.x < closestVehicle.transform.position.x) {
                        closestVehicle = possibleVehicles[i];
                    }
                }
            }
        }
        return closestVehicle;
    }
    
    private GameObject getEast(LaneProperties.direction dir, int lane) {
        closestVehicle = null;
        possibleVehicles = new List<GameObject>();
        
        switch (dir) {
            case LaneProperties.direction.East:
                allVehicles = vehicles.eastVehicles[lane];
                break;
            case LaneProperties.direction.West:
                allVehicles = vehicles.westVehicles[lane];
                break;
        }

        foreach (var vehicle in allVehicles) {
            if (vehicle.transform.position.x > gameObject.transform.position.x) {
                possibleVehicles.Add(vehicle);
            }
        }

        if (possibleVehicles.Count > 0) {
            closestVehicle = possibleVehicles[0];
            for (var i = 1; i < possibleVehicles.Count; i++) {
                if (possibleVehicles[i].transform.position.x < closestVehicle.transform.position.x) {
                    closestVehicle = possibleVehicles[i];
                }
            }
        }
        
        return closestVehicle;
    }
    
    private GameObject getSouthEast(LaneProperties.direction dir, int lane) {
        closestVehicle = null;
        possibleVehicles = new List<GameObject>();

        if (parameters.lanesNum > 1) {
            switch (dir) {
                case LaneProperties.direction.East:
                    allVehicles = vehicles.eastVehicles[lane - 1];
                    break;
                case LaneProperties.direction.West:
                    allVehicles = vehicles.westVehicles[lane + 1];
                    break;
            }

            foreach (var vehicle in allVehicles) {
                if (vehicle.transform.position.x >= gameObject.transform.position.x) {
                    possibleVehicles.Add(vehicle);
                }
            }

            if (possibleVehicles.Count > 0) {
                closestVehicle = possibleVehicles[0];
                for (var i = 1; i < possibleVehicles.Count; i++) {
                    if (possibleVehicles[i].transform.position.x < closestVehicle.transform.position.x) {
                        closestVehicle = possibleVehicles[i];
                    }
                }
            } 
        }
        
        return closestVehicle;
    }
    
    private GameObject getSouthWest(LaneProperties.direction dir, int lane) {
        closestVehicle = null;
        possibleVehicles = new List<GameObject>();

        if (parameters.lanesNum > 1) {
            switch (dir) {
                case LaneProperties.direction.East:
                    allVehicles = vehicles.eastVehicles[lane - 1];
                    break;
                case LaneProperties.direction.West:
                    allVehicles = vehicles.westVehicles[lane + 1];
                    break;
            }

            foreach (var vehicle in allVehicles) {
                if (vehicle.transform.position.x < gameObject.transform.position.x) {
                    possibleVehicles.Add(vehicle);
                }
            }

            if (possibleVehicles.Count > 0) {
                closestVehicle = possibleVehicles[0];
                for (var i = 1; i < possibleVehicles.Count; i++) {
                    if (possibleVehicles[i].transform.position.x > closestVehicle.transform.position.x) {
                        closestVehicle = possibleVehicles[i];
                    }
                }
            }
        }
        
        return closestVehicle;
    }
    
    private GameObject getWest(LaneProperties.direction dir, int lane) {
        closestVehicle = null;
        possibleVehicles = new List<GameObject>();
        
        switch (dir) {
            case LaneProperties.direction.East:
                allVehicles = vehicles.eastVehicles[lane];
                break;
            case LaneProperties.direction.West:
                allVehicles = vehicles.westVehicles[lane];
                break;
        }

        foreach (var vehicle in allVehicles) {
            if (vehicle.transform.position.x < gameObject.transform.position.x) {
                possibleVehicles.Add(vehicle);
            }
        }

        if (possibleVehicles.Count > 0) {
            closestVehicle = possibleVehicles[0];
            for (var i = 1; i < possibleVehicles.Count; i++) {
                if (possibleVehicles[i].transform.position.x > closestVehicle.transform.position.x) {
                    closestVehicle = possibleVehicles[i];
                }
            }
        }
        
        return closestVehicle;
    }
    
    private GameObject getNorthWest(LaneProperties.direction dir, int lane) {
        closestVehicle = null;
        possibleVehicles = new List<GameObject>();

        if (parameters.lanesNum > 1) {
            switch (dir) {
                case LaneProperties.direction.East:
                    allVehicles = vehicles.eastVehicles[lane + 1];
                    break;
                case LaneProperties.direction.West:
                    allVehicles = vehicles.westVehicles[lane - 1];
                    break;
            }
        
            foreach (var vehicle in allVehicles) {
                if (vehicle.transform.position.x < gameObject.transform.position.x) {
                    possibleVehicles.Add(vehicle);
                }
            }

            if (possibleVehicles.Count > 0) {
                closestVehicle = possibleVehicles[0];
                for(var i = 1; i < possibleVehicles.Count; i++) {
                    if (possibleVehicles[i].transform.position.x > closestVehicle.transform.position.x) {
                        closestVehicle = possibleVehicles[i];
                    }
                }
            }
        }
        
        return closestVehicle;
    }
}