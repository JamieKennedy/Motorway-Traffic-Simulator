using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    private Lanes lanes;
    private Parameters parameters;
    
    public bool canSpawn = false;

    private List<GameObject> freeLanes;

    private int prob;

    // Update is called once per frame
    void FixedUpdate() {
        if (canSpawn) {
            getFree(lanes.eastLanes);
            if (Random.Range(0, prob) == 0) {
                if (freeLanes.Count > 0) {
                    freeLanes[Random.Range(0, freeLanes.Count)].GetComponent<SpawnVehicle>().Spawn();
                }
            }
            
            getFree(lanes.westLanes);
            if (Random.Range(0, prob) == 0) {
                if (freeLanes.Count > 0) {
                    freeLanes[Random.Range(0, freeLanes.Count)].GetComponent<SpawnVehicle>().Spawn();
                }
            }
        }
    }

    public void setAssignments() {
        lanes = gameObject.GetComponent<Lanes>();
        parameters = gameObject.GetComponent<Parameters>();

        prob = Convert.ToInt32(1 / (parameters.arrivalRate * Time.fixedDeltaTime));
    }

    private void getFree(GameObject[] lanes) {
        freeLanes = new List<GameObject>();
        foreach (var lane in lanes) {
            if (lane.GetComponent<SpawnVehicle>().isFree) {
                freeLanes.Add(lane);
            }
        }
    }
}