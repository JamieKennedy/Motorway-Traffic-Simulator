using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorwayStats : MonoBehaviour {

    public int eastboundArrivalCount;
    public int eastboundDepartureCount;
    public int westboundArrivalCount;
    public int westboundDepartureCount;

    public float eastboundArrivalRate;
    public float eastboundDepartureRate;
    public float westboundArrivalRate;
    public float westboundDepartureRate;

    public int eastboundVehicleNum;
    public int westboundVehicleNum;

    public float eastboundAverageSpeed;
    public float westboundAverageSpeed;

    public List<float> eastVehicleArrivals;
    public List<float> westVehicleArrivals;
    
    public List<float> eastVehicleDepartures;
    public List<float> westVehicleDepartures;

    public List<float> eastAverageSpeeds;
    public List<float> westAverageSpeeds;

    public List<int> eastVehicleNums;
    public List<int> westVehicleNums;

    public List<float> eastArrivalRates;
    public List<float> westArrivalRates;

    public List<float> eastDepartureRates;
    public List<float> westDepartureRates;
    

    private float delay = 1f;
    private float time;

    private MotorwayTiming motorwayTiming;

    private Vehicles vehicles;

    // Start is called before the first frame update
    void Start() {
        eastboundArrivalCount = 0;
        eastboundDepartureCount = 0;
        westboundArrivalCount = 0;
        westboundDepartureCount = 0;
        
        eastboundArrivalRate = 0f;
        eastboundDepartureRate = 0f;
        westboundArrivalRate = 0f;
        westboundDepartureRate = 0f;

        eastboundVehicleNum = 0;
        westboundVehicleNum = 0;

        eastboundAverageSpeed = 0f;
        westboundAverageSpeed = 0f;

        motorwayTiming = gameObject.GetComponent<MotorwayTiming>();
        vehicles = gameObject.GetComponent<Vehicles>();

        StartCoroutine(DoEverySecond());
    }

    // Update is called once per frame
    void Update() {
        eastboundArrivalRate = eastboundArrivalCount / motorwayTiming.elapsedTime;
        eastboundDepartureRate = eastboundDepartureCount / motorwayTiming.elapsedTime;

        westboundArrivalRate = westboundArrivalCount / motorwayTiming.elapsedTime;
        westboundDepartureRate = westboundDepartureCount / motorwayTiming.elapsedTime;

        eastboundVehicleNum = calcLength(vehicles.eastVehicles);
        westboundVehicleNum = calcLength(vehicles.westVehicles);

        eastboundAverageSpeed = calcAverageSpeed(vehicles.eastVehicles);
        westboundAverageSpeed = calcAverageSpeed(vehicles.westVehicles);
    }

    private float calcAverageSpeed(List<GameObject>[] lanes) {
        var totalSpeed = 0f;
        var count = 0;

        foreach (var lane in lanes) {
            foreach (var vehicle in lane) {
                count += 1;
                totalSpeed += vehicle.GetComponent<VehicleProperties>().currentVel;
            }
        }

        return totalSpeed / count;
    }

    private int calcLength(List<GameObject>[] lanes) {
        var count = 0;

        foreach (var lane in lanes) {
            foreach (var vehicle in lane) {
                count += 1;
            }
        }

        return count;
    }

    IEnumerator DoEverySecond() {
        while (true) {
            yield return new WaitForSeconds(delay);
            AddToLists();
        }
    }

    private void AddToLists() {
        eastAverageSpeeds.Add(eastboundAverageSpeed);
        westAverageSpeeds.Add(westboundAverageSpeed);
        
        eastVehicleNums.Add(eastboundVehicleNum);
        westVehicleNums.Add(westboundVehicleNum);
        
        eastArrivalRates.Add(eastboundArrivalRate);
        westArrivalRates.Add(westboundArrivalRate);

        eastDepartureRates.Add(eastboundDepartureRate);
        westDepartureRates.Add(westboundDepartureRate);
        
        
    }
}