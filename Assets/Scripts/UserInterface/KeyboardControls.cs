using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour {
    public GameObject pausePanel;
    public GameObject playPauseButton;
    private SimSpeedControls simSpeedControls;

    private bool paused;
    // Start is called before the first frame update
    void Start() {
        simSpeedControls = playPauseButton.GetComponent<SimSpeedControls>();
        paused = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("escape")) {
            if (paused) {
                pausePanel.SetActive(false);
                simSpeedControls.Play();
                paused = false;
            } else {
                pausePanel.SetActive(true);
                simSpeedControls.Pause();
                paused = true;
            }
        }

        if (Input.GetKeyDown("space")) {
            if (simSpeedControls.currentState) {
                simSpeedControls.Pause();
            } else {
                simSpeedControls.Play();
            }
        }
    }

    public void Resume() {
        pausePanel.SetActive(false);
        simSpeedControls.Play();
        paused = false;
    }
}