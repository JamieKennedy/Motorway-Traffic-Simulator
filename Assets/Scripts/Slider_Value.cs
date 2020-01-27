using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slider_Value : MonoBehaviour {
    public Slider slider;
    public TMP_Text text;

    // Update is called once per frame
    void Update() {
        text.text = slider.value.ToString("0");
    }
}
