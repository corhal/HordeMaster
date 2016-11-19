using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RangedWeapon))]
public class RangedHitscanWeapon : MonoBehaviour, IShootable {
	
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;    
	LineRenderer gunLine;
	GameObject victim;

	void Awake() {
	    shootableMask = LayerMask.GetMask("RealObjects");
	    gunLine = GetComponentInChildren<LineRenderer>();
	    Walker.OnWalkerDied += Walker_OnWalkerDied;   
	}

	public void Shoot(float inaccuracy, int shotsAtOnce, float range, int damagePerShot) {
		gunLine.SetVertexCount (shotsAtOnce * 2);

		gunLine.enabled = true;

		int j = 0;
		for (int i = 0; i < shotsAtOnce; i++) {
			gunLine.SetPosition (j, transform.position);
			j++;
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
				gunLine.SetPosition (j, shootHit.point);	
			} else {
				gunLine.SetPosition (j, shootRay.origin + shootRay.direction * range);
			}
			j++;
		}
	}

	public void StopShooting() {
		gunLine.enabled = false;
	}

	void Walker_OnWalkerDied(Walker e) {
	    if (victim == e.gameObject) {
	        victim = null;
	    }
	}    
}
