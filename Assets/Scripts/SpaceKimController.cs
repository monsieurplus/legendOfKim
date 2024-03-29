﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceKimController : MonoBehaviour
{
    public float minX = -0.9f;
    public float maxX = 0f;
    public float minZ = -0.7f;
    public float maxZ = 1f;

    public float movingSpeedX = 1f;
    public float movingDirectionX = 0f;
    public float movingSpeedZ = 2f;
    public float movingDirectionZ = 0f;

    public bool controllable = true;
    private bool moving = false;

    private AudioSource audioSource;
    public AudioClip[] soundDesign;

    public GameObject projectilePrefab;

    public GameObject rulesLabel;
    private int shotNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controllable) {
            bool keyPressedLeft = Input.GetKey("left");
            bool keyPressedRight = Input.GetKey("right");
            bool keyPressedUp = Input.GetKey("up");
            bool keyPressedDown = Input.GetKey("down");

            moving = false;

            if (keyPressedLeft != keyPressedRight)
            {
                moving = true;
                movingDirectionX = (keyPressedLeft ? -1f : 1f);
            }
            else {
                movingDirectionX = 0f;
            }

            if (keyPressedUp != keyPressedDown)
            {
                moving = true;
                movingDirectionZ = (keyPressedDown ? -1f : 1f);
            }
            else {
                movingDirectionZ = 0f;
            }

            if (Input.GetKeyDown("space")) {
                shotNumber++;
                if (shotNumber > 3)
                    rulesLabel.SetActive(false);

                GameObject proj = GameObject.Instantiate(projectilePrefab);
                proj.transform.position = transform.position - new Vector3(0f, 0f, 0.1f);
                PlayRandomDesign();
            }

            UpdatePosition();
        }
    }

    private void UpdatePosition() {
        if (moving) {
            transform.Translate(
                new Vector3(
                    Time.deltaTime * movingDirectionX * movingSpeedX, 
                    0f, 
                    Time.deltaTime * movingDirectionZ * movingSpeedZ
                )
            );

            // Clamp the player's position
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
            transform.position = pos;
        }
    }


    public void PlayRandomDesign() {
        int clipIndex = Mathf.FloorToInt(Random.Range(0f, (float)soundDesign.Length));
        if (clipIndex == soundDesign.Length)
            clipIndex--;

        audioSource.PlayOneShot(soundDesign[clipIndex]);
    }

    public void StartWalking() {
        this.GetComponent<Animator>().SetBool("walking", true);
    }

    public void StopWalking() {
        this.GetComponent<Animator>().SetBool("walking", false);
    }
}
