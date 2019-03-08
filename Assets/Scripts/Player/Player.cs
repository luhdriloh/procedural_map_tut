using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void HealthChangeHandler(int newHealthValue, int maxHealthValue);

public class Player : MonoBehaviour
{
    public event HealthChangeHandler _healthChangedEvent;

    // player stats
    public int _maxHealth;
    public int _health;
    public float _movementSpeed;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _walking;
    private bool _rightFacing;

    private void Awake()
    {
        // set up player ammo
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _walking = false;
        _rightFacing = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        Move();
    }

    private void Move()
    {
        float xMovement = Input.GetAxisRaw("Horizontal");

        // flip sprite if needed
        if (xMovement > Mathf.Epsilon)
        {
            if (_rightFacing == false)
            {
                _rightFacing = true;
                _spriteRenderer.flipX = false;
            }
        }
        else if (xMovement < -Mathf.Epsilon)
        {
            if (_rightFacing)
            {
                _rightFacing = false;
                _spriteRenderer.flipX = true;
            }
        }

        // set new player speed
        Vector2 movementInDirection = new Vector2(xMovement, Input.GetAxisRaw("Vertical")).normalized * _movementSpeed;
        _rigidbody.velocity = movementInDirection;

        // set walking animation
        if (_walking == false && movementInDirection.magnitude >= 1)
        {
            _animator.SetBool("Moving", true);
            _walking = true;
        }
        else if (_walking == true && movementInDirection.magnitude < 1)
        {
            _animator.SetBool("Moving", false);
            _walking = false;
        }

        float yPos = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y, -3f + (yPos * .01f));
    }


    public void AddHealthChangeSubscriber(HealthChangeHandler healthChangeHandler)
    {
        _healthChangedEvent += healthChangeHandler;
    }


    public void HealthChange(int healthAmount)
    {
        // change health based on if health is added or subtracted
        _health = healthAmount < 0
            ? Mathf.Max(_health + healthAmount, 0)
            : Mathf.Min(_health + healthAmount, _maxHealth);

        OnHealthChange();

        if (_health <= 0)
        {
            // death animation
            Destroy(this);
        }
    }


    protected virtual void OnHealthChange()
    {
        if (_healthChangedEvent != null)
        {
            _healthChangedEvent(_health, _maxHealth);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
        {
            if (_health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            _health--;
        }
    }
}
