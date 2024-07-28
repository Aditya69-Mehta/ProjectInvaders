using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] ParticleSystem enemyExplosionVFXnSFX;
    [SerializeField] ParticleSystem enemyHitVFX;

    [SerializeField] int enemyHitPoint = 1;
    [SerializeField] int enemyHealthPoints = 1;

    // [SerializeField] float visibleTime = 0f;
    // [SerializeField] float selfDestructTime = 5f;


    GameObject spawnParent;
    ScoreBoard scoreBoard;

    void Start(){
        scoreBoard = FindAnyObjectByType<ScoreBoard>();
        spawnParent = GameObject.FindWithTag("SpawnAtRunTime");

        gameObject.AddComponent<Rigidbody>().useGravity=false;
        enemyHealthPoints *= 2;
    }

    // void Update(){
    //     float currTime = Time.time;
    //     Debug.Log(currTime);
    //     if (currTime >= visibleTime || visibleTime == 0){
    //         gameObject.SetActive(true);
    //     }else{
    //         gameObject.SetActive(false);
    //     }
    //     if (currTime >= selfDestructTime){
    //         Destroy(gameObject);
    //     }
    // }

    void OnParticleCollision(GameObject other)
    {
        HitProcess();
        if (enemyHealthPoints == 0){
            EnemySpawnAndDestroy();
            ScoreUpdation();
        }
    }

    void HitProcess()
    {
        ParticleSystem hitVFX = Instantiate(enemyHitVFX, transform.position, Quaternion.identity);
        hitVFX.transform.parent = spawnParent.transform;
        enemyHealthPoints--;
    }

    void EnemySpawnAndDestroy()
    {
        ParticleSystem vfxNsfx = Instantiate(enemyExplosionVFXnSFX, transform.position, Quaternion.identity);
        vfxNsfx.transform.parent = spawnParent.transform;
        Destroy(this.gameObject);
    }

    void ScoreUpdation()
    {
        scoreBoard.IncreaseScore(enemyHitPoint);
    }
}
