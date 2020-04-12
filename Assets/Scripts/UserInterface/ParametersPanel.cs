using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParametersPanel : MonoBehaviour {

    public TMP_InputField speedLimitInputText;
    public TMP_Text speedLimitUnitsText;
    public TMP_InputField arrivalRateInputText;
    public TMP_Text sliderValueText;
    public Slider politenessSlider;

    private GameObject motorwayManager;
    private Parameters parameters;

    // Start is called before the first frame update
    void Start() {
        motorwayManager = GameObject.FindWithTag("MotorwayManager");
        parameters = motorwayManager.GetComponent<Parameters>();

        speedLimitInputText.text = convertSpeed(parameters.speedLimit).ToString();
        speedLimitUnitsText.text = parameters.speedUnits;

        arrivalRateInputText.text = (parameters.arrivalRate * 60f).ToString();

        politenessSlider.value = parameters.politeness;
    }

    // Update is called once per frame
    void Update() {
    }
    
    private float convertSpeed(float speed) {
        switch (parameters.speedUnits) {
            case "Mph":
                return speed * 2.237f;
            case "Kph":
                return speed * 3.6f;
        }

        return 0f;
    }
    
    private float convertToFloat(string str) {
        try {
            return float.Parse(str);
        } catch {
            return 0f;
        }
    }
}