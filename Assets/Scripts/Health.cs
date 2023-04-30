using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    private SoldierPawn soldier;
    public float healAmount;
    public float healPrice;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        soldier = GetComponent<SoldierPawn>();
    }

    //heal on click
    private void OnMouseDrag()
    {
        //if allies have score to spend, not max health, heal and remove points
        if (soldier.team == SoldierPawn.Allegiance.Ally &&
            GameManager.instance.allyScore > 0 &&
            currentHealth < maxHealth)
        {
            Heal(healAmount);

            GameManager.instance.allyScore -= (int)healPrice;
        }
        //if it's an enemy soldier with points to spend and not at max health
        else if (soldier.team == SoldierPawn.Allegiance.Enemy &&
                GameManager.instance.enemyScore > 0 &&
                currentHealth < maxHealth)
        {
            //heal soldier
            Heal(healAmount);
            //remove score as this happens
            GameManager.instance.enemyScore -= (int)healPrice;
        }

    }

    public void TakeDamage(float amount, SoldierPawn owner)
    {
        //take damage
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die(owner);
        }
    }

    public void Heal(float amount)
    {
        //heal
        currentHealth += amount;
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.heal);
        //clamp health at max
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    private void RewardKiller(SoldierPawn killer, int rewardAmount)
    {
        if (killer != null)
        {
            if (killer.team == SoldierPawn.Allegiance.Enemy)
            {
                GameManager.instance.enemyScore += rewardAmount;
            }
            else
            {
                GameManager.instance.allyScore += rewardAmount;
            }
        }
    }

    public void Die()
    {
        if(soldier != null)
        {
            //destroy soldier
            soldier.birthSpawner.destroySoldier();
        }

        //check for win state
        if (GameManager.instance.hasTeamWon())
        {
            GameManager.instance.ActivateGameOverState();
        }
    }

    public void Die(SoldierPawn killer)
    {
        RewardKiller(killer, soldier.killValue);
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.death);
        Die();
    }
}
