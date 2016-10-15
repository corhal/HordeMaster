using UnityEngine;
using System.Collections;

public class HumanAttackSpotter : MonoBehaviour {

    public HumanShooter humanShooter;

    void Awake() {
        humanShooter = GetComponentInParent<HumanShooter>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Follower" && !humanShooter.ShootableWalkers.Contains(other.gameObject)) {
            humanShooter.ShootableWalkers.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Follower") {
            humanShooter.ShootableWalkers.Remove(other.gameObject);
        }
    }
}
