using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speedX;
    float moveX;

    public float jumpTimeDefault;
    float jumpTime;
    public float forceYDefault;
    public AudioClip jumpSound;
    public AudioClip damageSound;
    public AudioClip defeatSound;
    public GameObject defeatEffect;

    public static int weaponUpgrade = 0;
    public static int hp = 3;
    public static bool invulnerability = false;
    public static bool isAlive = true;
    public static bool completedLevel = false;

    Rigidbody2D _rb;
    Collider2D _collider;
    SpriteRenderer _sr;
    Animator _animator;
    AudioSource _as;

    Scene scene;
    string sceneName;

    public LayerMask solidMask;

    public bool isOnGround;
    RaycastHit2D rayHit;

    bool wallLeft, wallRight;
    bool hitCeiling = false;

    public GameObject projectile;
    public GameObject lagTest;
    bool canShoot = true;

    public AudioClip fire1;
    public GameObject projectile2;
    public AudioClip fire2;

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
        canShoot = true;
        completedLevel = false;
        hitCeiling = false;
        isAlive = true;
        hp = 3;

        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

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

        if(isAlive == true && completedLevel == false && Time.timeScale > 0)
        {
            moveX = Input.GetAxisRaw("Horizontal") * speedX * Time.deltaTime;
        }
        else
        {
            moveX = 0;
            _animator.SetBool("MovingX", false);
        }

        rayHit = Physics2D.BoxCast(transform.position, new Vector2(_collider.bounds.size.x * 0.52f, _collider.bounds.size.y * 0.925f), 0, transform.right, 0.4f, solidMask);

        if(rayHit.collider != null)
        {
            wallRight = true;
        }
        else
        {
            wallRight = false;
        }

        rayHit = Physics2D.BoxCast(transform.position, new Vector2(_collider.bounds.size.x * 0.52f, _collider.bounds.size.y * 0.925f), 0, -transform.right, 0.4f, solidMask);

        if (rayHit.collider != null)
        {
            wallLeft = true;
        }
        else
        {
            wallLeft = false;
        }

        if (((moveX > 0 && wallRight == false) || (moveX < 0 && wallLeft == false)) && isAlive && completedLevel == false)
        {
            transform.Translate(moveX, 0, 0);
        }

        if (moveX > 0)
        {
            _sr.flipX = false;
            _animator.SetBool("MovingX", true);
        }
        else if(moveX == 0)
        {
            _animator.SetBool("MovingX", false);
        }
        else if(moveX < 0)
        {
            _sr.flipX = true;
            _animator.SetBool("MovingX", true);
        }

        // Jump

        rayHit = Physics2D.CircleCast(transform.position, 0.36f, -transform.up, _collider.bounds.size.y * 0.65f, solidMask);

        if(rayHit.collider != null)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if(Input.GetKeyDown(KeyCode.X) && isOnGround && isAlive && completedLevel == false && hitCeiling == false && Time.timeScale > 0)
        {
            jumpTime = jumpTimeDefault;
            Sound(jumpSound);
        }

        _animator.SetBool("OnGround", isOnGround);

        // Hit ceiling

        rayHit = Physics2D.CircleCast(transform.position, 0.365f, transform.up, _collider.bounds.size.y * 0.33f, solidMask);

        if(rayHit.collider != null)
        {
            hitCeiling = true;

            //_rb.velocity = new Vector2(_rb.velocity.x, Mathf.Abs(_rb.velocity.y) * -1f);
            _rb.velocity = new Vector2(_rb.velocity.x, -6f);
        }
        else if(Input.GetKey(KeyCode.X) == false)
        {
            hitCeiling = false;
        }

        // Shoot

        if (Input.GetKeyDown(KeyCode.Z) && canShoot && isAlive && completedLevel == false && Time.timeScale > 0)
        {
            StartCoroutine(Shoot());
        }

        // Test

        if(Input.GetKeyDown(KeyCode.G) && StaticClass.debug == true)
        {
            Instantiate(lagTest, gameObject.transform.position, gameObject.transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.H) && StaticClass.debug == true)
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

        // HP

        if (hp <= 0 && isAlive == true && completedLevel == false && Time.timeScale > 0)
        {
            StartCoroutine(Defeat());
        }

        if(transform.position.y < -8)
        {
            hp = 0;
        }

        if(hp > 3)
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

        // Quit

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SpaceTitle");
        }
    }

    void FixedUpdate()
    {
        jumpTime -= Time.deltaTime;

        if(jumpTime > 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, forceYDefault);

            if(Input.GetKey(KeyCode.X) == false || hitCeiling == true)
            {
                jumpTime = 0;
            }
        }
    }

    IEnumerator Shoot()
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

        if(_sr.flipX == true)
        {
            pr.GetComponent<Projectile>().direction = -1;
        }
        else
        {
            pr.GetComponent<Projectile>().direction = 1;
        }

        _animator.SetBool("Shooting", true);
        canShoot = false;

        yield return new WaitForSeconds(0.4f);

        _animator.SetBool("Shooting", false);

        canShoot = true;
    }

    void Sound(AudioClip clip)
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
    IEnumerator Damage(int amount, int seconds)
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
    IEnumerator IgnoreCollision(GameObject obj, int seconds)
    {
        Physics2D.IgnoreCollision(_collider, obj.GetComponent<Collider2D>(), true);

        yield return new WaitForSeconds(seconds);

        if (obj.gameObject != null)
        {
            Physics2D.IgnoreCollision(_collider, obj.GetComponent<Collider2D>(), false);
        }
    }

    // Player dies.
    IEnumerator Defeat()
    {
        isAlive = false;
        _collider.enabled = false;
        _sr.enabled = false;
        _rb.gravityScale = 0;
        _rb.velocity = new Vector2(0, 0);
        weaponUpgrade = 0;
        moveX = 0;

        StaticClass.lives--;

        if (transform.position.y >= -8)
        {
            Instantiate(defeatEffect, transform.position, transform.rotation);
        }

        Sound(defeatSound);

        yield return new WaitForSeconds(2.5f);

        if(StaticClass.lives > 0)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene("SpaceGameOver");
        }
    }
}
