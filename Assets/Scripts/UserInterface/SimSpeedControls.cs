﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimSpeedControls : MonoBehaviour {

    public int simSpeedIndex;
    private int[] speeds;

    public bool currentState;

    public GameObject pauseIcon;
    public GameObject playIcon;
    public GameObject skipButton;
    public TMP_Text speedText;
    
    // Start is called before the first frame update
    void Start() {
        currentState = true;
        simSpeedIndex = 0;
        speeds = new [] {1, 2, 3, 4, 5};
    }

    public void PlayPause() {
        if (currentState) {
            Pause();
            
        } else {
            Play();
        }
    }

    public void ChangeSpeed() {
        // Increment the speed
        if (simSpeedIndex < speeds.Length - 1) {
            simSpeedIndex += 1;
        } else {
            simSpeedIndex = 0;
        }

        // Set the timescale and the text
        Time.timeScale = speeds[simSpeedIndex];
        speedText.text = speeds[simSpeedIndex].ToString() + "X";
    }

    public void Pause() {
        Time.timeScale = 0;
        playIcon.SetActive(true);
        pauseIcon.SetActive(false);
        currentState = false;
    }

    public void Play() {
        Time.timeScale = speeds[skipButton.GetComponent<SimSpeedControls>().simSpeedIndex];
        playIcon.SetActive(false);
        pauseIcon.SetActive(true);
        currentState = true;
    }
}
