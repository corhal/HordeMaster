using UnityEngine;
using System.Collections;

public class RangedHitscanWeapon : MonoBehaviour, IShootable {

	RangedWeapon gun;
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
		gun = GetComponentInParent<RangedWeapon>();
	    Walker.OnWalkerDied += Walker_OnWalkerDied;
		RangedWeapon.OnDisableEffects += Gun_OnDisableEffects;
	    //gun.OnGunShoot += Gun_OnGunShoot;
	}

	public void Shoot(float inaccuracy, int shotsAtOnce, float range, int damagePerShot) {
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		shootRay.origin = transform.position;
		shootRay.direction = new Vector3 (transform.up.x + Random.Range (-inaccuracy, inaccuracy), transform.up.y, transform.up.z + Random.Range (-inaccuracy, inaccuracy));
		if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
			victim = shootHit.collider.gameObject;
			Walker walker = victim.GetComponent<Walker> ();
			if (walker != null) {				
				walker.TakeDamage (damagePerShot, transform.position);
			} else {
				HumanHealth human = victim.GetComponent<HumanHealth> ();
				if (human != null) {
					human.TakeDamage (damagePerShot);
				} else {
					DoorHealth door = victim.GetComponent<DoorHealth> ();
					if (door != null) {
						door.TakeDamage (1);
					}
				}
			}
			gunLine.SetPosition (1, shootHit.point);	
		} else {
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}	          
	}

	//void Gun_OnGunShoot(RangedWeapon e) {
		/*gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);

		shootRay.origin = transform.position;
		shootRay.direction = new Vector3 (transform.up.x + Random.Range (-gun.Inaccuracy, gun.Inaccuracy), transform.up.y, transform.up.z + Random.Range (-gun.Inaccuracy, gun.Inaccuracy));
		if (Physics.Raycast (shootRay, out shootHit, gun.Range, shootableMask)) {
			victim = shootHit.collider.gameObject;
			Walker walker = victim.GetComponent<Walker> ();
			if (walker != null) {				
				walker.TakeDamage (gun.DamagePerShot, transform.position);
			} else {
				HumanHealth human = victim.GetComponent<HumanHealth> ();
				if (human != null) {
					human.TakeDamage (gun.DamagePerShot);
				} else {
					DoorHealth door = victim.GetComponent<DoorHealth> ();
					if (door != null) {
						door.TakeDamage (1);
					}
				}
			}
			gunLine.SetPosition (1, shootHit.point);	
		} else {
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * gun.Range);
		}	*/          
	//}

	void Gun_OnDisableEffects(RangedWeapon e) {
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
