using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    public GameObject youWin;
    public GameObject youLose;

    public void winOrLose()
    {
        //you lose if enemy score is higher than ally score
        if(GameManager.instance.enemyScore > GameManager.instance.allyScore)
        {
            youLose.SetActive(true);
            youWin.SetActive(false);
        }
        else //you win
        {
            youLose.SetActive(false);
            youWin.SetActive(true);
        }
    }

    public void Update()
    {
        winOrLose();
    }

    //Resets and restarts game
    public void RestartGameButton()
    {
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.menuButton);
        GameManager.instance.ActivateGameState();
    }

    //returns to main menu
    public void MainMenuButton()
    {
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.menuButton);
        GameManager.instance.ActivateMainMenuState();
    }

    //quits the game
    public void QuitGameButton()
    {
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.menuButton);
        Application.Quit();
    }
}