using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTooltip : MonoBehaviour {
    [SerializeField]
    private string tooltipText;
    [SerializeField]
    private Vector3 position;

    [SerializeField] private GameObject element;

    [SerializeField] 
    private GameObject toolTip;

    private Vector3 tooltipPosition;

    private void Awake() {
        tooltipPosition = element.GetComponent<RectTransform>().position + new Vector3(
                              element.GetComponent<RectTransform>().rect.width,
                              -element.GetComponent<RectTransform>().rect.height / 2,
                              0);
    }

    public void Show(){
        toolTip.GetComponent<Tooltip>().ShowTooltip(tooltipText, tooltipPosition);
    }

    public void Hide() {
        toolTip.GetComponent<Tooltip>().HideToolTip();
    }
}
