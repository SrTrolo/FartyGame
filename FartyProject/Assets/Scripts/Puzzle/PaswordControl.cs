using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaswordControl : MonoBehaviour
{
    public PuzleControl manager;
    private bool playerEnter;
    public string code;

    private void Start()
    {
        Cursor.visible = false;
        manager = GameObject.FindGameObjectWithTag("PuzzleManager").GetComponent<PuzleControl>();
    }


    void Update()
    {
        if(playerEnter==true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(manager.code.activeSelf==true)
                {
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = true;
                    manager.DesactiveCodePuzzle();
                }
                else
                {
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = false;
                    manager.ActiveCodePuzzle(gameObject);

                }
            }
                
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Cursor.visible = false;
            Time.timeScale = 1;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = true;
            playerEnter = false;
            manager.DesactiveCodePuzzle();
        }
    }

}
