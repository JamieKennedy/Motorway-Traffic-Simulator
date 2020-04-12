using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveParameters : MonoBehaviour {
    public TMP_InputField speedLimitInputText;
    public TMP_InputField arrivalRateInputText;
    public Slider politenessSlider;

    private float newSpeedLimit;
    private float newArrivalRate;
    private float newPoliteness;

    private GameObject motorwayManager;
    private Parameters parameters;
    private Vehicles vehicles;
    private Spawner spawner;

    public GameObject errorBox;
    public TMP_Text[] messages = new TMP_Text[2];
    
    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        parameters = motorwayManager.GetComponent<Parameters>();
        vehicles = motorwayManager.GetComponent<Vehicles>();
        spawner = motorwayManager.GetComponent<Spawner>();
    }

    public void Save() {
        if (GetSpeedLimit()) {
            parameters.speedLimit = newSpeedLimit;
            UpdateDesiredSpeed();
        }

        if (GetArrivalRate()) {
            parameters.arrivalRate = newArrivalRate;
            spawner.SetProb();
        }
        
        newPoliteness = politenessSlider.value;
        parameters.politeness = newPoliteness;
        UpdatePoliteness();
    }

    private bool GetSpeedLimit() {
        switch (parameters.speedUnits) {
            case "Mph":
                try {
                    newSpeedLimit = float.Parse(speedLimitInputText.text) / 2.237f;
                    return true;
                } catch{
                    ErrorHandler(0);
                    return false;
                }
            case "Kph":
                try {
                    newSpeedLimit = float.Parse(speedLimitInputText.text) / 3.6f;
                    return true;
                } catch {
                    ErrorHandler(0);
                    return false;
                }
        }

        return false;
    }
    
    private bool GetArrivalRate() {
        try {
            newArrivalRate = float.Parse(arrivalRateInputText.text) / 60f;
            return true;
        } catch {
            ErrorHandler(1);
            return false;
        }
    }

    private void UpdateDesiredSpeed() {
        foreach (var lane in vehicles.eastVehicles) {
            foreach (var vehicle in lane) {
                vehicle.GetComponent<VehicleProperties>().SetDesiredSpeed();
            }
        }
        
        foreach (var lane in vehicles.westVehicles) {
            foreach (var vehicle in lane) {
                vehicle.GetComponent<VehicleProperties>().SetDesiredSpeed();
            }
        }
    }
    
    private void UpdatePoliteness() {
        foreach (var lane in vehicles.eastVehicles) {
            foreach (var vehicle in lane) {
                vehicle.GetComponent<VehicleProperties>().SetPoliteness();
            }
        }
        
        foreach (var lane in vehicles.westVehicles) {
            foreach (var vehicle in lane) {
                vehicle.GetComponent<VehicleProperties>().SetPoliteness();
            }
        }
    }
    
    private void ErrorHandler(int errorIndex) {
        // disables all error messages
        foreach (var message in messages) {
            message.gameObject.SetActive(false);
        }
        // enables error message box and the correct error message
        errorBox.SetActive(true);
        messages[errorIndex].gameObject.SetActive(true);
    }
}