using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Crab : LevelItem
{
    public enum TaskCycleCrab
    {
        Patrol,
        Death
    }

    [SerializeField] private TaskCycleCrab taskCycleCrab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int patrolSpeed;
    [SerializeField] private float xMin, yMin, xMax, yMax;
    [SerializeField] private float startWaitTime = 1f;
    public Transform moveSpot;
    public GameObject patrolBorders;
    public GameObject deadCrab;
    public int fallSpeed;
    private float _patrolTimer;
    private string _currentAnimation;
    private Animator _animator;
    private Vector3 _patrolPos;
    private bool isHit;

    private void Start()
    {
        BoxCollider2D squareCollider = patrolBorders.GetComponent<BoxCollider2D>();

        xMin = patrolBorders.transform.position.x - squareCollider.size.x / 2;
        xMax = patrolBorders.transform.position.x + squareCollider.size.x / 2;
        yMin = patrolBorders.transform.position.y - squareCollider.size.y / 2;
        yMax = patrolBorders.transform.position.y + squareCollider.size.y / 2;

        _patrolPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));

        moveSpot.SetParent(null);
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        switch (taskCycleCrab)
        {
           


            case TaskCycleCrab.Patrol:
                PatrolPosition();
                transform.position =
                    transform.position =
                        Vector2.MoveTowards(transform.position, _patrolPos, patrolSpeed * Time.deltaTime);
                FlipSprite(moveSpot);
                break;
            
            case TaskCycleCrab.Death:
                // ChangeAnimationState();
                OnHit();
                break;
        }
    }

    private void PatrolPosition()
    {
        _patrolTimer += Time.deltaTime;

        if (!(_patrolTimer >= startWaitTime)) return;
        _patrolTimer = 0;

        transform.position =
            Vector2.MoveTowards(transform.position, _patrolPos, patrolSpeed * Time.deltaTime);

        if (transform.position == (Vector3)_patrolPos)
        {
            _patrolPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        }
    }


    protected virtual void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Projectile"))
        {
            taskCycleCrab = TaskCycleCrab.Death;

        }
        if (col.CompareTag("OutScreen"))
        {
            Destroy(this.gameObject);
        }

        if (col.CompareTag("Player"))
        {
            if (isHit)
            {
                Destroy(this.gameObject); 
                gameObject.SetActive(false);
            }
            else
            {
               
            }
            
        }
        
    }

    private void FlipSprite(Transform dest)
    {
        spriteRenderer.flipX = (transform.position.x - dest.position.x < 0);
    }

    void ChangeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (_currentAnimation == newState)
        {
            return;
        }

        //play the animation
        _animator.Play(newState);
    }

    private void OnHit()
    {
        isHit = true;
        gameObject.tag = "DeadCrab";
        
        // gameObject.layer = LayerMask.NameToLayer("DeadCrabLayer");
        float moveAmount = fallSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * moveAmount);
    }

    
    
}