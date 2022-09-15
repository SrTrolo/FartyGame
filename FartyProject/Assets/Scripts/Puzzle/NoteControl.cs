using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NoteControl : MonoBehaviour
{
    bool playerEnter;
    public GameObject Note;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        Note.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEnter == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Note.activeSelf == true)
                {
                    Time.timeScale = 1;

                    Note.SetActive(false);
                }
                else
                {
                    Note.SetActive(true);
                    Time.timeScale = 0;

                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Time.timeScale = 1;
            playerEnter = false;
            Note.SetActive(false);

        }
    }
}

