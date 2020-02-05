using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowTooltip : MonoBehaviour {

    [SerializeField] private GameObject toolTip;


    public void Show(){
        toolTip.SetActive(true);
    }

    public void Hide() {
        toolTip.SetActive(false);
    }
}
