using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameplay : MonoBehaviour
{
    public TextMeshProUGUI allySoldiersLeft;
    public TextMeshProUGUI allyPassedWall;
    public TextMeshProUGUI allyScore;

    public TextMeshProUGUI enemySoldiersLeft;
    public TextMeshProUGUI enemyPassedWall;
    public TextMeshProUGUI enemyScore;

    // Start is called before the first frame update
    void Start()
    {
        allySoldiersLeft.text = "0";
        allyPassedWall.text = "0";
        allyScore.text = "0";

        enemySoldiersLeft.text = "0";
        enemyPassedWall.text = "0";
        enemyScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        allySoldiersLeft.text = GameManager.instance.allyTally.ToString();
        allyPassedWall.text = GameManager.instance.allyToWall.ToString();
        allyScore.text = GameManager.instance.allyScore.ToString();

        enemySoldiersLeft.text = GameManager.instance.enemyTally.ToString();
        enemyPassedWall.text = GameManager.instance.enemyToWall.ToString();
        enemyScore.text = GameManager.instance.enemyScore.ToString();

        //back to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.menuButton);

            GameManager.instance.ActivateMainMenuState();
        }
    }

}
