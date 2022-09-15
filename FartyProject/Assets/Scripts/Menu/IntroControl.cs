using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroControl : MonoBehaviour
{
    float IntroTime;
    void Start()
    {
        
    }

    
    void Update()
    {
        IntroTime += Time.deltaTime;
        if(IntroTime >= 60f)
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
    }
    public void SkipIntro()
    {
        SceneManager.LoadScene(1);
    }
}
