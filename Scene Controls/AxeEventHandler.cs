using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeEventHandler : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}
