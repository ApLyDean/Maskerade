using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //manage player and enemy hp
    //attack and damage animations for player and enemy
    //enemy chases player once spotted
    //maybe have enemy indicator once enemy starts chasing player
    //have enemy despawn ("die") when health hits 0
    //have player die when health hits 0
    
    #region Character HP Variables
    int playerCurrentHP;
    int playerMaxHP;
    public int enemyCurrentHP;
    public int enemyMaxHP;
    #endregion 

    public GameObject enemyIndicators;
    public GameObject playerIndicator;

    bool playerSpotted;

    // Start is called before the first frame update
    void Start()
    {
        //connect player health to player and enemy health to enemy
        playerCurrentHP = playerMaxHP = 12;
        FindEnemyHP();
        enemyCurrentHP = enemyMaxHP;     
        enemyIndicators.SetActive(false);
        playerIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHP <= 0)
        {
            //player dies
            PlayerDeath();
        }        
    }

    public void FindEnemyHP()
    {                
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("Rabble");
        foreach (GameObject go in gameObjectArray) 
        {     
            enemyMaxHP = 2;         
        }

    }

    public void EnemyPatrol()
    {
        /*if(playerSpotted)
        {
            enemyIndicators.SetActive(true);
            playerIndicator.SetActive(true);
            EnemyPursuit();
        }*/
    }

    void EnemyPursuit()
    {

    }

    public void PlayerHealthChange()
    {
        //player health increase
        //player health decrease
        /*if (playerCurrentHP <= 0)
        {
            playerCurrentHP = 0;
        }
        if (playerCurrentHP >= playerMaxHP)
        {
            playerCurrentHP = playerMaxHP;
        }*/
    }

    public void EnemyHealthChange()//variable for change)
    {
        //I am unsure if the enemies will be allowed to heal at this point
        //but at the very least they sure can take damage
        /*if (enemyCurrentHP <= 0)
        {
            EnemyDefeated();
        }*/
    }

    public void EnemyDefeated()
    {

    }

    public void PlayerDeath()
    {
        Debug.Log("Player Dead");
        //what happens when player dies?
        //game over screen appears with options to quit or restart
        //player's health should return to max
    }
}
