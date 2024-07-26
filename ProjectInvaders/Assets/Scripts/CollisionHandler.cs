using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] GameObject[] jetParts;
    // [SerializeField] GameObject[] jetCollider;
    // [SerializeField] ParticleSystem[] lasers;
    
    private void OnTriggerEnter(Collider other) {
        Debug.Log($"{name}Triggered{other.name} ");
        
        CrashSequence();
    }

    private void CrashSequence()
    {
        gameObject.GetComponent<PlayerController>().enabled = false; // Disables Player Controller Script
        foreach (var part in jetParts){
            part.SetActive(false);
        }
        // foreach (var laser in lasers){
        //     laser.SetActive(false);
        // }

        explosionVFX.Play();
        Invoke("ReloadLevel",loadDelay);
    }
    private void ReloadLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Loads Current Scene
    }
}
