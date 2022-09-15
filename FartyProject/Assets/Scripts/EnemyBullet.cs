using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public enum BulletType { FlyBullet, WormBullet}
    public BulletType bullet;
    private float damage;
    Rigidbody2D rb;
    PlayerControl player;
    float rotation;
    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerControl>();
        rb = GetComponent<Rigidbody2D>();
        if (bullet == BulletType.FlyBullet)
        {
            moveDirection = (player.transform.position - transform.position);
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            moveDirection = (player.transform.position - transform.position).normalized * 7;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            Destroy(gameObject, 0.35f);
        }
        if (bullet == BulletType.WormBullet)
        {
            rotation = Random.Range(0, 360);
            
            //moveDirection = (player.transform.position - (transform.position)).normalized * 0;
            //rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
            Destroy(gameObject, 6f);

        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (bullet)
        {
            case BulletType.FlyBullet:
                FlyBulletUpdate();
                break;
            case BulletType.WormBullet:
                WormBulletUpdate();
                break;
            default:
                break;
        }
    }
    public void FlyBulletUpdate()
    {
        
    }
    public void WormBulletUpdate()
    {

    }
    
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {

            if (bullet == BulletType.FlyBullet)
            {
                Destroy(gameObject);
            }
            if(bullet == BulletType.WormBullet)
            {

            } 
        }
        if(other.tag == "BG" || other.tag == "KillEnemies")
        {           
            if (bullet == BulletType.FlyBullet)
            {
                if(player.states== PlayerControl.PlayerStates.NORMAL)
                {
                    Destroy(gameObject);
                }
                if (player.states == PlayerControl.PlayerStates.ICE)
                {
                    Destroy(gameObject);
                }
                else
                {

                }
                
            }
            if (bullet == BulletType.WormBullet)
            {

            }
        }
    }
}
