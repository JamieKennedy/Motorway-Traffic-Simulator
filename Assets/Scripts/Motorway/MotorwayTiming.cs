using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MotorwayTiming : MonoBehaviour {
    public float elapsedTime;
    private float startTime;

    private Parameters motorwayParameters;
    private MotorwayControl motorwayControl;
    // Start is called before the first frame update
    void Start() {
        motorwayControl = gameObject.GetComponent<MotorwayControl>();
        motorwayParameters = gameObject.GetComponent<Parameters>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        elapsedTime = Time.time - startTime;
        hasFinished();
    }

    private void hasFinished() {
        if (motorwayParameters.duration != 0) {
            if (elapsedTime >= motorwayParameters.duration) {
                motorwayControl.EndSim();
            }
        }
    }
    
    
}
