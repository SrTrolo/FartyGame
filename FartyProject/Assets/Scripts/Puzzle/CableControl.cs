using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableControl : MonoBehaviour
{
    public PuzleControl manager;
    private bool playerEnter;
    public List<CableProperties> cables;
    public GameObject Lights, electric;


    private void Start()
    {
        Cursor.visible = false;
        manager = GameObject.FindGameObjectWithTag("PuzzleManager").GetComponent<PuzleControl>();
    }
    public List<CableProperties> CloneCable()
    {
        List<CableProperties> newList = new List<CableProperties>();
        for (int i = 0; i < cables.Count; i++)
        {
            CableProperties newCable = new CableProperties()
            {
                color = cables[i].color,

            };
            newList.Add(newCable);
        }
        return newList;
    }

    void Update()
    {
        if (playerEnter == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (manager.cables.activeSelf == true)
                {
                    Cursor.visible = false;
                    Time.timeScale = 1;
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = true;
                    manager.DesactiveCablePuzle();
                }
                else
                {
                    Cursor.visible = true;
                    Time.timeScale = 0;
                    //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = false;
                    manager.ActiveCablesPuzle(gameObject);

                }
            }

        }
    }

    public void EndPuzzle()
    {
        transform.root.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        Lights.SetActive(true);
        electric.SetActive(false);
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
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = true;
            playerEnter = false;
            manager.DesactiveCablePuzle();
            Cursor.visible = false;
        }
    }

}
[System.Serializable]
public class CableProperties
{
    public Color color;
    [HideInInspector] public bool actived;
    [HideInInspector] public GameObject cable;
    [HideInInspector] public LineRenderer line;
}