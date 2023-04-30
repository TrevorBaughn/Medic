using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : Controller
{
    // Start is called before the first frame update
    protected override void Start()
    {
        //add itself to list of players
        GameManager.instance.players.Add(this);
    }
    protected override void OnDestroy()
    {
        //remove itself from list of players
        GameManager.instance.players.Remove(this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(pawn != null)
        {
            ProcessInputs();
        }
    }

    protected abstract void ProcessInputs();
}
