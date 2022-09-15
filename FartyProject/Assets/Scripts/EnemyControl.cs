using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private float speed;
    public enum EnemyStates { Fly, Worm, Smoke }
    public EnemyStates state;
    public GameObject flyBullet, wormBullet, enemyParticle;
    PlayerControl player;
    public RoomControl room;
    WorldControl world;
    private Vector2 playerAxis;
    private Rigidbody2D rb;
    private float fireRate, countRate, TpRate, WormRate;

    Animator anim;
    public float life;
    private float damage;

    public List<GameObject> allSounds;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (state == EnemyStates.Fly)
        {
            speed = 1;
            life = 7;
        }
        if (state == EnemyStates.Worm)
        {
            playerAxis = transform.position;
            life = 10;

        }
        fireRate = Random.Range(1, 1.5f);
        damage = Random.Range(0.1f, 1.5f); //random damage
        player = GameObject.FindObjectOfType<PlayerControl>();
        rb = GetComponent<Rigidbody2D>();
        TpRate = Random.Range(2.7f, 3.2f);
        world = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldControl>();
    }


    void Update()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().enabled = !GameManager.isPaused;
        }

        switch (state)
        {
            case EnemyStates.Fly:

                FlyUpdate();
                FlyShoot();
                break;
            case EnemyStates.Worm:

                WormUpdate();
                break;
            case EnemyStates.Smoke:

                break;
            default:
                break;
        }
    }
    public void FlyUpdate()
    {
        playerAxis = (player.transform.position - transform.position);
        float angle = Mathf.Atan2(playerAxis.y, playerAxis.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        playerAxis = new Vector2(player.transform.position.x, player.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, playerAxis, speed * Time.deltaTime);
    }
    public void FlyShoot()
    {
        fireRate = Random.Range(1, 1.5f);
        countRate += Time.deltaTime;
        if (countRate >= fireRate)
        {
            countRate = 0;
            Instantiate(flyBullet, transform.position, Quaternion.identity);
            fireRate = Random.Range(1, 1.5f); //---------------------------------------------------RESTABLECER ENEMIGOS 
            speed = 1;
            GameObject newSound = Instantiate(allSounds[0]);
            Destroy(newSound, 2);
        }
    }
    public void WormUpdate()
    {
        WormRate += Time.deltaTime;

        if (WormRate >= TpRate)
        {
            WormRate = 0;
            TpRate = Random.Range(2.7f, 3.2f);


            //ANIMACION DE GUSANO APARECIENDO
            playerAxis = new Vector2(player.transform.position.x, player.transform.position.y);

            Invoke("WormShoot", 1);

        }

    }
    public void TpWorm()
    {
        anim.SetBool("Shoot", false);
        transform.position = playerAxis;


    }
    public void WormShoot()
    {
        Invoke("TpWorm", 1);
        anim.SetBool("Shoot", true);
        Instantiate(wormBullet, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        GameObject newSound = Instantiate(allSounds[0]);
        Destroy(newSound, 2);
    }
    public void RandomDamage() //---------------------------------DAÑO NORMAL
    {

        damage = Random.Range(0.5f, 1.5f);
        Instantiate(enemyParticle, transform.position, Quaternion.identity);
        life -= damage;
        if (life <= 0)
        {
            
            room.DelayCheckEnemies();
            Destroy(gameObject);

        }
    }
    public void RandomElectricDamage() //-------------------------DAÑO ELECTRICO
    {

        damage = Random.Range(0.3f, 0.7f);
        life -= damage;
        Instantiate(enemyParticle, transform.position, Quaternion.identity);
        if (life <= 0)
        {

            room.DelayCheckEnemies();
            Destroy(gameObject);
        }
    }
    public void RandomIceDamage() //-------------------------DAÑO ELECTRICO
    {
        damage = Random.Range(5, 7);
        life -= damage;
        Instantiate(enemyParticle, transform.position, Quaternion.identity);
        if (life <= 0)
        {
            room.DelayCheckEnemies();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillEnemies")
        {
            if (player.states == PlayerControl.PlayerStates.NORMAL)
            {
                RandomDamage();
                Destroy(other.gameObject);
            }
            if (player.states == PlayerControl.PlayerStates.ELECTRIC)
            {
                RandomElectricDamage(); //-------------------------------------------PARALIZAR ENEMIGOS
                fireRate = Random.Range(0.75f, 1f);
                speed = 0;
                Destroy(other.gameObject);
            }
            if (player.states == PlayerControl.PlayerStates.ICE)
            {
                RandomIceDamage();
            }


        }
    }
}
