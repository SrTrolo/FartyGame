using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManuPlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private int dirX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dirX = 1;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.velocity *= speed;
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            dirX = (int)Input.GetAxisRaw("Horizontal");
        }
        transform.GetChild(0).transform.localScale = new Vector3(dirX, 1, 1);
    }
}
