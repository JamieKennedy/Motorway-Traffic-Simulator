using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartSim : MonoBehaviour {
    public TMP_InputField duration;
    public TMP_Dropdown durationUnits;
    private string durationUnitsText;
    public TMP_InputField lanesNum;
    public TMP_InputField speedLimit;
    public TMP_InputField arrivalRate;
    public Slider politeness;
    
    public GameObject motorwayManagerPrefab;
    private GameObject motorwayManager;
    private Parameters parameters;

    public GameObject StartUI;

    public void StartSimulation() {
        // Instantiates the MotorwayManager object into the scene
        DeleteAndInstantiate(motorwayManagerPrefab, "MotorwayManager");
        
        // Gets the reference to the now MotorwayManager object in the scene
        // as well as its parameters component
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        parameters = motorwayManager.GetComponent<Parameters>();
        
        // Assigns the parameters values from the start UI to parameters component
        if (!duration.text.Equals("")) {
            durationUnitsText = durationUnits.options[durationUnits.value].text;
            
            switch (durationUnitsText) {
                case "Seconds":
                    try {
                        parameters.duration = float.Parse(duration.text);
                        break;
                    } catch(Exception e) {
                        ErrorHandler(e);
                        Destroy(motorwayManager);
                        return;
                    }
                case "Minutes":
                    try {
                        parameters.duration = float.Parse(duration.text) * 60f;
                        break;
                    } catch(Exception e) {
                        ErrorHandler(e);
                        Destroy(motorwayManager);
                        return;
                    }
                case "Hours":
                    try {
                        parameters.duration = float.Parse(duration.text) * 600f;
                        break;
                    } catch(Exception e) {
                        ErrorHandler(e);
                        Destroy(motorwayManager);
                        return;
                    }
            }
        } else {
            parameters.duration = 0f;
        }

        try {
            parameters.lanesNum = float.Parse(lanesNum.text);
        } catch (Exception e) {
            ErrorHandler(e);
            Destroy(motorwayManager);
            return;
        }
        
        try {
            parameters.speedLimit = float.Parse(speedLimit.text);
        } catch (Exception e) {
            ErrorHandler(e);
            Destroy(motorwayManager);
            return;
        }
        
        try {
            parameters.arrivalRate = float.Parse(arrivalRate.text);
        } catch (Exception e) {
            ErrorHandler(e);
            Destroy(motorwayManager);
            return;
        }

        parameters.politeness = politeness.value;

        // Disables the start UI
        StartUI.SetActive(false);
    }

    private static void DeleteAndInstantiate(GameObject prefab, string ObjectTag) {
        if(GameObject.FindWithTag(ObjectTag) != null) {
            Destroy(GameObject.FindWithTag(ObjectTag));
        } 
        Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }

    private static void ErrorHandler(Exception e) {
        Debug.Log(e);
    }
}
