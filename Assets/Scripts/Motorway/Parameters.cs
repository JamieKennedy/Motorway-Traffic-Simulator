using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Parameters : MonoBehaviour {
    public float duration;
    public int lanesNum;
    public float speedLimit;
    public float arrivalRate;
    public float politeness;

    private void OnEnable() {
        SceneManager.sceneLoaded += onSceneLoaded;
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode) {
        // code to be ran when scene is loaded
    }
}
