using UnityEngine;

public class EnemyBehaviour : LevelItem
{
     public enum TaskCycleEnemy
    {
        Chase,
        Death,
    }

    [SerializeField] private TaskCycleEnemy taskCycleEnemy;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float chaseSpeed;

    public int fallSpeed;
    public int enemyScore = 200;
    private LevelManager levelManager;
    private Rigidbody2D _rb;
    private Transform _target;
    private string _currentAnimation;
    private Animator _animator;

    
    const string ENEMY_CHASE = "Chase";
    const string ENEMY_DEATH = "Death";

    
    
    protected virtual void Start()
    {
        taskCycleEnemy = TaskCycleEnemy.Chase;
        _rb = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        LevelManager.onScoreChanged += HandleScoreChanged;
    }

    protected virtual void FixedUpdate()
    {

      
        switch (taskCycleEnemy)
        {
            case TaskCycleEnemy.Chase:
                transform.position =
                    Vector2.MoveTowards(transform.position, _target.position, chaseSpeed * Time.deltaTime);
                FlipSprite(_target);
                ChangeAnimationState(ENEMY_CHASE);
                break;
          

            case TaskCycleEnemy.Death:
                ChangeAnimationState(ENEMY_DEATH);
                OnHit();
                break;
        }
    }


    protected virtual void  OnHit()
    {
        float moveAmount = fallSpeed * Time.deltaTime;
        transform.Translate(Vector3.down * moveAmount);
        
    }
    


   
   protected virtual void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Projectile"))
        {
            taskCycleEnemy = TaskCycleEnemy.Death;
              PlaySound();
            LevelManager.Instance.AddScore(enemyScore);
        }

        if (col.CompareTag("OutScreen"))
        {
            Destroy(this.gameObject);
        }
        
    }

   protected virtual void PlaySound()
   {
       SoundManager.Instance.PlaySound("JellyDie");
   }

   private void OnDestroy()
   {
       LevelManager.onScoreChanged -= HandleScoreChanged;
   }

   
   private void HandleScoreChanged(int newScore)
   {

   }
   // private void OnEnable()
   // {
   //     SceneManager.OnNextLevelCalled += DestroyOnNextLevel;
   // }
   //
   // private void OnDisable()
   // {
   //     SceneManager.OnNextLevelCalled -= DestroyOnNextLevel;
   // }
   //
   // private void DestroyOnNextLevel()
   // {
   //     Destroy(gameObject);
   // }

   private void FlipSprite(Transform dest)
    {
        spriteRenderer.flipX = (transform.position.x - dest.position.x > 0);
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
