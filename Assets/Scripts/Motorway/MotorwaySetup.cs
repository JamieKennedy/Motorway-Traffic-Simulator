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

    [SerializeField] private GameObject motorwayManagerPrefab;
    private GameObject motorwayManager;
    
    
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

        // Uses the setup parameters to assign to the motorway manager 
        motorwayManagerParameters.duration = setupParameters.duration;
        motorwayManagerParameters.lanesNum = setupParameters.lanesNum;
        motorwayManagerParameters.speedLimit = setupParameters.speedLimit;
        motorwayManagerParameters.arrivalRate = setupParameters.arrivalRate;
        motorwayManagerParameters.politeness = setupParameters.politeness;
        motorwayManagerParameters.durationUnits = setupParameters.durationUnits;
        motorwayManagerParameters.speedUnits = setupParameters.speedUnits;

        // sets assignments for vehicles class
        //motorwayManager.GetComponent<Vehicles>().setAssignments();
        
        // Adds the lane sprites to the scene
        motorwayManager.GetComponent<Lanes>().CreateLanes();
        
        // Creates the vehicle Pool
        motorwayManager.GetComponent<VehiclePool>().CreateVehiclePool();
        
        // Assigns spawner vars and allows spawning
        motorwayManager.GetComponent<Spawner>().setAssignments();
        motorwayManager.GetComponent<Spawner>().canSpawn = true;
    }

    

    
}