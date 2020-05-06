using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colours : MonoBehaviour {

    public Sprite[] colours = new Sprite[7];
    public Sprite[] coloursHighlighted = new Sprite[7];

    private GameObject infoPanel;
    private CurrentlySelected currentlySelected;

    private SpriteRenderer spriteRenderer;
    
    private int colourIndex;
    // Start is called before the first frame update
    void Start() {
        colourIndex = Random.Range(0, 6);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = colours[colourIndex];
        infoPanel = GameObject.Find("VehicleInfoPanel");
        currentlySelected = infoPanel.GetComponent<CurrentlySelected>();
    }

    // Update is called once per frame
    void Update() {
        if (currentlySelected.currentVehicle == gameObject) {
            if (spriteRenderer.sprite != coloursHighlighted[colourIndex]) {
                spriteRenderer.sprite = coloursHighlighted[colourIndex];
            }
        } else {
            if (spriteRenderer.sprite != colours[colourIndex]) {
                spriteRenderer.sprite = colours[colourIndex];
            }
        }
    }
}