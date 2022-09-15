using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitControl : MonoBehaviour
{
    public GameObject YouWin;
    PlayerControl player;
    // Start is called before the first frame update
    void Start()
    {
        YouWin.SetActive(false);
        player = GameObject.FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        YouWin.SetActive(true);
        Cursor.visible = true;
        player.GetComponent<AudioSource>().enabled = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(2);
    }
    public void ExitMenu()
    {
        SceneManager.LoadScene(1);
    }
}
