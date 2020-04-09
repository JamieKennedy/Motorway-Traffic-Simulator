using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimSpeedControls : MonoBehaviour {

    private int simSpeedIndex;
    private int[] speeds;

    public bool currentState;

    public GameObject pauseIcon;
    public GameObject playIcon;
    public TMP_Text speedText;
    
    // Start is called before the first frame update
    void Start() {
        currentState = true;
        simSpeedIndex = 0;
        speeds = new [] {1, 2, 3, 4, 5};
    }

    public void PlayPause() {
        if (currentState) {
            Time.timeScale = 0;
            playIcon.SetActive(true);
            pauseIcon.SetActive(false);
            currentState = false;
        } else {
            Time.timeScale = speeds[simSpeedIndex];
            playIcon.SetActive(false);
            pauseIcon.SetActive(true);
            currentState = true;
        }
    }

    public void ChangeSpeed() {
        if (simSpeedIndex < speeds.Length - 1) {
            simSpeedIndex += 1;
        } else {
            simSpeedIndex = 0;
        }

        Time.timeScale = speeds[simSpeedIndex];
        speedText.text = speeds[simSpeedIndex].ToString() + "X";
    }
}
