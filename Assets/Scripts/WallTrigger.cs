using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.toWall);

        //increase to wall scores and team scores
        if (other.GetComponent<SoldierPawn>().team == SoldierPawn.Allegiance.Ally)
        {
            GameManager.instance.allyToWall += 1;
            GameManager.instance.allyScore += other.GetComponent<SoldierPawn>().killValue;
        }
        else
        {
            GameManager.instance.enemyToWall += 1;
            GameManager.instance.enemyScore += other.GetComponent<SoldierPawn>().killValue;
        }

        //kill it
        other.GetComponent<Health>().Die();
    }

}
