using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public bool isPaused;
    public GameObject Pause;
    void Start()
    {
        Pause.SetActive(false);
        isPaused = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        isPaused = !isPaused; // esto es un switch para invertir la acción
        Time.timeScale = isPaused ? 0 : 1;
        
        Pause.SetActive(isPaused);//ActivarCanvas
        Cursor.visible = isPaused;
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
    public void GoMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

}
