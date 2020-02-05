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
    public TMP_InputField arrivalRate;
    public Slider politeness;
    
    public GameObject motorwayManagerPrefab;
    private GameObject motorwayManager;
    private Parameters parameters;

    public GameObject StartUI;

    [SerializeField] private GameObject errorBox;
    [SerializeField] private TMP_Text[] messages = new TMP_Text[4];

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
                    } catch {
                        ErrorHandler(0);
                        Destroy(motorwayManager);
                        return;
                    }
                case "Minutes":
                    try {
                        parameters.duration = float.Parse(duration.text) * 60f;
                        break;
                    } catch {
                        ErrorHandler(0);
                        Destroy(motorwayManager);
                        return;
                    }
                case "Hours":
                    try {
                        parameters.duration = float.Parse(duration.text) * 600f;
                        break;
                    } catch {
                        ErrorHandler(0);
                        Destroy(motorwayManager);
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
            Destroy(motorwayManager);
            return;
        }
        
        try {
            parameters.speedLimit = float.Parse(speedLimit.text);
        } catch {
            ErrorHandler(2);
            Destroy(motorwayManager);
            return;
        }
        
        try {
            parameters.arrivalRate = float.Parse(arrivalRate.text);
        } catch {
            ErrorHandler(3);
            Destroy(motorwayManager);
            return;
        }

        parameters.politeness = politeness.value;

        // Disables the start UI
        //StartUI.SetActive(false);
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
