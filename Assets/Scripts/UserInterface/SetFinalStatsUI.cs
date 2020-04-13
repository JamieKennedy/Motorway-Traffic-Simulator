using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class SetFinalStatsUI : MonoBehaviour {

    public TMP_Text eastArrivalsText;
    public TMP_Text eastDeparturesText;
    public TMP_Text eastSpeedText;
    public TMP_Text eastVehicleNumText;

    public TMP_Text westArrivalsText;
    public TMP_Text westDeparturesText;
    public TMP_Text westSpeedText;
    public TMP_Text westVehicleNumText;

    private StringBuilder sb;

    private GameObject motorwaySetup;
    private Parameters parameters;

    private GameObject carryOverStats;
    private FinalStats finalStats;
    

    // Start is called before the first frame update
    void Start() {
        motorwaySetup = GameObject.FindWithTag("MotorwaySetup");
        parameters = motorwaySetup.GetComponent<Parameters>();

        carryOverStats = GameObject.Find("CarryOverStats");
        finalStats = carryOverStats.GetComponent<FinalStats>();

        eastArrivalsText.text = GetArrivalText(finalStats.eastAverageArrivalRate);
        eastDeparturesText.text = GetDepartureText(finalStats.eastAverageDepartureRate);
        eastSpeedText.text = GetSpeedText(finalStats.eastAverageSpeed);
        eastVehicleNumText.text = GetVehicleNumText(finalStats.eastAverageVehicleNum);
        
        westArrivalsText.text = GetArrivalText(finalStats.westAverageArrivalRate);
        westDeparturesText.text = GetDepartureText(finalStats.westAverageDepartureRate);
        westSpeedText.text = GetSpeedText(finalStats.westAverageSpeed);
        westVehicleNumText.text = GetVehicleNumText(finalStats.westAverageVehicleNum);
    }
    
    private string GetArrivalText(float arrivalRate) {
        sb = new StringBuilder("Average Arrival Rate: ");
        sb.AppendFormat("{0:F1} Vehicles / Minute", arrivalRate * 60f);

        return sb.ToString();
    }

    private string GetDepartureText(float departureRate) {
        sb = new StringBuilder("Average Departure Rate: ");
        sb.AppendFormat("{0:F1} Vehicles / Minute", departureRate * 60f);

        return sb.ToString();
    }

    private string GetSpeedText(float speed) {
        sb = new StringBuilder("Average Speed: ");

        switch (parameters.speedUnits) {
            case "Mph":
                sb.AppendFormat("{0:F2} Mph", speed * 2.237f);
                break;
            case "Kph":
                sb.AppendFormat("{0:F2} Kph", speed * 3.6f);
                break;
        }
        
        return sb.ToString();
    }

    private string GetVehicleNumText(float num) {
        sb = new StringBuilder("Average Number Of Vehicles: ");
        sb.AppendFormat("{0:F1} Vehicles", num);

        return sb.ToString();
    }
}