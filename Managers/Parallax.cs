using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Transform camPos;
    public Vector3 camCoord;
    public Transform bgPos;
    public Vector3 bgCoord;
    public float smoothing = 1f;

    void FixedUpdate()
    {
        camPos = Camera.main.transform;
        camCoord = camPos.position;
        bgCoord = bgPos.position;
        bgPos.position = new Vector3((camPos.position.x / smoothing) - 10, bgPos.position.y, bgPos.position.z);
    }
}
