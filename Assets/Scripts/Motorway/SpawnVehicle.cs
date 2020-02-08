using UnityEngine;

public class SpawnVehicle : MonoBehaviour {
    
    public enum direction {
        East = -1,
        West = 1
    }

    public direction dir;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private float distance;
    public int laneIndex;

    public bool isFree;
    private GameObject motorwayManager;
    private Vehicles vehicles;

    // Start is called before the first frame update
    void Start() {
        spawnPos = new Vector3(300f * (int) dir, gameObject.transform.position.y, 0);
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        vehicles = motorwayManager.GetComponent<Vehicles>();
        isFree = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        switch (dir) {
            case direction.East:
                var tempEast = true;
                if (vehicles.eastVehicles[laneIndex] != null) {
                    foreach (var vehicle in vehicles.eastVehicles[laneIndex]) {
                        if (Vector3.Distance(vehicle.transform.position, spawnPos) < distance) {
                            tempEast = false;
                            break;
                        }
                    }
                }
                
                isFree = tempEast;
                break;
            case direction.West:
                var tempWest = true;
                if (vehicles.westVehicles[laneIndex] != null) {
                    foreach (var vehicle in vehicles.westVehicles[laneIndex]) {
                        if (Vector3.Distance(vehicle.transform.position, spawnPos) < distance) {
                            tempWest = false;
                            break;
                        }
                    }
                }

                isFree = tempWest;
                break;
        }
    }
}
