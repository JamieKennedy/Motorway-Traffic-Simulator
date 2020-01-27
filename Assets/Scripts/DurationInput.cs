using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurationInput : MonoBehaviour {

    public Text durationText;
    public InputField durationField;
    public Dropdown durationUnits;
    
    private readonly Color32 blackFaded = new Color32(0, 0, 0, 128);
    private readonly Color32 whiteFaded = new Color32(255, 255, 255, 128);
    
    // Checks if duration field is empty and fades out the UI elements if it is
    void Update() {
        if (durationField.text.Equals("")) {
            durationText.color = blackFaded;
            durationField.image.color = whiteFaded;
            durationUnits.image.color = whiteFaded;
        } else {
            durationText.color = Color.black;
            durationUnits.image.color = Color.white;
            durationField.image.color = Color.white;
        }
    }
}
