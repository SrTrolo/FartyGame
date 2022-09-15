using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public enum RoomType { NORMAL, SPECIAL }
    public RoomType type;
    private GameObject RDoor, LDoor, TDoor, Rack;
    public Transform Spawner;
    public Transform cameraPos;
    public float cameraSize = 5;
    public Animator anim;


    public List<Animator> doorAnims;

    private void Awake()
    {
        Spawner = transform.Find("Spawner");

    }
    void Start()
    {
        
        LDoor = transform.Find("PuertaL").gameObject;
        RDoor = transform.Find("PuertaR").gameObject;

        
        if (type == RoomType.SPECIAL)
        {
            TDoor = transform.Find("PuertaT").gameObject;
            Rack = TDoor.transform.GetChild(2).gameObject;
        }
        DesactiveEnemies();
        CheckEnemies();
        
    }


    void Update()
    {

        switch (type)
        {

            case RoomType.NORMAL:

                break;
            case RoomType.SPECIAL:
                SpecialRoomUpdate();
                break;
        }


        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    DemoDestroyEnemies();
        //}
    }
    private void SpecialRoomUpdate()
    {

    }
    private void DemoDestroyEnemies()
    {
        for (int i = Spawner.childCount - 1; i >= 0; i--)
        {
            Destroy(Spawner.GetChild(i).gameObject);
        }
        RDoor.GetComponent<BoxCollider2D>().isTrigger = true;
        LDoor.GetComponent<BoxCollider2D>().isTrigger = true;
        if (type == RoomType.SPECIAL)
        {
            TDoor.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        DelayCheckEnemies();
    }

    private void DesactiveEnemies()
    {
        if (Spawner.childCount > 0 && Spawner != null)
        {
            for (int i = 0; i < Spawner.childCount; i++)
            {
                Spawner.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    private void Spawn()
    {

        if (Spawner.childCount > 0 && Spawner != null)
        {
            for (int i = 0; i < Spawner.childCount; i++)
            {
                Spawner.GetChild(i).gameObject.SetActive(true);
            }
        }



    }

    public void DelayCheckEnemies()
    {
        Invoke("CheckEnemies", 0.1f); ////DELAYY DE LOS ENEMIGOS
    }

    public void CheckEnemies() //PARA DESTROY ENEMIES
    {
        if (Spawner != null)
        {
            if (Spawner.childCount > 0)
            {
                //print(Spawner.childCount);
            }
            else
            {
                for (int i = 0; i < doorAnims.Count; i++)
                {
                    if (doorAnims[i].tag == "DragDoor")
                    {
                        doorAnims[i].GetComponent<Rigidbody2D>().isKinematic = false;
                        BoxCollider2D[] doors = doorAnims[i].GetComponents<BoxCollider2D>();
                        for (int j = 0; j < doors.Length; j++)
                        {
                            doors[j].enabled = true;
                        }
                    }
                    else
                    {
                        doorAnims[i].enabled = true;
                        doorAnims[i].transform.parent.GetComponent<BoxCollider2D>().isTrigger = true;
                        if (type == RoomType.SPECIAL)
                        {
                            TDoor.GetComponent<BoxCollider2D>().isTrigger = true;
                        }
                    }
                }



                //RDoor.GetComponent<BoxCollider2D>().isTrigger = true;
                //LDoor.GetComponent<BoxCollider2D>().isTrigger = true;
                //if (type == RoomType.SPECIAL)
                //{

                //    TDoor.GetComponent<BoxCollider2D>().isTrigger = true;
                //    //Rack.GetComponent<Rigidbody2D>().simulated = true;
                //    //Rack.GetComponent<BoxCollider2D>().enabled = true;
                //}
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Vector3 InitPos = cameraPos.position;
            InitPos.z = -10;
            Camera.main.GetComponent<CameraControl>().MoveCamera(InitPos, cameraSize);
            //CAMBIO DE CAMARA DA ERROR
            Spawn();
        }

    }


}
