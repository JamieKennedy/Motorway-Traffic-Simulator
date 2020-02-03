using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DurationInput : MonoBehaviour {

    public TMP_Text durationText;
    public TMP_InputField durationField;
    public TMP_Dropdown durationUnits;
    
    private readonly Color32 blackFaded = new Color32(0, 0, 0, 128);
    private readonly Color32 whiteFaded = new Color32(255, 255, 255, 128);
    
    // Checks if duration field is empty and fades out the UI elements if it is
    void Update() {
        if (durationField.text.Equals("")) {
            durationText.color = whiteFaded;
            durationField.image.color = whiteFaded;
            durationUnits.image.color = whiteFaded;
        } else {
            durationText.color = Color.white;
            durationUnits.image.color = Color.white;
            durationField.image.color = Color.white;
        }
    }
}
