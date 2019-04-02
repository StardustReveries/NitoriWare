
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoom_BatBehavior : MonoBehaviour {

    [Header("Adjustables")]
    [Tooltip("Health decreases by 1 per frame (e.g. 60 health should roughly hit 0 in a second)")]
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float sinAmplitude;
    [SerializeField] private float sinSpeed;
    [SerializeField] private float flyAwayAccel;

    private ParticleSystem myParticleSystem;

    private float startY;
    private float startTime;

    private float sinOffset;

    private bool hasFlownAway = false;
    private float flyAwayDirection;
    private float flyAwayComponentX;
    private float flyAwayComponentY;
    private float flyAwaySpeed = 1f;

    /* Base methods */

    void Start () {
        // Initialization
        myParticleSystem = GetComponentInChildren<ParticleSystem>();

        startY = transform.position.y;
        startTime = Time.time;

        flyAwayDirection = Random.Range(0.05f, 0.20f) * Mathf.PI;
        flyAwayComponentX = Mathf.Cos(flyAwayDirection);
        flyAwayComponentY = Mathf.Sin(flyAwayDirection);

        sinOffset = Random.Range(0f, 2f);
	}

	void Update () {

        // Handle flying
        if (hasFlownAway)
            FlyAway();
        else
            Fly(sinAmplitude, sinSpeed, sinOffset);

	}

    /* My methods */

    private void Fly(float sinAmplitude, float sinSpeed, float sinOffset) {
        // Fly towards Renko
        float y = startY + Mathf.Sin((Time.time - startTime) * sinSpeed + Mathf.PI * sinOffset) * sinAmplitude;
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, y, transform.position.z);
    }
    
    private void FlyAway() {
        // Fly away
        flyAwaySpeed += flyAwayAccel * Time.deltaTime;
        transform.position += new Vector3(flyAwayComponentX, flyAwayComponentY, 0f) * flyAwaySpeed * Time.deltaTime;
    }
    
    /* Collision handling */

    private void OnTriggerStay2D(Collider2D otherCollider) {
        GameObject other = otherCollider.gameObject;

        // WITH: Light
        if (other.name == "Light") {
            if (hasFlownAway) return;

            health -= 60 * Time.deltaTime;
            if (health <= 0) hasFlownAway = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D otherCollider) {
        GameObject other = otherCollider.gameObject;

        // WITH: Light
        if (other.name == "Light") {
            if (hasFlownAway) return;
            myParticleSystem.Play();
        }

    }

    private void OnTriggerExit2D(Collider2D otherCollider) {
        GameObject other = otherCollider.gameObject;

        // WITH: Light
        if (other.name == "Light") {
            if (hasFlownAway) return;
            myParticleSystem.Stop();
        }

    }

    /* Getters and setters */

    public bool HasFlownAway { get { return hasFlownAway; } set { hasFlownAway = value; } }

}
