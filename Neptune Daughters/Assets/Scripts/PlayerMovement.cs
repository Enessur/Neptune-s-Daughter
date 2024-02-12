using System;
using TMPro;
using UnityEngine;

public class PlayerMovement : LevelItem
{
    [SerializeField] private GameObject playerSkin;
    [SerializeField] private OxygenBar oxygenBar;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private TMP_Text lifeText;
    
    public float playerLife;
    public Animator _animator;
    public FloatingJoystick Joystick;
    public int maxOxygen;
    public int consumeOxygen;
    public float moveSpeed;

    public bool isRight;
    public bool isHoldingCrab;
    public static bool PointerDown = false;

    
    private int oxygen;
    private Vector2 _move;
    private Vector3 direction;
    private Rigidbody2D _rb;
    private string _currentAnimation;
    private float lastZAxis;
    
    const string PLAYER_SWIM = "Swim";
    const string PLAYER_HOLDCRAB = "HoldCrab";


    

    protected override void Init()
    {
        base.Init();
        oxygen = maxOxygen;
        oxygenBar.SetMaxOxygen(oxygen);

        _rb = GetComponent<Rigidbody2D>();

        _animator = playerSkin.GetComponent<Animator>();
        spriteRenderer = playerSkin.GetComponent<SpriteRenderer>();
        LevelManager.onScoreChanged += HandleScoreChanged;
        
        lifeText.text = $"LIFE :{playerLife}";
    }
    

    // Update is called once per frame
    private void Update()
    {
        _move.x = Joystick.Horizontal;
        _move.y = Joystick.Vertical;

        direction = Vector3.forward * Joystick.Vertical + Vector3.right * Joystick.Horizontal;

        if (Input.GetKeyDown(KeyCode.R))
        {
           // SceneManager.Instance.RestartButton();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.Instance.LoadLevel();
        }

        if (isHoldingCrab)
        {
            ChangeAnimationState(PLAYER_HOLDCRAB);
        }
        else
        {
            ChangeAnimationState(PLAYER_SWIM);
        }

        
        float hAxis = _move.x;
        float vAxis = _move.y;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        if (_move != Vector2.zero)
        {
            // Save the current rotation angle when moving
            lastZAxis = zAxis;
        }

        transform.eulerAngles = new Vector3(0f, 0f, -lastZAxis);
        if (_move.x > 0)
        {
            // Moving right
            playerSkin.transform.localScale = new Vector3(1f, 1f, 1f);
            isRight = true;
        }
        else if (_move.x < 0)
        {
            // Moving left
            playerSkin.transform.localScale = new Vector3(1f, -1f, 1f);
            isRight = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("NextLevel"))
        {
            LevelManager.Instance.AddScore(oxygen);
            SceneManager.RestartScene();
            SceneManager.Instance.NextLevel();
            Init();
        }

        else if (col.CompareTag("DeadCrab"))
        {
            isHoldingCrab = true;
        }
        else if (col.CompareTag("Crab"))
        {
            OnDeath();
        }
    }

    public void MaxHealth()
    {
        playerLife = 4;
        lifeText.text = $"LIFE :{playerLife}";
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        OnDeath();
    }

    private void OnDeath()
    {
        SoundManager.Instance.PlaySound("Die");
        SceneManager.RestartScene();
        // SceneManager.Instance.RestartButton();
        playerLife--;
        lifeText.text = $"LIFE :{playerLife}";
        
        if (playerLife < 0)
        {
           SceneManager.Instance.LoseGame();
        }
    }

    private void DecreaseOxygen()
    {
        oxygen -= consumeOxygen;
        oxygenBar.SetOxygen(oxygen);
        if (oxygen <= 0)
        {
            OnDeath();
        }
    }

    public void AddOxygen(int addOxygen)
    {
        oxygen += addOxygen;
        if (oxygen > maxOxygen)
        {
            oxygen = maxOxygen;
        }
        oxygenBar.SetOxygen(oxygen);
    }
    
    private void FixedUpdate()
    {
        DecreaseOxygen();

        if (PointerDown)
        {
        }
        else
        {
            _rb.MovePosition(_rb.position + _move * moveSpeed * Time.fixedDeltaTime);
        }
    }

    protected override void SubscribeEvents()
    {
        base.SubscribeEvents();
        SceneManager.Instance.OnNextLevel += SetStartPosition;
    }
    protected override void UnSubscribeEvents()
    {
        base.SubscribeEvents();
        SceneManager.Instance.OnNextLevel -= SetStartPosition;
    }
    
    protected override void Reset()
    {
        base.Reset();
        AddOxygen(maxOxygen);
        
    } 
    
    private void HandleScoreChanged(int newScore)
    {

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
}