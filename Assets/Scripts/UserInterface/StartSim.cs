using System;
using System.Collections;
using System.Collections.Generic;
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
    private Parameters parameters;

    [SerializeField] private GameObject errorBox;
    [SerializeField] private TMP_Text[] messages = new TMP_Text[4];

    public void StartSimulation() {
        DeleteAndInstantiate(motorwaySetupPrefab, "MotorwaySetup");

        motorwaySetup = GameObject.FindWithTag("MotorwaySetup");
        
        parameters = motorwaySetup.GetComponent<Parameters>();
        
        // Assigns the parameters values from the start UI to parameters component
        if (!duration.text.Equals("")) {
            durationUnitsText = durationUnits.options[durationUnits.value].text;
            
            switch (durationUnitsText) {
                case "Seconds":
                    try {
                        parameters.duration = float.Parse(duration.text);
                        break;
                    } catch {
                        ErrorHandler(0);
                        Destroy(motorwaySetup);
                        return;
                    }
                case "Minutes":
                    try {
                        parameters.duration = float.Parse(duration.text) * 60f;
                        break;
                    } catch {
                        ErrorHandler(0);
                        Destroy(motorwaySetup);
                        return;
                    }
                case "Hours":
                    try {
                        parameters.duration = float.Parse(duration.text) * 3600f;
                        break;
                    } catch {
                        ErrorHandler(0);
                        Destroy(motorwaySetup);
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
            Destroy(motorwaySetup);
            return;
        }

        try {
            if (speedLimitUnits.options[speedLimitUnits.value].text.Equals("Mph")) {
                // converts Miles per Hour to meters per second
                parameters.speedLimit = float.Parse(speedLimit.text) / 2.237f;
            } else {
                // converts Kilometers per Hour to meters per second
                parameters.speedLimit = float.Parse(speedLimit.text) / 3.6f;
            }
        } catch {
            ErrorHandler(2);
            Destroy(motorwaySetup);
            return;
        }
        
        try {
            parameters.arrivalRate = float.Parse(arrivalRate.text) / 60f;
        } catch {
            ErrorHandler(3);
            Destroy(motorwaySetup);
            return;
        }

        parameters.politeness = politeness.value;

        
        SceneManager.LoadScene("MainSim");
    }

    private static void DeleteAndInstantiate(GameObject prefab, string ObjectTag) {
        if(GameObject.FindWithTag(ObjectTag) != null) {
            Destroy(GameObject.FindWithTag(ObjectTag));
        } 
        Instantiate(prefab, Vector3.zero, Quaternion.identity);
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
