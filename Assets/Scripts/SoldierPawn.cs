using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPawn : Pawn
{
    public enum Allegiance {Ally, Enemy};
    public Allegiance team;
    public int killValue;
    public float damageDealt;
    [SerializeField] private float maxCountdown;
    [SerializeField] private float minCountdown;
    private float countdown;
    private int randomSeed;
    public SoldierSpawner birthSpawner;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        SetRandomCountdown(minCountdown, maxCountdown);
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
    }

    private void SetRandomCountdown(float minCountdown, float maxCountdown)
    {
        //seed the rng with a random number
        randomSeed = (int)System.DateTime.Now.Ticks;
        Random.InitState(randomSeed);

        //set random countdown
        countdown = Random.Range(minCountdown, maxCountdown);
    }

    public override void Attack(SoldierPawn target)
    {
        if(countdown <= 0)
        {
            target.GetComponent<Health>().TakeDamage(damageDealt, this);

            SetRandomCountdown(minCountdown, maxCountdown);
        }
    }

    public override void MoveForward()
    {
        if(mover != null)
        {
            mover.MoveForward(moveSpeed);
        }
    }

    public override void MoveBackward()
    {
        if (mover != null)
        {
            mover.MoveForward(-moveSpeed);
        }
    }
}
