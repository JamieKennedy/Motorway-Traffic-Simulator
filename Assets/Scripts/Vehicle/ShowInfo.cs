using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfo : MonoBehaviour {
    private CurrentlySelected currentlySelected;
    
    // Start is called before the first frame update
    void Start() {
        currentlySelected = GameObject.Find("VehicleInfoPanel").GetComponent<CurrentlySelected>();
    }

    private void OnMouseDown() {
        currentlySelected.currentVehicle = gameObject;
    }
}
