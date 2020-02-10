using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleProperties : MonoBehaviour {
    
    [SerializeField] public float desiredSpeed;
    public bool canMove = false;
    public int currentLane;
    public LaneProperties.direction direction;
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}