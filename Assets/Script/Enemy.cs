using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float speed;
    public int enemyScore;
    public int health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject player;

    public int score;

    SpriteRenderer spriteRenderer;

    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {

        Fire();
        Reload();
    }

    public void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if(health <= 0) {
            Player1 playerLogic = player.GetComponent<Player1>();
            playerLogic.score += enemyScore;
            Destroy(gameObject);
        }
    }

    public void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;

        if(enemyName == "S") {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
        }
        else if (enemyName == "L") {
            GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right*0.3f, transform.rotation);
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right*0.3f);
            rigidR.AddForce(dirVecR.normalized * 10, ForceMode2D.Impulse);

            GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left*0.3f, transform.rotation);
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left*0.3f);
            rigidL.AddForce(dirVecL.normalized * 10, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }

    public void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    public void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorberBullet") {
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "PlayerBullet") {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);
            Destroy(collision.gameObject);
        } 
    }
}