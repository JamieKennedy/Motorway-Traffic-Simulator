using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnderlineOnHover : MonoBehaviour {

    public TMP_Text text;

    public void onEnter() {
        text.text = "<u>" + text.text;
    }

    public void onExit() {
        text.text = text.text.Replace("<u>", "");
    }
}