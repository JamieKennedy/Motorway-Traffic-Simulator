﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour {
    
    private VehicleProperties vehicleProperties;
    

    private float s; // bumper to bumper distance
    private float deltaV;
    private float a;
    private float v;
    private float v0;
    private float delta;
    private float s0;
    private float T;
    private float b;

    private VehicleProperties vehicleAProperties;
    private VehicleProperties vehicleBProperties;
    private Neighbours vehicleNeighbours;

    // Start is called before the first frame update
    void Start() {
        vehicleProperties = gameObject.GetComponent<VehicleProperties>();
        vehicleNeighbours = GetComponent<Neighbours>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (vehicleProperties.canMove) {
            switch (vehicleProperties.direction) {
                case LaneProperties.direction.East:
                    vehicleProperties.currentAccel = IDM(gameObject, vehicleNeighbours.neighbours[1]);
                    break;
                
                case LaneProperties.direction.West:
                    vehicleProperties.currentAccel = IDM(gameObject, vehicleNeighbours.neighbours[4]);
                    break;
            }

            if (vehicleProperties.currentVel + vehicleProperties.currentAccel * 0.02f > 0) {
                vehicleProperties.currentVel += vehicleProperties.currentAccel * 0.02f;
            } else {
                vehicleProperties.currentVel = 0;
            }
            
            gameObject.transform.position += new Vector3(vehicleProperties.currentVel * 0.02f * (int) vehicleProperties.direction, 0 , 0);
        }
    }

    // Vehicle A follows Vehicle B
    public float IDM(GameObject vehicleA, GameObject vehicleB) {
        vehicleAProperties = vehicleA.GetComponent<VehicleProperties>();

        // Retrieve the values for the IDM
        a = vehicleAProperties.maxAccel; // Max Acceleration
        v = vehicleAProperties.currentVel; // Current velocity
        v0 = vehicleAProperties.desiredSpeed; // Desired velocity
        delta = vehicleAProperties.freeAccExponent; // Free Acceleration Exponent
        s0 = vehicleAProperties.jamDistance; // Minimum Jam Distance
        T = vehicleAProperties.desiredTimeGap; // Desired Time Gap
        b = vehicleAProperties.desiredDecel; // Desired Deceleration

        // Get the bumper to bumper distance between the two vehicles
        if (vehicleB != null) {
            vehicleBProperties = vehicleB.GetComponent<VehicleProperties>();
            switch (vehicleAProperties.direction) {
                case LaneProperties.direction.East:
                    s = (vehicleB.transform.position.x - vehicleBProperties.vehicleWidth / 2f) - 
                        (vehicleA.transform.position.x + vehicleAProperties.vehicleWidth / 2f);
                    break;
            
                case LaneProperties.direction.West:
                    s = (vehicleA.transform.position.x - vehicleAProperties.vehicleWidth / 2) -
                        (vehicleB.transform.position.x + vehicleBProperties.vehicleWidth / 2f);
                    break;
            }
            
            deltaV = vehicleAProperties.currentVel - vehicleBProperties.currentVel;
        } else {
            s = 600f;
            deltaV = 0f;
        }
        
        // IDM equation
        return Convert.ToSingle(a * (1 - Math.Pow(v / v0, delta) - Math.Pow(sStar() / s, 2)));
    }

    private double sStar() {
        return s0 + (v * T) + (v * deltaV) / (2 * Math.Sqrt(a * b));
    }
}