using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject MegumiGo;
    bool isDying = false;
    // Start is called before the first frame update
    [Header("Enemy Stats")]
    float shotCounter;
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Sound Effects")]
    //[SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] AudioClip shootSound;
    [SerializeField][Range(0, 1)] float shootSoundVolume = 0.25f;
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
        Spawn();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime; 
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Spawn()
    {
        Vector2 position = transform.position;

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerBulletTag")
        {
            DameDealer damageDealer = other.gameObject.GetComponent<DameDealer>();
            if (!damageDealer) { return; }
            HitByPlayer(damageDealer);
        }
    }

    private void HitByPlayer(DameDealer damageDealer)
    {
        if (isDying)
        {
            return;
        }

        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDying = true;
        FindObjectOfType<ScoreDisplay>().AddToScore(scoreValue);
        EXPLOSIONNNN();
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        Destroy(gameObject);
    }

    private void EXPLOSIONNNN()
    {
        GameObject explosion = (GameObject)Instantiate(MegumiGo);
        explosion.transform.position = transform.position;
    }
}

