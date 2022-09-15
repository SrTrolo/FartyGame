using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public enum DoorType { RIGHT, LEFT, UP}
    public DoorType direction;
    PlayerControl player;
    WorldControl manager;
    public Transform teleporPoint;

    void Start()
    {

    }

    
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            
            other.transform.position = teleporPoint.position;
            
            
        }
    }
}
