using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour {
    
    private VehicleProperties vehicleProperties;
    
    // Start is called before the first frame update
    void Start() {
        vehicleProperties = gameObject.GetComponent<VehicleProperties>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (vehicleProperties.canMove) {
            gameObject.transform.position += new Vector3(vehicleProperties.currentVel * 0.02f * (int) vehicleProperties.direction, 0 , 0);
        }
    }

    public float IDM(GameObject vehicleA, GameObject vehicleB) {
        var s = vehicleB.transform.position.x - vehicleA.transform.position.x;

        return 0;
    }
}