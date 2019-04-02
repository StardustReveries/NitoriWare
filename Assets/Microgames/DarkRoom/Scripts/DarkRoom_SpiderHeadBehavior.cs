using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoom_SpiderHeadBehavior : MonoBehaviour {

    [Header("Adjustables")]
    [SerializeField] private float retreatSpeed;
    [SerializeField] private float lowerSpeed;
    [SerializeField] private float retreatDelay;

    [Header("GameObjects")]
    [SerializeField] private GameObject light;

    private Transform transformThread;
    
    private float retreatDelayTimer;

	/* Base methods */

	void Start () {
        // Initialization
        transformThread = transform.parent.Find("Thread");
	}
	
	void Update () {

        // Handle movement
        retreatDelayTimer -= Time.deltaTime;

        if (retreatDelayTimer > 0f)
            Retreat();
        else
        if (retreatDelayTimer < -2 * retreatDelay * Time.deltaTime)
            Lower();
    }

    /* My methods */

    private void Retreat() {
        // Retreat up
        if (transform.position.y + retreatSpeed * Time.deltaTime >= 7f) {
            transform.position = new Vector3(transform.position.x, 7f, transform.position.z);
            transformThread.localScale = new Vector3(transformThread.localScale.x, 1f, transformThread.localScale.z);
        } else {
            transform.position += new Vector3(0f, retreatSpeed, 0f) * Time.deltaTime;
            transformThread.localScale += new Vector3(0f, -retreatSpeed, 0f) * Time.deltaTime;
        }
    }

    private void Lower() {
        if ((light.transform.position - transform.position).magnitude < light.transform.localScale.x)
            return;

        // Lower.. down
        if (transform.position.y - retreatSpeed * Time.deltaTime <= 2f) {
            transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
            transformThread.localScale = new Vector3(transformThread.localScale.x, 6f, transformThread.localScale.z);
        } else {
            transform.position += new Vector3(0f, -lowerSpeed, 0f) * Time.deltaTime;
            transformThread.localScale += new Vector3(0f, lowerSpeed, 6f) * Time.deltaTime;
        }
    }
    
    /* Collision handling */

    private void OnTriggerStay2D(Collider2D otherCollider) {
        GameObject other = otherCollider.gameObject;

        // WITH: Light
        if (other.name == "Light") {
            retreatDelayTimer = retreatDelay;
        }

    }

}
