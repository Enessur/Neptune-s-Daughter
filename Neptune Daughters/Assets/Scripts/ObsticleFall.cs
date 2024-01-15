using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObsticleFall : LevelItem
{
    public int fallSpeed;
    void Update()
    {
        float moveAmount = fallSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * moveAmount);
    }
    
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
 
        if (col.CompareTag("OutScreen"))
        {
            Destroy(this.gameObject);
        }
        
    }
}
