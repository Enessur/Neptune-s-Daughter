using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuffFish : EnemyBehaviour
{
    private enum Inheritance
    {
        Stop
    }

    [SerializeField] private Inheritance inheritance;

    [SerializeField] private GameObject oxygen;
    private Collider2D puffFishCollider;  // Reference to the collider component

    private int randNum;
    private bool hold;
    private bool onHit = true;
    
    protected override void Start()
    {
        base.Start();
        puffFishCollider = GetComponent<Collider2D>();  // Assign the collider reference
        randNum = Random.Range(2, 6);
        StartCoroutine(RepeatWaitAndWorkCoroutine());
    }

    IEnumerator RepeatWaitAndWorkCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(randNum);
            randNum = Random.Range(2, 6);
            hold = false;

            yield return new WaitForSeconds(randNum);
            randNum = Random.Range(2, 6);
            hold = true;
        }
    }

    protected override void FixedUpdate()
    {
        if (hold)
        {
            inheritance = Inheritance.Stop;
        }
        else
        {
            base.FixedUpdate();
        }

        switch (inheritance)
        {
            case Inheritance.Stop:
                break;
        }
    }

    protected override void OnHit()
    {
        // base.OnHit();

        if (onHit)
        {
            SceneManager.Instance.WinCondition();
            puffFishCollider.enabled = false;
            Instantiate(oxygen, transform.position, transform.rotation);
            onHit = false;
        }
    }

    protected override void PlaySound()
    {
        SoundManager.Instance.PlaySound("PuffDie");
    }

    protected override void Reset()
    {
        base.Reset();
        SceneManager.Instance.ResetWinCondition();
    } 

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);
        hold = false;
    }
}