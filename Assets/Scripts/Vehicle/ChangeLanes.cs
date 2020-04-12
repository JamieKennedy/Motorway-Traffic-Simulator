using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLanes : MonoBehaviour {
    private VehicleProperties vehicleProperties;
    private Neighbours neighbours;
    private GameObject motorwayManager;
    private LaneProperties laneProperties;
    private VehicleMovement vehicleMovement;
    private Vehicles vehicles;
    private Lanes lanes;

    private GameObject infrontVehicle;
    private GameObject infrontVehicleNewLane;
    private GameObject behindVehicle;
    private GameObject behindVehicleNewLane;

    private float accelNewLaneChanged;
    private float accelBehindCurr;
    private float accelBehindChanged;
    private float accelBehindNewLaneCurr;
    private float accelBehindNewLaneChanged;

    private int newLaneIndex;
    private float newYPos;
    private int laneChangeDir;
    public bool changingLane = false;
    public bool canChange;

    public float laneChangeCoolDownTimer;
    
    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        vehicleProperties = gameObject.GetComponent<VehicleProperties>();
        neighbours = gameObject.GetComponent<Neighbours>();
        laneProperties = motorwayManager.GetComponent<LaneProperties>();
        vehicleMovement = gameObject.GetComponent<VehicleMovement>();
        vehicles = motorwayManager.GetComponent<Vehicles>();
        lanes = motorwayManager.GetComponent<Lanes>();
    }

    private void Update() {
        laneChangeCoolDown();

        if (changingLane) {
            changeYPos(laneChangeDir);
        }
    }

    void FixedUpdate() {
        if (vehicleProperties.canMove && laneChangeCoolDownTimer <= 0 && !vehicleProperties.hasStopped) {
            laneChangeCoolDownTimer = vehicleProperties.laneChangeCoolDown;
            switch (vehicleProperties.direction) {
                case LaneProperties.direction.East:
                    if (vehicleProperties.currentLane == lanes.eastLanes.Length - 1) {
                        vehicleProperties.mobil = calc_MOBIL(-1);
                        if (vehicleProperties.mobil >= vehicleProperties.changeThreshold) {
                            newLaneIndex = vehicleProperties.currentLane - 1;
                            laneChangeDir = -1;
                            changeLane(newLaneIndex, vehicleProperties.direction); 
                        } 
                    } else if (vehicleProperties.currentLane == 0) {
                        vehicleProperties.mobil = calc_MOBIL(1);
                        if (vehicleProperties.mobil >= vehicleProperties.changeThreshold) {
                            newLaneIndex = vehicleProperties.currentLane + 1;
                            laneChangeDir = 1;
                            changeLane(newLaneIndex, vehicleProperties.direction);
                        }
                    } else {
                        vehicleProperties.mobil = calc_MOBIL(-1);
                        if (vehicleProperties.mobil >= vehicleProperties.changeThreshold) {
                            newLaneIndex = vehicleProperties.currentLane - 1;
                            laneChangeDir = -1;
                            changeLane(newLaneIndex, vehicleProperties.direction);
                        } else {
                            vehicleProperties.mobil = calc_MOBIL(1);
                            if (vehicleProperties.mobil >= vehicleProperties.changeThreshold) {
                                newLaneIndex = vehicleProperties.currentLane + 1;
                                laneChangeDir = 1;
                                changeLane(newLaneIndex, vehicleProperties.direction);
                            }
                        }
                    }
                    break;
                case LaneProperties.direction.West:
                    if (vehicleProperties.currentLane == lanes.westLanes.Length - 1) {
                        vehicleProperties.mobil = calc_MOBIL(1);
                        if (vehicleProperties.mobil >= vehicleProperties.changeThreshold) {
                            newLaneIndex = vehicleProperties.currentLane - 1;
                            laneChangeDir = 1;
                            changeLane(newLaneIndex, vehicleProperties.direction); 
                        } 
                    } else if (vehicleProperties.currentLane == 0) {
                        vehicleProperties.mobil = calc_MOBIL(-1);
                        if (vehicleProperties.mobil >= vehicleProperties.changeThreshold) {
                            newLaneIndex = vehicleProperties.currentLane + 1;
                            laneChangeDir = -1;
                            changeLane(newLaneIndex, vehicleProperties.direction);
                        }
                    } else {
                        vehicleProperties.mobil = calc_MOBIL(-1);
                        if (vehicleProperties.mobil >= vehicleProperties.changeThreshold) {
                            newLaneIndex = vehicleProperties.currentLane + 1;
                            laneChangeDir = -1;
                            changeLane(newLaneIndex, vehicleProperties.direction);
                        } else {
                            vehicleProperties.mobil = calc_MOBIL(1);
                            if (vehicleProperties.mobil >= vehicleProperties.changeThreshold) {
                                newLaneIndex = vehicleProperties.currentLane - 1;
                                laneChangeDir = 1;
                                changeLane(newLaneIndex, vehicleProperties.direction);
                            }
                        }
                    }
                    break;
            }
        }
    }

    private float calc_MOBIL(int newLane) {
        var currAccel = vehicleProperties.currentAccel;
        var facingDir = vehicleProperties.direction;
        getFrontAndRear(facingDir);
        getFrontAndRearNewLane(facingDir, newLane);

        if (infrontVehicleNewLane != null) {
            if (CanChange(gameObject, infrontVehicleNewLane)) {
                accelNewLaneChanged = vehicleMovement.IDM(gameObject, infrontVehicleNewLane);
                if (behindVehicleNewLane != null) {
                    if (CanChange(behindVehicleNewLane, gameObject)) {
                        accelBehindNewLaneCurr = vehicleMovement.IDM(behindVehicleNewLane, infrontVehicleNewLane);
                        accelBehindNewLaneChanged = vehicleMovement.IDM(behindVehicleNewLane, gameObject);
                    } else {
                        return 0f;
                    }
                } else {
                    accelBehindNewLaneCurr = 0f;
                    accelBehindNewLaneChanged = 0f;
                }
            } else {
                return 0f;
            }
        } else {
            accelNewLaneChanged = vehicleProperties.maxAccel;
            if (behindVehicleNewLane != null) {
                if (CanChange(behindVehicleNewLane, gameObject)) {
                    accelBehindNewLaneCurr = behindVehicleNewLane.GetComponent<VehicleProperties>().maxAccel;
                } else {
                    return 0f;
                }
            } else {
                accelBehindNewLaneCurr = 0f;
            }
            
        }

        if (behindVehicle != null) {
            accelBehindCurr = vehicleMovement.IDM(behindVehicle, gameObject);
            if (infrontVehicle != null) {
                accelBehindChanged = vehicleMovement.IDM(behindVehicle, infrontVehicle);
            } else {
                accelBehindChanged = behindVehicle.GetComponent<VehicleProperties>().maxAccel;
            }
        } else {
            accelBehindCurr = 0f;
            accelBehindChanged = 0f;
        }
        
        var deltaVehicle = accelNewLaneChanged - currAccel;
        var deltaBehind = accelBehindChanged - accelBehindCurr;
        var deltaBehindNewLane = accelBehindNewLaneChanged - accelBehindNewLaneCurr;

        return deltaVehicle + vehicleProperties.politeness * (deltaBehind + deltaBehindNewLane);
    }

    private void getFrontAndRear(LaneProperties.direction dir) {
        switch (dir) {
            case LaneProperties.direction.East:
                infrontVehicle = neighbours.neighbours[1];
                behindVehicle = neighbours.neighbours[4];
                
                break;
            case LaneProperties.direction.West:
                infrontVehicle = neighbours.neighbours[4];
                behindVehicle = neighbours.neighbours[1];
                break;
        }
    }

    private void getFrontAndRearNewLane(LaneProperties.direction dir, int newLane) {
        switch (dir) {
            case LaneProperties.direction.East:
                switch (newLane) {
                    case 1:
                        infrontVehicleNewLane = neighbours.neighbours[0];
                        behindVehicleNewLane = neighbours.neighbours[5];
                        break;
                    case -1:
                        infrontVehicleNewLane = neighbours.neighbours[2];
                        behindVehicleNewLane = neighbours.neighbours[3];
                        break;
                }
                break;
            case LaneProperties.direction.West:
                switch (newLane) {
                    case 1:
                        infrontVehicleNewLane = neighbours.neighbours[5];
                        behindVehicleNewLane = neighbours.neighbours[0];
                        break;
                    case -1:
                        infrontVehicleNewLane = neighbours.neighbours[3];
                        behindVehicleNewLane = neighbours.neighbours[2];
                        break;
                }
                break;
        }
    }

    private void changeLane(int laneIndex, LaneProperties.direction dir) {
        switch (dir) {
            case LaneProperties.direction.East:
                vehicles.eastVehicles[vehicleProperties.currentLane].Remove(gameObject);
                vehicles.eastVehicles[laneIndex].Add(gameObject);
                vehicleProperties.currentLane = laneIndex;
                newYPos = lanes.eastLanes[laneIndex].GetComponent<LaneProperties>().yPos;
                changingLane = true;
                break; 
            case LaneProperties.direction.West:
                vehicles.westVehicles[vehicleProperties.currentLane].Remove(gameObject);
                vehicles.westVehicles[laneIndex].Add(gameObject);
                vehicleProperties.currentLane = laneIndex;
                newYPos = lanes.westLanes[laneIndex].GetComponent<LaneProperties>().yPos;
                changingLane = true;
                break;
        }  
    }

    // Vehicle A follows Vehicle B
    private bool CanChange(GameObject vehicleA, GameObject vehicleB) {
        var vehicleAProperties = vehicleA.GetComponent<VehicleProperties>();
        var vehicleBProperties = vehicleB.GetComponent<VehicleProperties>();
        var vehicleAFront = 0f;
        var vehicleBBack = 0f;

        switch (vehicleAProperties.direction) {
            case LaneProperties.direction.East:
                vehicleAFront = vehicleA.transform.position.x + (vehicleAProperties.vehicleWidth / 2f);
                break;
            case LaneProperties.direction.West:
                vehicleAFront = vehicleA.transform.position.x - (vehicleAProperties.vehicleWidth / 2f);
                break;
        }
        
        switch (vehicleBProperties.direction) {
            case LaneProperties.direction.East:
                vehicleBBack = vehicleB.transform.position.x - (vehicleBProperties.vehicleWidth / 2f);
                break;
            case LaneProperties.direction.West:
                vehicleBBack = vehicleB.transform.position.x + (vehicleBProperties.vehicleWidth / 2f);
                break;
        }


        return Math.Abs(vehicleBBack - vehicleAFront) > vehicleAProperties.jamDistance;
    }

    private void changeYPos(int direction) {
        switch (direction) {
            case 1:
                if (gameObject.transform.position.y >= newYPos) {
                    changingLane = false;
                } else {
                    gameObject.transform.position += new Vector3(0, 0.5f, 0);
                }
                break;
            case -1:
                if (gameObject.transform.position.y <= newYPos) {
                    changingLane = false;
                } else {
                    gameObject.transform.position -= new Vector3(0, 0.5f,0);
                }
                break;
        }
    }

    private void laneChangeCoolDown() {
        if (laneChangeCoolDownTimer > 0) {
            laneChangeCoolDownTimer -= Time.deltaTime;
        }
    }
}