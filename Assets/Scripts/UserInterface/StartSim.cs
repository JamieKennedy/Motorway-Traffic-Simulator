﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartSim : MonoBehaviour {
    public TMP_InputField duration;
    public TMP_Dropdown durationUnits;
    private string durationUnitsText;
    public TMP_InputField lanesNum;
    public TMP_InputField speedLimit;
    public TMP_Dropdown speedLimitUnits;
    public TMP_InputField arrivalRate;
    public Slider politeness;
    
    [SerializeField] private GameObject motorwaySetupPrefab;
    private GameObject motorwaySetup;
    public GameObject motorwayManager;
    private Parameters parameters;

    [SerializeField] private GameObject errorBox;
    [SerializeField] private TMP_Text[] messages = new TMP_Text[4];

    public void StartSimulation() {
        InstantiateSetup();

        parameters = motorwaySetup.GetComponent<Parameters>();
        
        // Assigns the parameters values from the start UI to parameters component
        if (!duration.text.Equals("")) {
            durationUnitsText = durationUnits.options[durationUnits.value].text;
            parameters.durationUnits = durationUnitsText;
            switch (durationUnitsText) {
                case "Seconds":
                    try {
                        parameters.duration = float.Parse(duration.text);
                        break;
                    } catch {
                        ErrorHandler(0);
                        return;
                    }
                case "Minutes":
                    try {
                        parameters.duration = float.Parse(duration.text) * 60f;
                        break;
                    } catch {
                        ErrorHandler(0);
                        return;
                    }
                case "Hours":
                    try {
                        parameters.duration = float.Parse(duration.text) * 3600f;
                        break;
                    } catch {
                        ErrorHandler(0);
                        return;
                    }
            }
        } else {
            parameters.duration = 0f;
        }

        try {
            parameters.lanesNum = int.Parse(lanesNum.text);
        } catch {
            ErrorHandler(1);
            return;
        }

        try {
            parameters.speedUnits = speedLimitUnits.options[speedLimitUnits.value].text;
            if (speedLimitUnits.options[speedLimitUnits.value].text.Equals("Mph")) {
                // converts Miles per Hour to meters per second
                parameters.speedLimit = float.Parse(speedLimit.text) / 2.237f;
            } else {
                // converts Kilometers per Hour to meters per second
                parameters.speedLimit = float.Parse(speedLimit.text) / 3.6f;
            }
        } catch {
            ErrorHandler(2);
            return;
        }
        
        try {
            parameters.arrivalRate = float.Parse(arrivalRate.text) / 60f;
        } catch {
            ErrorHandler(3);
            return;
        }

        parameters.politeness = politeness.value;

        
        SceneManager.LoadScene("MainSim");
    }

    private void InstantiateSetup() {
        if(!GameObject.FindWithTag("MotorwaySetup")) {
            motorwaySetup = Instantiate(motorwaySetupPrefab, Vector3.zero, Quaternion.identity);
        } else {
            motorwaySetup = GameObject.FindWithTag("MotorwaySetup");
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
