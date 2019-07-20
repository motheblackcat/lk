using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    Rigidbody2D rb;
    public float rbValues;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rbValues = rb.velocity.x;
        if (Input.GetAxis("Horizontal") != 0)
        {
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), 0));
        }
    }
}
