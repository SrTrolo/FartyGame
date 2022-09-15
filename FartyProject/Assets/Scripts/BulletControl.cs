using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public enum direction { RIGHT, LEFT, UP, DOWN };
    private direction dir;
    PlayerControl player;
    private float speed;

    
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerControl>();
    }

    public void SetBullet(direction _dir, string _tag, float _Speed)
    {
        speed = _Speed;
        dir = _dir;
        gameObject.tag = _tag;
        //gameObject.layer = _layer;

    }
    
    void Update()
    {        
        switch (dir)
        {
            case direction.RIGHT:
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                break;
            case direction.LEFT:
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                break;
            case direction.UP:
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                break;
            case direction.DOWN:
                transform.Translate(Vector2.down * speed * Time.deltaTime);
                break;

        }
    }
    public void SetDir(direction _dir)
    {
        dir = _dir;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(player.states == PlayerControl.PlayerStates.NORMAL || player.states == PlayerControl.PlayerStates.ELECTRIC)
        {
            if (other.tag == "BG" || other.tag == "EnemyBullet")
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.tag == "BG")
            {
                Destroy(gameObject);
            }
        }
        
        

    }
}
