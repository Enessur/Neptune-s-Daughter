using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabDeath : MonoBehaviour
{

    [SerializeField] private int fallSpeed;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Snake"))
        {
            Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveAmount = fallSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * moveAmount);
    }
}
