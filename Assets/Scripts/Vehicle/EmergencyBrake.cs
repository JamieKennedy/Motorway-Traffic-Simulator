using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmergencyBrake : MonoBehaviour {
    public GameObject infoPanel;
    public TMP_Text buttonText;
    private CurrentlySelected currentlySelected;
    private VehicleControls vehicleControls;

    private GameObject currentVehicle;

    // Start is called before the first frame update
    void Start() {
        currentlySelected = infoPanel.GetComponent<CurrentlySelected>();
    }

    private void Update() {
        if (currentlySelected.currentVehicle) {
            vehicleControls = currentlySelected.currentVehicle.GetComponent<VehicleControls>();
            if (vehicleControls.emergencyBraked) {
                gameObject.GetComponent<Image>().color = new Color32(0, 212, 0, 255);
                buttonText.text = "Resume";
            } else {
                gameObject.GetComponent<Image>().color = new Color32(253, 46, 46, 255);
                buttonText.text = "Emergency Brake";
            }
        } else {
            gameObject.GetComponent<Image>().color = new Color32(253, 46, 46, 255);
            buttonText.text = "Emergency Brake";
        }
    }

    public void BrakeResume() {
        if (vehicleControls.emergencyBraked) {
            vehicleControls.emergencyBraked = false;
            vehicleControls.Resume();
        } else {
            vehicleControls.emergencyBraked = true;
        }
    }
}