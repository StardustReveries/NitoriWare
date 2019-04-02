using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoom_TrapdoorBehavior : MonoBehaviour {

    [Header("Adjustables")]
    [SerializeField] private float rotationSpeed;

    [Header("Sprites")]
    [SerializeField] private Sprite lampOpen;
    [SerializeField] private Sprite lampClose;

    private GameObject myDoor;
    private GameObject myLamp;
    private SpriteRenderer myLampSpriteRenderer;

    private Quaternion targetRotation;
    private bool isOpen = true;

	/* Base methods */

	void Start () {
        // Initialization
        myDoor = transform.Find("Rig/Door").gameObject;
        myLamp = transform.Find("Rig/Hinge/Lamp").gameObject;
	}

	void Update () {
        
        HandleDoorRotation();

	}

    /* My methods */

    private void HandleDoorRotation() {
        // Handle door rotation
        myDoor.transform.rotation = Quaternion.RotateTowards(myDoor.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        targetRotation = (isOpen) ? Quaternion.Euler(0f, 0f, 180f) : Quaternion.Euler(0f, 0f, 90f);
    }

    /* Collision handling */

    private void OnTriggerStay2D(Collider2D otherCollider) {
        GameObject other = otherCollider.gameObject;

        // WITH: Light
        if (other.name == "Light") {
            myLamp.GetComponent<SpriteRenderer>().sprite = lampClose;
            isOpen = false;
        }

    }

    private void OnTriggerExit2D(Collider2D otherCollider) {
        GameObject other = otherCollider.gameObject;

        // WITH: Light
        if (other.name == "Light") {
            myLamp.GetComponent<SpriteRenderer>().sprite = lampOpen;
            isOpen = true;
        }

    }

    /* Getters and setters */

    public bool IsOpen { get { return isOpen; } set { isOpen = value; } }

}
