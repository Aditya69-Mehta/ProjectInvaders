using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("General Player Settings")]
    // [SerializeField] InputAction movement;
    [Tooltip("Player Movement Speed")]
    [SerializeField] float movementFactor = 1f;
    [Tooltip("Range Of Movement Allowed On X Axis")]
    [SerializeField] float xRange = 10f;
    [Tooltip("Range Of Movement Allowed On Y Axis")]
    [SerializeField] float yRange = 10f;

    [Tooltip("Pitch And Y Axis Relation")]
    [SerializeField] float pitchFactor = -2f;
    [Tooltip("Controls Jet Pitch Angle")]
    [SerializeField] float controlPitchFactor = -15f;

    [Tooltip("Controls Yaw Angle")]
    [SerializeField] float yawFactor = 2f;

    [Tooltip("Controls Roll Angle")]
    [SerializeField] float rollFactor = -20f;

    float xForce, yForce;

    [Tooltip("Lasers Attached To GameObject")]
    [SerializeField] GameObject[] lasers;

    [SerializeField] TMP_Text scoreText;
    int score;



    void Start()
    {
        
    }
    // void OnEnable(){
    //     movement.Enable();
    // }
    // void OnDisable(){
    //     movement.Disable();
    // }

    void Update()
    {
        ProcessTranslation(); // Position Control
        ProcessRotation(); // Rotation Control
        ProcessFiring(); // Firing Control

        
        if (Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void ProcessFiring(){
        if (Input.GetButton("Fire1")){
            // Debug.Log("Firing");
            IsFiring(true);
        }else{
            IsFiring(false);
        }

    }

    void IsFiring(bool isFiring)
    {
        foreach (GameObject laser in lasers){
            // laser.SetActive(isFiring);
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isFiring; 
        }
    }

    void ProcessRotation(){
        float pitch = transform.localPosition.y * pitchFactor + yForce * controlPitchFactor;
        float yaw = transform.localPosition.x * yawFactor;
        float roll = xForce * rollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        // Old Input Manager
        xForce = Input.GetAxis("Horizontal");
        yForce = Input.GetAxis("Vertical");

        // xForce = movement.ReadValue<Vector2>().x;
        // yForce = movement.ReadValue<Vector2>().y;




        float rawXPos = transform.localPosition.x + (movementFactor * Time.deltaTime * xForce);
        float rawYPos = transform.localPosition.y + (movementFactor * Time.deltaTime * yForce);

        float xClampped = Mathf.Clamp(rawXPos, -xRange, xRange);
        float yClampped = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(xClampped, yClampped, transform.localPosition.z);
    }


    public void IncreaseScore(int incremental){
        score += incremental;
        scoreText.text = $"Score : {score}".ToString();
        Debug.Log(score);
    }
}