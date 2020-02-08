using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MotorwaySetup : MonoBehaviour {
    private Parameters setupParameters;
    private Parameters motorwayManagerParameters;

    private Vehicles motorwayVehicles;

    [SerializeField] private GameObject motorwayManagerPrefab;
    private GameObject motorwayManager;

    private GameObject motorwayBackground;
    [SerializeField] private GameObject lanePrefab;

    [SerializeField] private Sprite laneEdgeInner;
    [SerializeField] private Sprite laneInner;
    [SerializeField] private Sprite laneEdgeOuter;

    public Queue<GameObject> vehiclePool = new Queue<GameObject>();
    [SerializeField] private GameObject vehiclePrefab;
    
    private readonly int[] directions = {-1, 1};
    private readonly float vehicleWidth = 5.12f;
    

    private void OnEnable() {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode) {
        Setup();
    }

    private void Setup() {
        setupParameters = gameObject.GetComponent<Parameters>();
        
        // Instantiate the MotorwayManager Object
        Instantiate(motorwayManagerPrefab, Vector3.zero, Quaternion.identity);

        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        motorwayManagerParameters = motorwayManager.GetComponent<Parameters>();
        motorwayVehicles = motorwayManager.GetComponent<Vehicles>();

        // Uses the setup parameters to assign to the motorway manager 
        motorwayManagerParameters.duration = setupParameters.duration;
        motorwayManagerParameters.lanesNum = setupParameters.lanesNum;
        motorwayManagerParameters.speedLimit = setupParameters.speedLimit;
        motorwayManagerParameters.arrivalRate = setupParameters.arrivalRate;
        motorwayManagerParameters.politeness = setupParameters.politeness;

        // Adds the lane sprites to the scene
        CreateLanes();
        
        // Creates the vehicle Pool
        CreateVehiclePool();
    }

    private void CreateLanes() {
        motorwayBackground = GameObject.FindWithTag("MotorwayBackground");
        
        foreach (var direction in directions) {
            for (var i = 0; i < motorwayManagerParameters.lanesNum; i++) {
                var pos = new Vector3(0, (5 + 15 * (i + 1)) * direction, 0);
                var lane = Instantiate(lanePrefab, pos, Quaternion.identity, motorwayBackground.transform);
                //lane.transform.parent = ;

                if (i == 0) {
                    switch (direction) {
                        case -1:
                            lane.GetComponent<Image>().sprite = laneEdgeOuter;
                            lane.GetComponent<SpawnVehicle>().dir = SpawnVehicle.direction.West;
                            lane.GetComponent<SpawnVehicle>().laneIndex = i;
                            break;
                        case 1:
                            lane.GetComponent<Image>().sprite = laneEdgeInner;
                            lane.GetComponent<SpawnVehicle>().dir = SpawnVehicle.direction.East;
                            lane.GetComponent<SpawnVehicle>().laneIndex = i;
                            break;
                    }
                } else if (i == motorwayManagerParameters.lanesNum - 1) {
                    switch (direction) {
                        case -1:
                            lane.GetComponent<Image>().sprite = laneEdgeInner;
                            lane.GetComponent<SpawnVehicle>().dir = SpawnVehicle.direction.West;
                            lane.GetComponent<SpawnVehicle>().laneIndex = i;
                            break;
                        case 1:
                            lane.GetComponent<Image>().sprite = laneEdgeOuter;
                            lane.GetComponent<SpawnVehicle>().dir = SpawnVehicle.direction.East;
                            lane.GetComponent<SpawnVehicle>().laneIndex = i;
                            break;
                    }
                    
                } else {
                    switch (direction) {
                        case -1:
                            lane.GetComponent<Image>().sprite = laneInner;
                            lane.GetComponent<SpawnVehicle>().dir = SpawnVehicle.direction.West;
                            lane.GetComponent<SpawnVehicle>().laneIndex = i;
                            break;
                        case 1:
                            lane.GetComponent<Image>().sprite = laneInner;
                            lane.GetComponent<SpawnVehicle>().dir = SpawnVehicle.direction.East;
                            lane.GetComponent<SpawnVehicle>().laneIndex = i;
                            break;
                    }
                    
                }
            }
        }
        motorwayVehicles.eastVehicles = new List<GameObject>[motorwayManagerParameters.lanesNum];
        motorwayVehicles.westVehicles = new List<GameObject>[motorwayManagerParameters.lanesNum];
    }

    private void CreateVehiclePool() {
        var vehicleQueueParent = GameObject.Find("VehicleQueue");
        var vehiclesPerLane = Math.Ceiling(lanePrefab.GetComponent<RectTransform>().rect.width / vehicleWidth);
        for (int i = 0; i < vehiclesPerLane * motorwayManagerParameters.lanesNum * 2; i++) {
            var vehicle = Instantiate(vehiclePrefab, new Vector3(1000, 1000, 0), Quaternion.identity, vehicleQueueParent.transform);
            vehiclePool.Enqueue(vehicle);
        }
        
        Debug.Log(vehiclePool.Count);
    }
}