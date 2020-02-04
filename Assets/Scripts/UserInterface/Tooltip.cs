using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    private void Awake() {
        toolTipTransform = toolTip.GetComponent<RectTransform>();
        textTransform = tooltipText.GetComponent<RectTransform>();
        HideToolTip();
    }

    public void ShowTooltip(string tooltipString) {
        gameObject.SetActive(true);
        tooltipText.text = tooltipString;
        
        backgroundY = (tooltipText.preferredHeight + textPaddingSize * 2f);
        
        Debug.Log(tooltipText.preferredWidth);
        if (tooltipText.preferredWidth < maxTextWidth) {
            backgroundX = tooltipText.preferredWidth + textPaddingSize * 2f;
            textTransform.sizeDelta = new Vector2(backgroundX, backgroundY);
        } else {
            backgroundX = maxTextWidth + textPaddingSize * 2f;
        }
        backgroundSize = new Vector3(backgroundX, backgroundY);
        toolTipTransform.sizeDelta = backgroundSize;

    }

    private void HideToolTip() {
        gameObject.SetActive(false);
    }

    
}
