using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject MegumiGo;
    bool isDying = false;
    [Header("Asteroids Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;
    [SerializeField] float speed = 2f;

    [Header("Sound Effects")]
    //[SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0, 1)] float deathSoundVolume = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        Vector2 pos = transform.position;

        pos = new Vector2(pos.x, pos.y - speed * Time.deltaTime);

        transform.position = pos;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
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
