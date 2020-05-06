using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BreakDown : MonoBehaviour {
    public GameObject infoPanel;
    public TMP_Text buttonText;
    private CurrentlySelected currentlySelected;
    private VehicleProperties vehicleProperties;
    private VehicleControls vehicleControls;

    private GameObject currentVehicle;

    // Start is called before the first frame update
    void Start() {
        currentlySelected = infoPanel.GetComponent<CurrentlySelected>();
    }

    private void Update() {
        if (currentlySelected.currentVehicle) {
            vehicleControls = currentlySelected.currentVehicle.GetComponent<VehicleControls>();
            if (vehicleControls.breakingDown) {
                gameObject.GetComponent<Image>().color = new Color32(0, 212, 0, 255);
                buttonText.text = "Resume";
            } else {
                gameObject.GetComponent<Image>().color = new Color32(69, 69, 255, 255);
                buttonText.text = "Break Down";
            }
        } else {
            gameObject.GetComponent<Image>().color = new Color32(69, 69, 255, 255);
            buttonText.text = "Break Down";
        }
    }

    public void BreakDownResume() {
        if (vehicleControls.breakingDown) {
            vehicleControls.breakingDown = false;
            vehicleControls.Resume();
        } else {
            vehicleControls.breakingDown = true;
        }
    }

    
}