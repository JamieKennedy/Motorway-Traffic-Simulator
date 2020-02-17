using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePool : MonoBehaviour {

    [SerializeField] private GameObject lanePrefab;
    
    private Parameters motorwayManagerParameters;
    
    public Queue<GameObject> vehiclePool = new Queue<GameObject>();
    [SerializeField] private GameObject vehiclePrefab;

    public Vector3 poolPos = new Vector3(1000, 1000, 0);

    public void CreateVehiclePool() {
        motorwayManagerParameters = gameObject.GetComponent<Parameters>();
        var vehicleQueueParent = GameObject.Find("VehicleQueue");
        var vehiclesPerLane = Math.Ceiling(lanePrefab.GetComponent<RectTransform>().rect.width / vehiclePrefab.GetComponent<RectTransform>().rect.width);
        for (int i = 0; i < vehiclesPerLane * motorwayManagerParameters.lanesNum * 2; i++) {
            var vehicle = Instantiate(vehiclePrefab, poolPos, Quaternion.identity, vehicleQueueParent.transform);
            vehiclePool.Enqueue(vehicle);
        }
    }
}
