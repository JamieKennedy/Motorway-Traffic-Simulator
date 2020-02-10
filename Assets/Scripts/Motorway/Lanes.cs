using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lanes : MonoBehaviour {
    
    private Parameters motorwayManagerParameters;
    
    private Vehicles motorwayVehicles;
    
    private GameObject motorwayBackground;
    [SerializeField] private GameObject lanePrefab;

    [SerializeField] private Sprite laneEdgeInner;
    [SerializeField] private Sprite laneInner;
    [SerializeField] private Sprite laneEdgeOuter;
    
    private readonly int[] directions = {-1, 1};

    public GameObject[] eastLanes;
    public GameObject[] westLanes;

    public void CreateLanes() {
        motorwayBackground = GameObject.FindWithTag("MotorwayBackground");
        motorwayManagerParameters = gameObject.GetComponent<Parameters>();
        motorwayVehicles = gameObject.GetComponent<Vehicles>();
        
        eastLanes = new GameObject[motorwayManagerParameters.lanesNum];
        westLanes = new GameObject[motorwayManagerParameters.lanesNum];
        
        
        foreach (var direction in directions) {
            for (var i = 0; i < motorwayManagerParameters.lanesNum; i++) {
                var pos = new Vector3(0, (5 + 15 * (i + 1)) * direction, 0);
                var lane = Instantiate(lanePrefab, pos, Quaternion.identity, motorwayBackground.transform);
                lane.GetComponent<LaneProperties>().setAssignments();

                if (i == 0) {
                    switch (direction) {
                        case -1:
                            lane.GetComponent<Image>().sprite = laneEdgeOuter;
                            lane.GetComponent<LaneProperties>().dir = LaneProperties.direction.West;
                            lane.GetComponent<LaneProperties>().laneIndex = i;
                            break;
                        case 1:
                            lane.GetComponent<Image>().sprite = laneEdgeInner;
                            lane.GetComponent<LaneProperties>().dir = LaneProperties.direction.East;
                            lane.GetComponent<LaneProperties>().laneIndex = i;
                            break;
                    }
                } else if (i == motorwayManagerParameters.lanesNum - 1) {
                    switch (direction) {
                        case -1:
                            lane.GetComponent<Image>().sprite = laneEdgeInner;
                            lane.GetComponent<LaneProperties>().dir = LaneProperties.direction.West;
                            lane.GetComponent<LaneProperties>().laneIndex = i;
                            break;
                        case 1:
                            lane.GetComponent<Image>().sprite = laneEdgeOuter;
                            lane.GetComponent<LaneProperties>().dir = LaneProperties.direction.East;
                            lane.GetComponent<LaneProperties>().laneIndex = i;
                            break;
                    }
                    
                } else {
                    switch (direction) {
                        case -1:
                            lane.GetComponent<Image>().sprite = laneInner;
                            lane.GetComponent<LaneProperties>().dir = LaneProperties.direction.West;
                            lane.GetComponent<LaneProperties>().laneIndex = i;
                            break;
                        case 1:
                            lane.GetComponent<Image>().sprite = laneInner;
                            lane.GetComponent<LaneProperties>().dir = LaneProperties.direction.East;
                            lane.GetComponent<LaneProperties>().laneIndex = i;
                            break;
                    }
                }

                switch (direction) {
                    case -1:
                        westLanes[i] = lane;
                        break;
                    case 1:
                        eastLanes[i] = lane;
                        break;
                }
            }
        }
        motorwayVehicles.eastVehicles = new List<GameObject>[motorwayManagerParameters.lanesNum];
        for (var i = 0; i < motorwayVehicles.eastVehicles.Length; i++) {
            motorwayVehicles.eastVehicles[i] = new List<GameObject>();
        }

        motorwayVehicles.westVehicles = new List<GameObject>[motorwayManagerParameters.lanesNum];
        for (var i = 0; i < motorwayVehicles.westVehicles.Length; i++) {
            motorwayVehicles.westVehicles[i] = new List<GameObject>();
        }
    }
}