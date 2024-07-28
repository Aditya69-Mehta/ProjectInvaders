using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    void Awake(){ // Singleton Pattern
        int musicSourceCount = FindObjectsOfType<MusicController>().Length;
        
        if (musicSourceCount > 1){
            Destroy(gameObject);
        }else {
            DontDestroyOnLoad(gameObject);
        }
    }
}
