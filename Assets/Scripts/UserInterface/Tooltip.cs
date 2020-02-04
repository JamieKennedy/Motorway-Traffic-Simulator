using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    public TMP_Text tooltipText;
    public GameObject toolTip;
    private RectTransform toolTipTransform;
    private RectTransform textTransform;
    
    private Vector3 backgroundSize;
    private float backgroundX;
    private float backgroundY;
    private const float textPaddingSize = 8f;
    private const float maxTextWidth = 300;

    private Vector2 localPoint;
    private readonly Vector3 offset = new Vector3(805, 600, 0);

    public Canvas canvas;
    private void Awake() {
        toolTipTransform = toolTip.GetComponent<RectTransform>();
        textTransform = tooltipText.GetComponent<RectTransform>();
        ShowTooltip("Hello My name is Jamie", new Vector3(50, 50, 0));
    }

    public void ShowTooltip(string tooltipString, Vector3 position) {
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        
        backgroundY = (tooltipText.preferredHeight + textPaddingSize * 2f);
        if (tooltipText.preferredWidth < maxTextWidth) {
            backgroundX = tooltipText.preferredWidth + textPaddingSize * 2f;
            textTransform.sizeDelta = new Vector2(backgroundX, backgroundY);
        } else {
            backgroundX = maxTextWidth + textPaddingSize * 2f;
        }
        backgroundSize = new Vector3(backgroundX, backgroundY);
        toolTipTransform.sizeDelta = backgroundSize;
        toolTip.transform.position = position;
    }

    public void HideToolTip() {
        gameObject.SetActive(false);
    }


}
