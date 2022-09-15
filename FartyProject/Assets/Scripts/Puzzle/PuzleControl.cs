using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Stracker.ListSystem;
using UnityEngine.EventSystems;

public class PuzleControl : MonoBehaviour
{
    public GameObject cables, code;

    private GameObject currentPuzzle;

    public List<CableProperties> originalCables, randomCables = new List<CableProperties>();
    public GameObject baseCable;
    public Transform leftGridCable, rightGridCable;

    private CableProperties currentCable = new CableProperties();
    private bool pressed;

    public PlayerControl player;
    public Sprite rightSprite, leftSprite;

    void Start()
    {
        cables.SetActive(false);
        code.SetActive(false);
    }
    
    public void ActiveCodePuzzle(GameObject _puzzle)
    {
        currentPuzzle = _puzzle;
        code.SetActive(true);
    }
    public void DesactiveCodePuzzle()
    {
        code.SetActive(false);
    }
    public void ConfirmCodePuzzle()
    {
        InputField newField = code.transform.Find("InputField").GetComponent<InputField>();
        string Code = currentPuzzle.GetComponent<PaswordControl>().code;
        if(newField.text==Code)
        {
            currentPuzzle.GetComponent<BoxCollider2D>().enabled = false;
            //Time.timeScale = 1;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = true;
            //PlayerControl TempPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
            //TempPlayer.enabled = true;
            player.ActiveIce();
            DesactiveCodePuzzle();
        }
    }
    public void ActiveCablesPuzle(GameObject _puzle)
    {
        currentPuzzle = _puzle;
        originalCables = currentPuzzle.GetComponent<CableControl>().cables;
        randomCables = currentPuzzle.GetComponent<CableControl>().CloneCable();
        randomCables = ListSystem.Shuffle(randomCables);
        PrintCables();
        cables.SetActive(true);
    }
    private void PrintCables()
    {
        for (int i = leftGridCable.childCount - 1; i >= 0; i--)
        {
            Destroy(leftGridCable.GetChild(i).gameObject);
            Destroy(rightGridCable.GetChild(i).gameObject);
        }
        for (int i = 0; i < originalCables.Count; i++)
        {
            //lista de imagenes de cable y hacer random en la imagen;
            //LEFT CABLE ---------------------------------------------------
            GameObject leftCable = Instantiate(baseCable, leftGridCable);
            leftCable.GetComponent<Image>().sprite = rightSprite;
            leftCable.GetComponent<Image>().color = originalCables[i].color;
            originalCables[i].cable = leftCable;
            originalCables[i].line = leftCable.GetComponent<LineRenderer>();
            originalCables[i].line.material.color = originalCables[i].color;

            EventTrigger leftTrigger = leftCable.GetComponent<EventTrigger>();
            EventTrigger.Entry pointerDown = new EventTrigger.Entry();
            pointerDown.eventID = EventTriggerType.PointerDown;
            CableProperties newCable = originalCables[i];
            pointerDown.callback.AddListener(delegate { PressCable(newCable); });
            leftTrigger.triggers.Add(pointerDown);


            //RIGHT CABLE -----------------------------------------------
            //ROTAR IMAGEN CABLE
            GameObject rightCable = Instantiate(baseCable, rightGridCable);
            rightCable.GetComponent<Image>().sprite = leftSprite;
            rightCable.GetComponent<Image>().color = randomCables[i].color;
            randomCables[i].cable = rightCable;
            rightCable.GetComponent<LineRenderer>().enabled = false;

            EventTrigger rightTrigger = rightCable.GetComponent<EventTrigger>();
            EventTrigger.Entry pointerDrop = new EventTrigger.Entry();
            pointerDrop.eventID = EventTriggerType.Drop;
            CableProperties newDropCable = randomCables[i];
            pointerDrop.callback.AddListener(delegate { DropCable(newDropCable); });
            rightTrigger.triggers.Add(pointerDrop);
        }
    }
    private void PressCable(CableProperties _cable)
    {
        if(_cable.actived==false)
        {
            currentCable = _cable;
            pressed = true;
        }
        
    }
    private void DropCable(CableProperties _cable)
    {
        if(pressed == true)
        {
            pressed = false;
            if(_cable.color == currentCable.color)
            {
                currentCable.actived = true;
                _cable.actived = true;
                CheckCables();
            }
            else
            {
                Vector3 initPos = currentCable.cable.transform.position;
                initPos.z = -6;
                currentCable.line.SetPosition(1, initPos);

            }
        }
    }
    public void DesactiveCablePuzle()
    {
        cables.SetActive(false);
    }

    void Update()
    {
        if(pressed==true && currentCable.cable != null)
        {
            Vector3 initPos = currentCable.cable.transform.position;
            initPos.z = -6;
            Vector3 finalPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            finalPos.z = -6;
            currentCable.line.SetPosition(0, initPos);
            currentCable.line.SetPosition(1, finalPos);

            if(Input.GetMouseButtonUp(0))
            {
                currentCable.line.SetPosition(1, initPos);
                currentCable = new CableProperties();
            }
        }

    }
    private void CheckCables()
    {
        for (int i = 0; i < originalCables.Count; i++)
        {
            if(originalCables[i].actived ==false)
            {
                return;
            }
        }
        //transform.Find("ELECTRIC(Clone)").GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        DesactiveCablePuzle();
        currentPuzzle.GetComponent<BoxCollider2D>().enabled = false;
        currentPuzzle.GetComponent<CableControl>().EndPuzzle();
        
        player.ActiveElectric();

    }
}
