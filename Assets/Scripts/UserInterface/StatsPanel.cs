using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour {

    public TMP_Text eastArrivalRateText;
    public TMP_Text eastDepartureRateText;
    public TMP_Text eastAverageSpeedText;
    public TMP_Text eastVehicleNumberText;
        
    public TMP_Text westArrivalRateText;
    public TMP_Text westDepartureRateText;
    public TMP_Text westAverageSpeedText;
    public TMP_Text westVehicleNumberText;
    
    private GameObject motorwayManager;
    private MotorwayStats motorwayStats;
    private Parameters motorwayParameters;

    private StringBuilder sb;
    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        motorwayStats = motorwayManager.GetComponent<MotorwayStats>();
        motorwayParameters = motorwayManager.GetComponent<Parameters>();
    }

    // Update is called once per frame
    void Update() {
        eastArrivalRateText.text = getArrivalRateText(LaneProperties.direction.East);
        eastDepartureRateText.text = getDepartureRateText(LaneProperties.direction.East);
        eastAverageSpeedText.text = getAverageSpeedText(LaneProperties.direction.East);
        eastVehicleNumberText.text = getVehicleNumText(LaneProperties.direction.East);
        
        westArrivalRateText.text = getArrivalRateText(LaneProperties.direction.West);
        westDepartureRateText.text = getDepartureRateText(LaneProperties.direction.West);
        westAverageSpeedText.text = getAverageSpeedText(LaneProperties.direction.West);
        westVehicleNumberText.text = getVehicleNumText(LaneProperties.direction.West);
    }

    private string getArrivalRateText(LaneProperties.direction dir) {
        sb = new StringBuilder("Arrival Rate: ");
        switch (dir) {
            case LaneProperties.direction.East:
                sb.AppendFormat("{0:F0} Vehicles Per Minute", motorwayStats.eastboundArrivalRate * 60f);
                break;
            case LaneProperties.direction.West:
                sb.AppendFormat("{0:F0} Vehicles Per Minute", motorwayStats.westboundArrivalRate * 60f);
                break;
                
        }
        return sb.ToString();
    }

    private string getDepartureRateText(LaneProperties.direction dir) {
        sb = new StringBuilder("Departure Rate: ");
        switch (dir) {
            case LaneProperties.direction.East:
                sb.AppendFormat("{0:F0} Vehicles Per Minute", motorwayStats.eastboundDepartureRate * 60f);
                break;
            case LaneProperties.direction.West:
                sb.AppendFormat("{0:F0} Vehicles Per Minute", motorwayStats.westboundDepartureRate * 60f);
                break;
                
        }
        return sb.ToString();
    }

    private string getAverageSpeedText(LaneProperties.direction dir) {
        sb = new StringBuilder("Average Speed: ");
        switch (dir) {
            case LaneProperties.direction.East:
                sb.AppendFormat("{0:F2} {1}", convertSpeed(motorwayStats.eastboundAverageSpeed), motorwayParameters.speedUnits);
                break;
            case LaneProperties.direction.West:
                sb.AppendFormat("{0:F2} {1}", convertSpeed(motorwayStats.westboundAverageSpeed), motorwayParameters.speedUnits);
                break;
                
        }
        return sb.ToString();
    }

    private string getVehicleNumText(LaneProperties.direction dir) {
        sb = new StringBuilder("Number of Vehicles: ");
        switch (dir) {
            case LaneProperties.direction.East:
                sb.AppendFormat("{0} Vehicles", motorwayStats.eastboundVehicleNum);
                break;
            case LaneProperties.direction.West:
                sb.AppendFormat("{0} Vehicles", motorwayStats.westboundVehicleNum);
                break;
                
        }
        return sb.ToString();
    }

    private float convertSpeed(float speed) {
        switch (motorwayParameters.speedUnits) {
            case "Mph":
                return speed * 2.237f;
            case "Kph":
                return speed * 3.6f;
        }

        return 0f;
    }
}