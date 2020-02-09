using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePool : MonoBehaviour {
    private const float vehicleWidth = 5.12f;

    [SerializeField] private GameObject lanePrefab;
    
    private Parameters motorwayManagerParameters;
    
    public Queue<GameObject> vehiclePool = new Queue<GameObject>();
    [SerializeField] private GameObject vehiclePrefab;
    
    

    public void CreateVehiclePool() {
        motorwayManagerParameters = gameObject.GetComponent<Parameters>();
        var vehicleQueueParent = GameObject.Find("VehicleQueue");
        var vehiclesPerLane = Math.Ceiling(lanePrefab.GetComponent<RectTransform>().rect.width / vehicleWidth);
        for (int i = 0; i < vehiclesPerLane * motorwayManagerParameters.lanesNum * 2; i++) {
            var vehicle = Instantiate(vehiclePrefab, new Vector3(1000, 1000, 0), Quaternion.identity, vehicleQueueParent.transform);
            vehiclePool.Enqueue(vehicle);
        }
    }
}
