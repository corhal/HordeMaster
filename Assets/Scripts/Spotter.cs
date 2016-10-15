using UnityEngine;
using System.Collections;

public class Spotter : MonoBehaviour {

    public Walker walker;

    void Awake() {
        walker = GetComponentInParent<Walker>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Human") {
            walker.HumanTargets.Add(other.gameObject);
        }        
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Human") {
            walker.HumanTargets.Remove(other.gameObject);
        }
    }
}
