using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Bullet, electricBullet, iceBullet, lightning, frost, GameOver, playerParticle;
    public float speed;
    private int dirX;
    private float countRate, fireRate;
    public List<KeyCode> shootKeys;
    BulletControl BulletC;
    public int Life;
    private GameObject particles;
    public enum PlayerStates { NORMAL, ELECTRIC, ICE }
    public PlayerStates states;
    Transform ShootPoint;
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    public Color ElectricColor1, ElectricColor2, IceColor1, IceColor2, DamageColor1, DamageColor2;
    public Text lifes;
    public Animator anim;
    public GameObject poof;

    public List<GameObject> allSounds;
    void Start()
    {
        GameOver.SetActive(false);
        ShootPoint = transform.Find("Face").GetChild(1);
        particles = transform.Find("Particles").gameObject;
        Life = 10;
        BulletC = GetComponent<BulletControl>();
        rb = GetComponent<Rigidbody2D>();
        states = PlayerStates.NORMAL;
        dirX = 1;
        //ActiveIce();
        //ActiveElectric();
        Cursor.visible = false;
    }
    private void Update()
    {
        lifes.text ="x" + Life.ToString();
        switch (states)
        {
            case PlayerStates.NORMAL:
                ShootBullet();
                SetDir();
                break;
            case PlayerStates.ELECTRIC:
                ShootElectricBullet();
                SetDir();
                break;
            case PlayerStates.ICE:
                ShootIceBullet();
                SetDir();
                break;
        }
    }
    void FixedUpdate()
    {
        switch (states)
        {
            case PlayerStates.NORMAL:
                //CAMBIO DE COLOR
                //DESACTIVAR PARTICLES RAYOS
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
                speed = 7; //VELOCIDAD DEL PLAYER
                Movement();

                break;
            case PlayerStates.ELECTRIC:
                
                speed = 10; //VELOCIDAD DEL PLAYER
                Movement();

                break;
            case PlayerStates.ICE:
                speed = 5; //VELOCIDAD DEL PLAYER
                Movement();
                break;
        }
    }

    public void Movement() //MOVIMIENTO DEL PLAYER
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity *= speed;
    }
    private void SetDir() //CAMBIO DE IMAGEN DEL PLAYER HORIZONTAL
    {
        if (shootKeys.Count == 0)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                dirX = (int)Input.GetAxisRaw("Horizontal");
            }
            transform.GetChild(0).transform.localScale = new Vector3(dirX, 1, 1);

        }
        else if (shootKeys.Count > 0)
        {

            if (shootKeys[shootKeys.Count - 1] == KeyCode.RightArrow) dirX = 1;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.LeftArrow) dirX = -1;

            transform.GetChild(0).transform.localScale = new Vector3(dirX, 1, 1);
        }
    }
    public void ActiveElectric()
    {
        states = PlayerStates.ELECTRIC;
        ParticleSystem tempParticles = particles.transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.ColorOverLifetimeModule colorParticles = tempParticles.colorOverLifetime;
        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(ElectricColor1, 0.0f),
                new GradientColorKey(ElectricColor2, 1.0f),
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.5f, 0),
                new GradientAlphaKey(1f, 0.5f),
                new GradientAlphaKey(0.5f, 1f),
            });
        colorParticles.color = new ParticleSystem.MinMaxGradient(gradient);

        ParticleSystem tempParticles1 = particles.transform.GetChild(1).GetComponent<ParticleSystem>();
        ParticleSystem.ColorOverLifetimeModule colorParticles1 = tempParticles1.colorOverLifetime;
        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(ElectricColor1, 0.0f),
                new GradientColorKey(ElectricColor2, 1.0f),
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.5f, 0),
                new GradientAlphaKey(1f, 0.5f),
                new GradientAlphaKey(0.5f, 1f),
            });
        colorParticles1.color = new ParticleSystem.MinMaxGradient(gradient);
        lightning.SetActive(true);
        //GameObject newPoof = Instantiate(poof);

    }
    public void ActiveIce()
    {
        states = PlayerStates.ICE;
        ParticleSystem tempParticles = particles.transform.GetChild(0).GetComponent<ParticleSystem>();
        ParticleSystem.ColorOverLifetimeModule colorParticles = tempParticles.colorOverLifetime;
        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(IceColor1, 0.0f),
                new GradientColorKey(IceColor2, 1.0f),
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.5f, 0),
                new GradientAlphaKey(1f, 0.5f),
                new GradientAlphaKey(0.5f, 1f),
            });
        colorParticles.color = new ParticleSystem.MinMaxGradient(gradient);

        ParticleSystem tempParticles1 = particles.transform.GetChild(1).GetComponent<ParticleSystem>();
        ParticleSystem.ColorOverLifetimeModule colorParticles1 = tempParticles1.colorOverLifetime;
        gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(IceColor1, 0.0f),
                new GradientColorKey(IceColor2, 1.0f),
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.5f, 0),
                new GradientAlphaKey(1f, 0.5f),
                new GradientAlphaKey(0.5f, 1f),
            });
        colorParticles1.color = new ParticleSystem.MinMaxGradient(gradient);
        frost.SetActive(true);
        //GameObject newPoof = Instantiate(poof);

    }
    
    public void ShootBullet()
    {
        fireRate = 0.5f;
        countRate += Time.deltaTime;
        if (countRate >= fireRate && shootKeys.Count > 0)
        {
           
            countRate = 0;
            
            Vector3 newPos = ShootPoint.position;
            newPos.z = -2;
            GameObject newBullet = Instantiate(Bullet, newPos, Quaternion.identity);
            BulletControl.direction finalDir = BulletControl.direction.RIGHT;
            if (shootKeys[shootKeys.Count - 1] == KeyCode.RightArrow) finalDir = BulletControl.direction.RIGHT;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.LeftArrow) finalDir = BulletControl.direction.LEFT;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.UpArrow) finalDir = BulletControl.direction.UP;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.DownArrow) finalDir = BulletControl.direction.DOWN;

            newBullet.GetComponent<BulletControl>().SetBullet(finalDir, "KillEnemies", 8f);
            anim.SetTrigger("Shoot");
            Destroy(newBullet, 2);
            GameObject newSound = Instantiate(allSounds[2]);
            Destroy(newSound, 2);

        }
    }
    public void ShootElectricBullet()
    {
        fireRate = 0.1f;
        countRate += Time.deltaTime;
        
        if (countRate >= fireRate && shootKeys.Count > 0)
        {
            //GameObject newSound = Instantiate(allSounds[2]); // sonido para que dispare
           // Destroy(newSound, 3);
            

            countRate = 0;

            Vector3 newPos = ShootPoint.position;
            newPos.z = -2;
            GameObject newBullet = Instantiate(electricBullet, newPos, Quaternion.identity);
            BulletControl.direction finalDir = BulletControl.direction.RIGHT;
            if (shootKeys[shootKeys.Count - 1] == KeyCode.RightArrow) finalDir = BulletControl.direction.RIGHT;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.LeftArrow) finalDir = BulletControl.direction.LEFT;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.UpArrow) finalDir = BulletControl.direction.UP;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.DownArrow) finalDir = BulletControl.direction.DOWN;

            newBullet.GetComponent<BulletControl>().SetBullet(finalDir, "KillEnemies", 11f);
            anim.SetTrigger("Shoot");
            Destroy(newBullet, 0.5f);
            GameObject newSound = Instantiate(allSounds[1]);
            Destroy(newSound, 2);
        }
    }
    public void ShootIceBullet()
    {
        fireRate = 2f;
        countRate += Time.deltaTime;

        if (countRate >= fireRate && shootKeys.Count > 0)
        {
            //GameObject newSound = Instantiate(allSounds[2]); // sonido para que dispare
            // Destroy(newSound, 3);

            countRate = 0;

            Vector3 newPos = ShootPoint.position;
            newPos.z = -2;
            GameObject newBullet = Instantiate(iceBullet, newPos, Quaternion.identity);
            BulletControl.direction finalDir = BulletControl.direction.RIGHT;
            if (shootKeys[shootKeys.Count - 1] == KeyCode.RightArrow) finalDir = BulletControl.direction.RIGHT;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.LeftArrow) finalDir = BulletControl.direction.LEFT;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.UpArrow) finalDir = BulletControl.direction.UP;
            else if (shootKeys[shootKeys.Count - 1] == KeyCode.DownArrow) finalDir = BulletControl.direction.DOWN;

            newBullet.GetComponent<BulletControl>().SetBullet(finalDir, "KillEnemies", 20f);
            anim.SetTrigger("Shoot");
            Destroy(newBullet, 3f);
            GameObject newSound = Instantiate(allSounds[0]);
            Destroy(newSound, 2);
        }
    }
    private bool HasKey(KeyCode _key)
    {
        for (int i = 0; i < shootKeys.Count; i++)
        {
            if (shootKeys[i] == _key)
            {
                return true;
            }
        }
        if (_key != KeyCode.RightArrow && _key != KeyCode.LeftArrow && _key != KeyCode.UpArrow && _key != KeyCode.DownArrow)
            return true;

        return false;
    }
    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Event e = Event.current;
            if (HasKey(e.keyCode) == false)
            {
                shootKeys.Add(e.keyCode);
            }

        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            Event e = Event.current;
            shootKeys.Remove(e.keyCode);
        }

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet")
        {
            anim.SetTrigger("Damage"); //DAÑO MOSCA
            if (states == PlayerStates.NORMAL)
            {
                Instantiate(playerParticle, transform.position, Quaternion.identity);
                Life -= 1;
                
            }
            if (states == PlayerStates.ELECTRIC)
            {
                Instantiate(playerParticle, transform.position, Quaternion.identity);
                Life -= 1;
                
            }
            if (states == PlayerStates.ICE)
            {
                Instantiate(playerParticle, transform.position, Quaternion.identity);
                Life -= 1;
                
            }
        }
        if (other.tag == "WormBullet")
        {
            anim.SetTrigger("Damage"); //DAÑO WORM 
            if (states == PlayerStates.NORMAL)
            {
                Instantiate(playerParticle, transform.position, Quaternion.identity);
                Life -= 1;
                
            }
            if (states == PlayerStates.ELECTRIC)
            {
                Instantiate(playerParticle, transform.position, Quaternion.identity);
                Life -= 1;
            }
            if (states == PlayerStates.ICE)
            {
                Instantiate(playerParticle, transform.position, Quaternion.identity);
                Life -= 1;
                
            }
        }
        if (Life <= 0)
        {
            Life = 0;
            //MUERTE
            GameOver.SetActive(true);
            Time.timeScale = 0;
            //GAME OVER
            Cursor.visible = true;
        }
    }
}
