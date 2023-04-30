using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //static (stays same) game manager instance
    public static GameManager instance;
    public static AudioManager audioManager;

    [Header("Lists")]
    public List<PlayerController> players;
    public List<AIController> ais;
    public List<SoldierSpawner> spawners;
    public int allyTally;
    public int enemyTally;

    [Header("Screen State Objects")]
    public GameObject titleScreenStateObject;
    public GameObject gameOverStateObject;
    public GameObject mainMenuStateObject;
    public GameObject optionsStateObject;
    public GameObject gameStateObject;

    [Header("Team Scores")]
    public int startEnemyScore;
    public int startAllyScore;
    public int enemyScore;
    public int allyScore;
    public int enemyToWall;
    public int allyToWall;

    [Header("Team Materials")]
    public Material enemyMaterial;
    public Material allyMaterial;

    //Awake is called before Start
    private void Awake()
    {
        if (instance == null)
        {
            //this is THE game manager
            instance = this;
            //don't kill it in a new scene.
            DontDestroyOnLoad(gameObject);
        }
        else //this isn't THE game manager
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        ActivateTitleScreenState();

        //attach audiomanager to gamemanager
        audioManager = AudioManager.instance;
    }

    public void ResetGame()
    {
        enemyScore = startEnemyScore;
        allyScore = startAllyScore;
        enemyToWall = 0;
        allyToWall = 0;

        ClearSoldiers();
        SpawnSoldiers();
    }

    public bool hasTeamWon()
    {
        UpdateSoldiersLeft();

        if (enemyTally <= 0 || allyTally <= 0)
        {
            return true;
        }
        return false;
    }

    public void UpdateSoldiersLeft()
    {
        allyTally = 0;
        enemyTally = 0;
        foreach (AIController ai in GameManager.instance.ais)
        {
            if (ai.GetComponent<SoldierPawn>().team == SoldierPawn.Allegiance.Ally)
            {
                allyTally += 1;
            }
            else
            {
                enemyTally += 1;
            }
        }
    }

    public void SpawnSoldiers()
    {
        //spawn a soldier at each spawner with allegiance based on which side of the z axis they are on
        foreach(SoldierSpawner spawner in spawners)
        {
            if (spawner.transform.position.z < 0)
            {
                spawner.spawnSoldier(SoldierPawn.Allegiance.Ally, allyMaterial);
                allyTally += 1;
            }
            else
            {
                spawner.spawnSoldier(SoldierPawn.Allegiance.Enemy, enemyMaterial);
                enemyTally += 1;
            }
            
        }
    }

    public void ClearSoldiers()
    {
        if(ais != null)
        {
            foreach (SoldierSpawner spawner in spawners)
            {
                if(spawner.spawnedSoldier != null)
                {
                    //die
                    spawner.spawnedSoldier.GetComponent<Health>().Die();
                }
            }
            //clear list
            ais.Clear();
        }
        
    }


    //deactivate all gamestates
    private void DeactivateAllStates()
    {
        titleScreenStateObject.SetActive(false);
        gameOverStateObject.SetActive(false);
        mainMenuStateObject.SetActive(false);
        optionsStateObject.SetActive(false);
        gameStateObject.SetActive(false);
    }

    public void ActivateTitleScreenState()
    {
        ResetGame();
        DeactivateAllStates();
        titleScreenStateObject.SetActive(true);
    }

    public void ActivateGameOverState()
    {
        DeactivateAllStates();
        gameOverStateObject.SetActive(true);
    }

    public void ActivateMainMenuState()
    {
        DeactivateAllStates();
        mainMenuStateObject.SetActive(true);
    }

    public void ActivateOptionsState()
    {
        DeactivateAllStates();
        optionsStateObject.SetActive(true);
    }

    public void ActivateGameState()
    {
        ResetGame();
        DeactivateAllStates();
        gameStateObject.SetActive(true);
    }
}
