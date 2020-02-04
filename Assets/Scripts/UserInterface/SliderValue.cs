using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour {
    public Slider slider;
    public TMP_Text text;

    // Update is called once per frame
    void Update() {
        text.text = (slider.value / 10).ToString("0.0");
    }
}
