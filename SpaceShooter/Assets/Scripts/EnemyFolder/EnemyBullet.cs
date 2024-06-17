using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (!IsInScreen())
        {
            Destroy(gameObject);
        }
    }

    bool IsInScreen()
    {
        Vector2 position = Camera.main.WorldToViewportPoint(transform.position);
        return position.x >= 0 && position.x <= 1 && position.y >= 0 && position.y <= 1;
    }
}
