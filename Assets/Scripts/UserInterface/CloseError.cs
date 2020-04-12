using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseError : MonoBehaviour {

    public GameObject errorBox;

    public void Close() {
        errorBox.SetActive(false);
    }
}