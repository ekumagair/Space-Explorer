using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("Controls")]
    public float speedX;
    public float jumpTimeDefault;
    public float forceYDefault;

    [Header("Collision")]
    public LayerMask solidMask;
    public bool isOnGround;

    [Header("Attacks")]
    public GameObject projectile;
    public GameObject projectile2;

    [Header("Sounds")]
    public AudioClip jumpSound;
    public AudioClip damageSound;
    public AudioClip defeatSound;
    public AudioClip fire1;
    public AudioClip fire2;

    [Header("Effects")]
    public GameObject defeatEffect;

    [Header("Debug Test")]
    public GameObject lagTest;

    public static int weaponUpgrade = 0;
    public static int hp = 3;
    public static bool invulnerability = false;
    public static bool isAlive = true;
    public static bool completedLevel = false;

    private float _moveX;
    private float _jumpTime;
    private RaycastHit2D _rayHit;
    private bool _wallLeft, _wallRight;
    private bool _hitCeiling = false;
    private bool _canShoot = true;

    private Scene _scene;
    private string _sceneName;

    private Rigidbody2D _rb;
    private Collider2D _collider;
    private SpriteRenderer _sr;
    private Animator _animator;
    private AudioSource _as;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _sr = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Shooting", false);
        _as = GetComponent<AudioSource>();

        invulnerability = false;
        _collider.enabled = true;
        _sr.enabled = true;
        _canShoot = true;
        completedLevel = false;
        _hitCeiling = false;
        isAlive = true;
        hp = 3;

        _scene = SceneManager.GetActiveScene();
        _sceneName = _scene.name;

        if (StaticClass.passedCheckpoint == true)
        {
            transform.position = new Vector3(StaticClass.checkpointX, StaticClass.checkpointY, transform.position.z);
        }

        if (StaticClass.hardMode == false)
        {
            StaticClass.enemySpeedMult = 1.0f;
        }
        else
        {
            StaticClass.enemySpeedMult = 2.0f;
        }
    }

    void Update()
    {
        // Movement
        if (isAlive == true && completedLevel == false && Time.timeScale > 0)
        {
            _moveX = Input.GetAxisRaw("Horizontal") * speedX * Time.deltaTime;
        }
        else
        {
            _moveX = 0;
            _animator.SetBool("MovingX", false);
        }

        _rayHit = Physics2D.BoxCast(transform.position, new Vector2(_collider.bounds.size.x * 0.52f, _collider.bounds.size.y * 0.925f), 0, transform.right, 0.4f, solidMask);

        if (_rayHit.collider != null)
        {
            _wallRight = true;
        }
        else
        {
            _wallRight = false;
        }

        _rayHit = Physics2D.BoxCast(transform.position, new Vector2(_collider.bounds.size.x * 0.52f, _collider.bounds.size.y * 0.925f), 0, -transform.right, 0.4f, solidMask);

        if (_rayHit.collider != null)
        {
            _wallLeft = true;
        }
        else
        {
            _wallLeft = false;
        }

        if (((_moveX > 0 && _wallRight == false) || (_moveX < 0 && _wallLeft == false)) && isAlive && completedLevel == false)
        {
            transform.Translate(_moveX, 0, 0);
        }

        if (_moveX > 0)
        {
            _sr.flipX = false;
            _animator.SetBool("MovingX", true);
        }
        else if (_moveX == 0)
        {
            _animator.SetBool("MovingX", false);
        }
        else if (_moveX < 0)
        {
            _sr.flipX = true;
            _animator.SetBool("MovingX", true);
        }

        // Jump
        _rayHit = Physics2D.CircleCast(transform.position, 0.36f, -transform.up, _collider.bounds.size.y * 0.65f, solidMask);

        if (_rayHit.collider != null)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if (Input.GetKeyDown(KeyCode.X) && isOnGround && isAlive && completedLevel == false && _hitCeiling == false && Time.timeScale > 0)
        {
            _jumpTime = jumpTimeDefault;
            Sound(jumpSound);
        }

        _animator.SetBool("OnGround", isOnGround);

        // Hit ceiling
        _rayHit = Physics2D.CircleCast(transform.position, 0.365f, transform.up, _collider.bounds.size.y * 0.33f, solidMask);

        if (_rayHit.collider != null)
        {
            _hitCeiling = true;

            //_rb.velocity = new Vector2(_rb.velocity.x, Mathf.Abs(_rb.velocity.y) * -1f);
            _rb.velocity = new Vector2(_rb.velocity.x, -6f);
        }
        else if (Input.GetKey(KeyCode.X) == false)
        {
            _hitCeiling = false;
        }

        // Shoot
        if (Input.GetKeyDown(KeyCode.Z) && _canShoot && isAlive && completedLevel == false && Time.timeScale > 0)
        {
            StartCoroutine(Shoot());
        }

        // Test
        if (StaticClass.debug)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                Instantiate(lagTest, gameObject.transform.position, gameObject.transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (weaponUpgrade == 0)
                {
                    weaponUpgrade = 1;
                }
                else
                {
                    weaponUpgrade = 0;
                }
            }
        }

        // HP
        if (hp <= 0 && isAlive == true && completedLevel == false && Time.timeScale > 0)
        {
            StartCoroutine(Defeat());
        }

        if (transform.position.y < -8)
        {
            hp = 0;
        }

        if (hp > 3)
        {
            hp = 3;
        }

        // Score
        if (StaticClass.score > 999999)
        {
            StaticClass.score = 999999;
        }

        if (StaticClass.score > StaticClass.highScore)
        {
            StaticClass.highScore = StaticClass.score;
        }

        // Quit while paused
        if (Time.timeScale == 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                SceneManager.LoadScene("SpaceTitle");
            }
        }
    }

    void FixedUpdate()
    {
        _jumpTime -= Time.deltaTime;

        if (_jumpTime > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, forceYDefault);

            if (Input.GetKey(KeyCode.X) == false || _hitCeiling == true)
            {
                _jumpTime = 0;
            }
        }
    }

    private IEnumerator Shoot()
    {
        GameObject pr;

        if (weaponUpgrade == 0)
        {
            pr = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation);
            Sound(fire1);
        }
        else
        {
            pr = Instantiate(projectile2, gameObject.transform.position, gameObject.transform.rotation);
            Sound(fire2);
        }

        pr.GetComponent<DestroyOutsideOfCamera>().active = true;

        if (_sr.flipX == true)
        {
            pr.GetComponent<Projectile>().direction = -1;
        }
        else
        {
            pr.GetComponent<Projectile>().direction = 1;
        }

        _animator.SetBool("Shooting", true);
        _canShoot = false;

        yield return new WaitForSeconds(0.4f);

        _animator.SetBool("Shooting", false);

        _canShoot = true;
    }

    private void Sound(AudioClip clip)
    {
        _as.clip = clip;
        _as.Play();
    }

    // Hit enemy projectile.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyShot" && invulnerability == false && completedLevel == false && Time.timeScale > 0)
        {
            StartCoroutine(Damage(1, 2));
            Destroy(collision.gameObject);
        }
    }

    // Hit the enemy itself.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (invulnerability == false && completedLevel == false)
            {
                StartCoroutine(Damage(1, 2));
            }
        }
    }

    // Player took damage.
    private IEnumerator Damage(int amount, int seconds)
    {
        invulnerability = true;
        Sound(damageSound);
        weaponUpgrade = 0;
        hp -= amount;

        for (int i = 0; i < seconds * 20; i++)
        {
            if (isAlive)
            {
                _sr.enabled = !_sr.enabled;
                yield return new WaitForSeconds(0.05f);
            }
            else
            {
                _sr.enabled = false;
                yield return new WaitForSeconds(0.01f);
            }
        }

        if (isAlive)
        {
            _sr.enabled = true;
        }

        invulnerability = false;
    }

    // Ignore collision with GameObject "obj" for X seconds.
    private IEnumerator IgnoreCollision(GameObject obj, int seconds)
    {
        Physics2D.IgnoreCollision(_collider, obj.GetComponent<Collider2D>(), true);

        yield return new WaitForSeconds(seconds);

        if (obj.gameObject != null)
        {
            Physics2D.IgnoreCollision(_collider, obj.GetComponent<Collider2D>(), false);
        }
    }

    // Player dies.
    private IEnumerator Defeat()
    {
        isAlive = false;
        _collider.enabled = false;
        _sr.enabled = false;
        _rb.gravityScale = 0;
        _rb.velocity = new Vector2(0, 0);
        weaponUpgrade = 0;
        _moveX = 0;

        StaticClass.lives--;

        if (transform.position.y >= -8)
        {
            Instantiate(defeatEffect, transform.position, transform.rotation);
        }

        Sound(defeatSound);

        yield return new WaitForSeconds(2.5f);

        if (StaticClass.lives > 0)
        {
            SceneManager.LoadScene(_sceneName);
        }
        else
        {
            SceneManager.LoadScene("SpaceGameOver");
        }
    }
}
