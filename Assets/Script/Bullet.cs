using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggertEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BorderBullet") {
            Destroy(gameObject);
            
        }
        Debug.Log(collision.gameObject.tag);
    }
}
