using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MotorwayControl : MonoBehaviour {

    public GameObject carryOverStats;

    private void Start() {
        carryOverStats = GameObject.Find("CarryOverStats");
    }

    public void EndSim() {
        Time.timeScale = 0;
        carryOverStats.GetComponent<FinalStats>().GetStats();
        SceneManager.LoadScene("EndScreen");
    }
}