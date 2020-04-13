using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStats : MonoBehaviour {
    public List<float> eastArrivalRates;
    public List<float> westArrivalRates;

    public List<float> eastDepartureRates;
    public List<float> westDepartureRates;
    
    public List<float> eastVehicleArrivals;
    public List<float> westVehicleArrivals;
    
    public List<float> eastVehicleDepartures;
    public List<float> westVehicleDepartures;

    public List<float> eastAverageSpeeds;
    public List<float> westAverageSpeeds;

    public List<int> eastVehicleNums;
    public List<int> westVehicleNums;

    public float eastAverageSpeed;
    public float westAverageSpeed;

    public float eastAverageVehicleNum;
    public float westAverageVehicleNum;

    public float eastAverageArrivalRate;
    public float westAverageArrivalRate;

    public float eastAverageDepartureRate;
    public float westAverageDepartureRate;

    private GameObject motorwayManager;
    private MotorwayTiming motorwayTiming;
    private MotorwayStats motorwayStats;

    public void GetStats() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        motorwayTiming = motorwayManager.GetComponent<MotorwayTiming>();
        motorwayStats = motorwayManager.GetComponent<MotorwayStats>();

        eastVehicleArrivals = motorwayStats.eastVehicleArrivals;
        westVehicleArrivals = motorwayStats.westVehicleArrivals;

        eastVehicleDepartures = motorwayStats.eastVehicleDepartures;
        westVehicleDepartures = motorwayStats.westVehicleDepartures;

        eastAverageSpeeds = motorwayStats.eastAverageSpeeds;
        westAverageSpeeds = motorwayStats.westAverageSpeeds;

        eastVehicleNums = motorwayStats.eastVehicleNums;
        westVehicleNums = motorwayStats.westVehicleNums;

        eastArrivalRates = motorwayStats.eastArrivalRates;
        westArrivalRates = motorwayStats.westArrivalRates;

        eastDepartureRates = motorwayStats.eastDepartureRates;
        westDepartureRates = motorwayStats.westDepartureRates;

        eastAverageArrivalRate = AverageFloatList(eastArrivalRates);
        westAverageArrivalRate = AverageFloatList(westArrivalRates);

        eastAverageDepartureRate = AverageFloatList(eastDepartureRates);
        westAverageDepartureRate = AverageFloatList(westDepartureRates);
        eastAverageSpeed = AverageFloatList(eastAverageSpeeds);
        westAverageSpeed = AverageFloatList(westAverageSpeeds);

        eastAverageVehicleNum = AverageIntList(eastVehicleNums);
        westAverageVehicleNum = AverageIntList(westVehicleNums);
    }

    private float AverageFloatList(List<float> list) {
        var sum = 0f;

        foreach (var num in list) {
            if (!float.IsNaN(num)) {
                sum += num;
            }
        }

        return sum / list.Count;
    }
    
    private float AverageIntList(List<int> list) {
        var sum = 0f;

        foreach (var num in list) {
            sum += num;
        }

        return sum / list.Count;
    }
}