using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] ParticleSystem enemyExplosionVFX;
    [SerializeField] ParticleSystem enemyHitVFX;

    [SerializeField] int enemyHitPoint = 1;
    [SerializeField] int enemyHealthPoints = 1;

    GameObject spawnParent;
    ScoreBoard scoreBoard;

    void Start(){
        scoreBoard = FindAnyObjectByType<ScoreBoard>();
        spawnParent = GameObject.FindWithTag("SpawnAtRunTime");

        gameObject.AddComponent<Rigidbody>().useGravity=false;
        enemyHealthPoints *= 2;
    }

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
        ParticleSystem vfx = Instantiate(enemyExplosionVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = spawnParent.transform;
        Destroy(this.gameObject);
    }

    void ScoreUpdation()
    {
        scoreBoard.IncreaseScore(enemyHitPoint);
    }
}
