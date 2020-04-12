using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class CurrentlySelected : MonoBehaviour {
    public GameObject currentVehicle;

    public TMP_Text speedText;
    public TMP_Text desiredSpeedText;
    public TMP_Text politenessText;

    private VehicleProperties vehicleProperties;
    private GameObject motorwayManager;
    private Parameters parameters;

    private StringBuilder sb;

    
    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        parameters = motorwayManager.GetComponent<Parameters>();
        currentVehicle = null;
    }

    // Update is called once per frame
    void Update() {
        if (currentVehicle) {
            vehicleProperties = currentVehicle.GetComponent<VehicleProperties>();
            show(true);
            speedText.text = getSpeedString();
            politenessText.text = getPolitenessString();
            desiredSpeedText.text = getDesiredSpeedString();
            
            if (!vehicleProperties.canMove) {
                currentVehicle = null;
            }
        } else {
            show(false);
        }
        
    }

    private string getSpeedString() {
        sb = new StringBuilder();
        sb.Append("Speed: ");
        sb.AppendFormat("{0:F2}", convertSpeed(vehicleProperties.currentVel));
        sb.AppendFormat(" {0}", parameters.speedUnits);

        return sb.ToString();
    }

    private string getDesiredSpeedString() {
        sb = new StringBuilder();
        sb.Append("Desired Speed: ");
        sb.AppendFormat("{0:F2}", convertSpeed(vehicleProperties.desiredSpeed));
        sb.AppendFormat(" {0}", parameters.speedUnits);

        return sb.ToString();
    }

    private string getPolitenessString() {
        sb = new StringBuilder();
        sb.Append("Politeness: ");
        sb.AppendFormat("{0:F1}", vehicleProperties.politeness * 10f);

        return sb.ToString();
    }

    private float convertSpeed(float speed) {
        switch (parameters.speedUnits) {
            case "Mph":
                return speed * 2.237f;
                
            case "Kph":
                return speed * 3.6f;
        }

        return 0;
    }

    private void show(bool show) {
        if (show) {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        } else {
            gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}