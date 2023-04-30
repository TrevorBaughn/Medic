using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public Controller controller;
    protected Mover mover;

    [Header("Speeds")]
    public float maxMoveSpeed;
    public float baseMoveSpeed;
    public float moveSpeed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        mover = GetComponent<Mover>();
        controller = GetComponent<Controller>();
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void Attack(SoldierPawn target);
}
