using UnityEngine;
using System.Collections;

public class GunShoot : MonoBehaviour {

    Gun gun;
    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;    
    LineRenderer gunLine;
    GameObject victim;

    void Awake() {
        shootableMask = LayerMask.GetMask("RealObjects");
        //gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponentInChildren<LineRenderer>();
        //gunAudio = GetComponent<AudioSource>();     
        gun = GetComponentInParent<Gun>();
        Walker.OnWalkerDied += Walker_OnWalkerDied;
        Gun.OnDisableEffects += Gun_OnDisableEffects;
        Gun.OnGunShoot += Gun_OnGunShoot;
    }

    void Gun_OnGunShoot(Gun e) {
        if (e == gun) {
            gunLine.enabled = true;
            gunLine.SetPosition(0, transform.position);

            shootRay.origin = transform.position;
            shootRay.direction = new Vector3(transform.up.x + Random.Range(-gun.Inaccuracy, gun.Inaccuracy), transform.up.y, transform.up.z + Random.Range(-gun.Inaccuracy, gun.Inaccuracy));
            if (Physics.Raycast(shootRay, out shootHit, gun.Range, shootableMask)) {
                victim = shootHit.collider.gameObject;
                Walker walker = victim.GetComponent<Walker>();
                if (walker != null) {
                    walker.TakeDamage(gun.DamagePerShot, transform.position);
                }
                else {
                    HumanHealth human = victim.GetComponent<HumanHealth>();
                    if (human != null) {
                        human.TakeDamage(gun.DamagePerShot);
                    }
                    else {
                        DoorHealth door = victim.GetComponent<DoorHealth>();
                        if (door != null) {
                            door.TakeDamage(1);
                        }
                    }
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * gun.Range);
            }
        }        
    }

    void Gun_OnDisableEffects(Gun e) {
        if (gun == e) {
            gunLine.enabled = false;
        }        
    }

    void Walker_OnWalkerDied(Walker e) {
        if (victim == e.gameObject) {
            victim = null;
        }
    }    
}
