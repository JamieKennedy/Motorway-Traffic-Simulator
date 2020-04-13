using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour {
    public TMP_Text timeTitleText;
    public TMP_Text timeText;

    private GameObject motorwayManager;
    private Parameters parameters;
    private MotorwayTiming motorwayTiming;
    
    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        parameters = motorwayManager.GetComponent<Parameters>();
        motorwayTiming = motorwayManager.GetComponent<MotorwayTiming>();

        if (parameters.duration == 0) {
            timeTitleText.text = "Elapsed Time";
        } else {
            timeTitleText.text = "Remaining Time";
        }
    }

    // Update is called once per frame
    void Update() {
        if (parameters.duration == 0) {
            timeText.text = ConvertTime(motorwayTiming.elapsedTime);
        } else {
            timeText.text = ConvertTime(parameters.duration - motorwayTiming.elapsedTime);
        }
    }

    private string ConvertTime(float time) {
        return TimeSpan.FromSeconds(time).ToString(@"hh\:mm\:ss");
    }
}