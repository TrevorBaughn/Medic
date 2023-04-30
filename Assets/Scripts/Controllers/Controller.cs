using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;

    [Header("Pawn Data")]
    public int score;

    // Start is called before the first frame update
    protected abstract void Start();

    // Update is called when the object is destroyed
    protected abstract void OnDestroy();
}
