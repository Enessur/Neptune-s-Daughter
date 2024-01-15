using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaPlant : LevelItem
{


    [SerializeField] private int moveAmount;
    public int enemyScore = 50;

    private void Start()
    {
        LevelManager.onScoreChanged += HandleScoreChanged;
    }
   

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Projectile"))
        {
            GoDown();
            LevelManager.Instance.AddScore(enemyScore);
        }
        
    }

    private void GoDown()
    {
        transform.Translate(Vector3.down * moveAmount);
    }
    private void HandleScoreChanged(int newScore)
    {

    }
}
