using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTongue : LevelItem
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("DeadCrab"))
        {
            SceneManager.Instance.EndGameCondition();
            SoundManager.Instance.PlaySound("SnakeEat");
        }
        
        
    }
    
    protected override void Reset()
    {
        SceneManager.Instance.ResetWinCondition();
    } 
}
