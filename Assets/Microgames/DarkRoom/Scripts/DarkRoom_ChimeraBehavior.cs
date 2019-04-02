﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkRoom_ChimeraBehavior : MonoBehaviour {

    [Header("Adjustables")]
    [Tooltip("Health decreases by 1 per frame (e.g. 60 health should roughly hit 0 in a second)")]
    [SerializeField] private float healthMax;
    [SerializeField] private float walkSpeed;

    [Header("GameObjects")]
    [SerializeField] private GameObject renko;

    private Animator myAnimator;

    private float health;

    private bool isFleeing = false;
    private Vector2 fleeEndPosition;

	/* Base methods */

	void Start () {
        // Initialization
        myAnimator = GetComponentInChildren<Animator>();
        health = healthMax;
    }
	
	void Update () {

        // Handle movement
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("animChimeraFlee")) {
            Flee();
        } else {
            Walk();
        }

	}

    /* My methods */

    private void Walk() {
        // Walk forward
        transform.position += new Vector3(walkSpeed, 0f, 0f) * Time.deltaTime;
    }

    private void Flee() {
        // Flee backwards
        transform.moveTowards2D(fleeEndPosition, 32);
    }

    /* Collision handling */

    private void OnTriggerStay2D(Collider2D otherCollider) {
        GameObject other = otherCollider.gameObject;

        // WITH: Light
        if (other.name == "Light") {
            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("animChimeraWalk")) {
                // Decrease health
                if (health - 60 * Time.deltaTime > 0f) {
                    if (transform.position.x > renko.transform.position.x - 6f)
                        health -= 60 * Time.deltaTime;
                } else {
                    health = healthMax;
                    myAnimator.SetTrigger("isShined");
                    fleeEndPosition = transform.position - new Vector3(8f, 0f, 0f);
                }
            }

        }

    }

}
