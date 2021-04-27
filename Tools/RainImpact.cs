using UnityEngine;

public class RainImpact : MonoBehaviour {
    [SerializeField] Material rainImpactMat;

    void OnParticleCollision(GameObject other) {
        if (other.tag == "Ground") Debug.Log(other.name);
    }
}