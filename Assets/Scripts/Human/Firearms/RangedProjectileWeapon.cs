using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RangedWeapon))]
public class RangedProjectileWeapon : MonoBehaviour, IShootable {
	
    public GameObject GunBarrelEnd;
    public GameObject ProjectilePrefab;

    void Awake() {
        Walker.OnWalkerDied += Walker_OnWalkerDied;
    }

    void Walker_OnWalkerDied(Walker e) {
		// Если этого не сделать, видимо walker пугается
    }

	public void Shoot(float inaccuracy, int shotsAtOnce, float range, int damagePerShot) {
		for (int i = 0; i < shotsAtOnce; i++) {
			Vector3 spawnPosition = new Vector3(GunBarrelEnd.transform.position.x + Random.Range(-inaccuracy, inaccuracy), GunBarrelEnd.transform.position.y + Random.Range(-inaccuracy, inaccuracy), GunBarrelEnd.transform.position.z + Random.Range(-inaccuracy, inaccuracy));
			GameObject bulletObject = GameObject.Instantiate(ProjectilePrefab, spawnPosition, GunBarrelEnd.transform.rotation) as GameObject;
			Bullet bullet = bulletObject.GetComponent<Bullet>();
			if (bullet != null) {
				bullet.Damage = damagePerShot;
			}
		}
	}

	public void StopShooting() {

	}
}
