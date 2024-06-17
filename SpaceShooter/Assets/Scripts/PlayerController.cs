using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PLayerController : MonoBehaviour
{

    public TextMeshProUGUI LivesText;
    [Header("GameObject")]
    public GameObject PlayerBullet;
    public GameObject TempBullet01;
    public GameObject TempBullet02;
    public GameObject MegumiGo;
    public GameObject GameManagerGo;
    const int MaxLives = 3;
    int lives;
    [Header("Player Stats")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.025f;
    [SerializeField] int health = 200;
    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0, 1)] float shootSoundVolume = 0.25f;
    [Header("Projectile")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    Coroutine firingCoroutine;
    Transform[] TempBullets = new Transform[2];
    public int GetHealth()
    {
        return health;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        TempBullets[0] = TempBullet01.transform;
        TempBullets[1] = TempBullet02.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;


        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Fire()
    {
        if(Input.GetKeyDown("space"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetKeyUp("space"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            for (int i = 0; i < TempBullets.Length; i++)
            {
                GameObject bullet = Instantiate(PlayerBullet, TempBullets[i].position, Quaternion.identity) as GameObject;
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            }
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyBulletTag" || other.gameObject.tag == "EnemyShipTag" || other.gameObject.tag == "AsteroidsTag")
        {
            DameDealer damageDealer = other.gameObject.GetComponent<DameDealer>();
            if (!damageDealer) { return; }
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DameDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        EXPLOSIONNNN();
        lives--;
        LivesText.text = lives.ToString();
        if (lives == 0)
        {
            GameManagerGo.GetComponent<GameManagerController>().SetGameManagerState(GameManagerController.GameManagerState.GameOver);
            FindObjectOfType<Level>().LoadGameOver();
            Destroy(gameObject);
        }
        else
        {
            health = 200; 
        }
    }


    private void EXPLOSIONNNN()
    {
        GameObject explosion = (GameObject) Instantiate(MegumiGo);
        explosion.transform.position = transform.position;
    }

    public void Init()
    {
        lives = MaxLives;
        LivesText.text = lives.ToString();
        transform.position = new Vector2(0, 0);
        gameObject.SetActive(true);
    }
}
