using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawner : MonoBehaviour
{
    public GameObject soldierToSpawn;
    public GameObject spawnedSoldier;

    public void Start()
    {
        GameManager.instance.spawners.Add(this);
    }

    public GameObject spawnObject(GameObject objectToSpawn)
    {
        return Instantiate(objectToSpawn, transform.position, transform.rotation);
    }

    public void spawnSoldier(SoldierPawn.Allegiance team, Material material)
    {
        if(spawnedSoldier != null)
        {
            return;
        }
        //spawn the soldier and set it's team
        GameObject soldier = spawnObject(soldierToSpawn);
        soldier.GetComponent<SoldierPawn>().team = team;
        soldier.GetComponent<SoldierPawn>().birthSpawner = this;
        soldier.GetComponent<Renderer>().material = material;

        //organize hierarchy
        soldier.transform.parent = this.transform;

        //save it
        spawnedSoldier = soldier;
    }

    public void destroySoldier()
    {
        if(spawnedSoldier != null)
        {
            GameManager.instance.ais.Remove(spawnedSoldier.GetComponent<AISoldierController>());
            Destroy(spawnedSoldier);
            spawnedSoldier = null;
        }
    }
}
