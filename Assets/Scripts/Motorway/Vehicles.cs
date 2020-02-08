using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles : MonoBehaviour {
    public List<GameObject>[] eastVehicles;
    public List<GameObject>[] westVehicles;

    private Parameters parameters;
    
    // Start is called before the first frame update
    void Start() {
        parameters = gameObject.GetComponent<Parameters>();
        eastVehicles = new List<GameObject>[parameters.lanesNum];
        westVehicles = new List<GameObject>[parameters.lanesNum];
    }

    // Update is called once per frame
    void Update() { }
}
