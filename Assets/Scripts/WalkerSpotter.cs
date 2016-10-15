using UnityEngine;
using System.Collections;

public class WalkerSpotter : MonoBehaviour { // пока не дописано, задумано что зомби будут следовать друг за другом

    public Walker walker;

    void Awake() {
        walker = GetComponentInParent<Walker>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Follower") {
            other.GetComponent<Walker>().GetTargetFromAnotherWalker(walker);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Follower") {
            other.GetComponent<Walker>().GetTargetFromAnotherWalker(walker);
        }
    }
}
