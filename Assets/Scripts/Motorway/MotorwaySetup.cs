using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MotorwaySetup : MonoBehaviour {
    private Parameters setupParameters;
    private Parameters motorwayManagerParameters;

    [SerializeField] private GameObject motorwayManagerPrefab;
    private GameObject motorwayManager;

    [SerializeField] private GameObject motorwayBackground;
    [SerializeField] private GameObject lanePrefab;

    [SerializeField] private Sprite laneEdgeInner;
    [SerializeField] private Sprite laneInner;
    [SerializeField] private Sprite laneEdgeOuter;
    
    private readonly int[] directions = {-1, 1};

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

        motorwayManagerParameters.duration = setupParameters.duration;
        motorwayManagerParameters.lanesNum = setupParameters.lanesNum;
        motorwayManagerParameters.speedLimit = setupParameters.speedLimit;
        motorwayManagerParameters.arrivalRate = setupParameters.arrivalRate;
        motorwayManagerParameters.politeness = setupParameters.politeness;

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
                            break;
                        case 1:
                            lane.GetComponent<Image>().sprite = laneEdgeInner;
                            break;
                    }
                } else if (i == motorwayManagerParameters.lanesNum - 1) {
                    switch (direction) {
                        case -1:
                            lane.GetComponent<Image>().sprite = laneEdgeInner;
                            break;
                        case 1:
                            lane.GetComponent<Image>().sprite = laneEdgeOuter;
                            break;
                    }
                    
                } else {
                    lane.GetComponent<Image>().sprite = laneInner;
                }
            }

        }
    }
}