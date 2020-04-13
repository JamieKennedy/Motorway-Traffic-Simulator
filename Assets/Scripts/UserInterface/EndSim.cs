using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSim : MonoBehaviour {

    private GameObject motorwayManager;
    private MotorwayControl motorwayControl;

    private void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        motorwayControl = motorwayManager.GetComponent<MotorwayControl>();
    }

    public void EndSimulation() {
        motorwayControl.EndSim();
    }
}