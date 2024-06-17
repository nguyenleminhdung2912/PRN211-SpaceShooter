using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] float bulletSpeed = 8;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BulletMovement();
    }

    private void BulletMovement()
    {
        Vector2 position = transform.position;

        position = new Vector2 (position.x, position.y + bulletSpeed * Time.deltaTime);

        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2(1,1));

        if(transform.position.y > max.y)
        {
            Destroy (gameObject);
        }
    }
}
