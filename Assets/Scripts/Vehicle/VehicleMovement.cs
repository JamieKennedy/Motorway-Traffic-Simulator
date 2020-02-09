using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour {
    [SerializeField] public float desiredSpeed;

    private GameObject motorwayManager;
    private Parameters motorwayParameters;

    public bool canMove = false;
    
    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        motorwayParameters = motorwayManager.GetComponent<Parameters>();
        desiredSpeed = Random.Range(motorwayParameters.speedLimit - 10, motorwayParameters.speedLimit + 10);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (canMove) {
            gameObject.transform.position += new Vector3(desiredSpeed * 0.02f, 0 , 0);
        }
    }
}