using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private bool isMoving;
    private Vector3 finalPos;
    private float size;
    void Update()
    {
        if (isMoving)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, size, 5 * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, finalPos, 7 * Time.deltaTime);
            if (Vector2.Distance(transform.position, finalPos) < 0.2f)
            {
                transform.position = finalPos;
                Camera.main.orthographicSize = size;
                isMoving = false;
            }
        }
    }
    public void MoveCamera(Vector3 _room, float _size)
    {
        size = _size;
        finalPos = _room;
        isMoving = true;
    }
}
