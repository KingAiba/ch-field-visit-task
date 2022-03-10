using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int score = 0;

    public PlayerController playerController;
    
   
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

   
    void Update()
    {
        
    }

    public void AddScore(int amount)
    {
        if(!playerController.isDead)
        {
            score += amount;
        }
        
    }
}
