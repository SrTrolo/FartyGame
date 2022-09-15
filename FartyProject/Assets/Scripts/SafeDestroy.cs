using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeDestroy : MonoBehaviour
{
    public bool destroyMySelf;
    public GameObject destruible;
    void DestroyObj()
    {
        if (destroyMySelf) Destroy(gameObject);
        else Destroy(destruible);
    }
}
