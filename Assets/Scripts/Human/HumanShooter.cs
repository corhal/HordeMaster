using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanShooter : MonoBehaviour {

    public List<GameObject> WalkersNearby;
    public List<GameObject> ShootableWalkers;

    public HumanHealth myHealth;

    public bool SpottedSomething;
    public RangedWeapon gun;   

    public GameObject Target;

    void Awake() { // Вообще говоря, безоружный человек должен пытаться убежать        
        myHealth = GetComponentInParent<HumanHealth>();
        gun = GetComponentInChildren<RangedWeapon>();        
        HumanHealth.OnHumanDied += HumanHealth_OnHumanDied;
        Walker.OnWalkerDied += Walker_OnWalkerDied;        
    }

    void HumanHealth_OnHumanDied(HumanHealth e) {
        if (e == myHealth) {
            gun.DisableEffects();
        }
    }

    void Walker_OnWalkerDied(Walker e) {        
        ShootableWalkers.Remove(e.gameObject);
        if (e.gameObject == Target) {
            Target = null;
            gun.Shooting = false;
            SpottedSomething = false;
        }
    }

    void Update() {
        if (!SpottedSomething && ShootableWalkers.Count > 0) {
            CheckTargets();
        }

        if (SpottedSomething && Target != null) {
            transform.LookAt(Target.transform);
            gun.Shooting = true;
            CheckTargets();
        }
    }    

    public void CheckTargets() {
        GameObject closestTarget = null;

        foreach (GameObject walker in ShootableWalkers) {

            Ray ray = new Ray(transform.position, walker.transform.position - transform.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("RealObjects"))) {
                if (hit.collider.gameObject == walker) {
                    if (closestTarget == null) {
                        closestTarget = walker;
                    }
                    else {
                        if (Vector3.Distance(walker.transform.position, transform.position) < Vector3.Distance(closestTarget.transform.position, transform.position)) {
                            closestTarget = walker;
                        }
                    }
                }
            }

        }

        if (closestTarget != null) {
            SpottedSomething = true;
            Target = closestTarget;
        }
        else {
            SpottedSomething = false;
            gun.Shooting = false;
            Target = null;
        }
    }
}
