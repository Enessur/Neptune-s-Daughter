using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private int oxygenAmount;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public int enemyScore = 150;
    public int floatSpeed;

    private string _currentAnimation;
    private Animator _animator;

     const string BUBBLE_MOVE = "Move";
     const string BUBBLE_BLAST = "Blast";

    

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = PlayerMovement.FindObjectOfType<PlayerMovement>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ChangeAnimationState(BUBBLE_MOVE);
        LevelManager.onScoreChanged += HandleScoreChanged;
    }
    
    void FixedUpdate()
    {
        float moveAmount = floatSpeed * Time.deltaTime;
        transform.Translate(Vector3.up * moveAmount);
    }
    private void HandleScoreChanged(int newScore)
    {

    }
    private void OnDestroy()
    {
        LevelManager.onScoreChanged -= HandleScoreChanged;
    }

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            ChangeAnimationState(BUBBLE_BLAST);
            playerMovement.AddOxygen(oxygenAmount);
            
            LevelManager.Instance.AddScore(enemyScore);
            Debug.Log("add oxygen");
            Destroy(this);
            gameObject.SetActive(false);

        }

    }
    void ChangeAnimationState(string newState)
    {
        if (_currentAnimation == newState)
        {
            return;
        }
        _animator.Play(newState);
    }
}