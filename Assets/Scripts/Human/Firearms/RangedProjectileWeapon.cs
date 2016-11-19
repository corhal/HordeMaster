using UnityEngine;
using System.Collections;

public class RangedProjectileWeapon : MonoBehaviour, IShootable {

	//RangedWeapon gun;
    public GameObject GunBarrelEnd;
    public GameObject BulletPrefab;

    void Awake() {                
		//gun = GetComponentInParent<RangedWeapon>();        
        //gun.OnGunShoot += Gun_OnGunShoot;
		RangedWeapon.OnDisableEffects += Gun_OnDisableEffects;
        Walker.OnWalkerDied += Walker_OnWalkerDied;
    }

    void Walker_OnWalkerDied(Walker e) {
        // same shit
    }

	void Gun_OnDisableEffects(RangedWeapon e) {
        // Если этого не сделать, Gun пугается
    }

	public void Shoot(float inaccuracy, int shotsAtOnce, float range, int damagePerShot) {
		for (int i = 0; i < shotsAtOnce; i++) {
			Vector3 spawnPosition = new Vector3(GunBarrelEnd.transform.position.x + Random.Range(-inaccuracy, inaccuracy), GunBarrelEnd.transform.position.y + Random.Range(-inaccuracy, inaccuracy), GunBarrelEnd.transform.position.z + Random.Range(-inaccuracy, inaccuracy));
			GameObject bulletObject = GameObject.Instantiate(BulletPrefab, spawnPosition, GunBarrelEnd.transform.rotation) as GameObject;
			Bullet bullet = bulletObject.GetComponent<Bullet>();
			if (bullet != null) {
				bullet.Damage = damagePerShot;
			}
		}
	}

	//void Gun_OnGunShoot(RangedWeapon e) {        
        //if (e == gun) {
           /* for (int i = 0; i < gun.ShotsAtOnce; i++) {
                Vector3 spawnPosition = new Vector3(GunBarrelEnd.transform.position.x + Random.Range(-gun.Inaccuracy, gun.Inaccuracy), GunBarrelEnd.transform.position.y + Random.Range(-gun.Inaccuracy, gun.Inaccuracy), GunBarrelEnd.transform.position.z + Random.Range(-gun.Inaccuracy, gun.Inaccuracy));
                GameObject bulletObject = GameObject.Instantiate(BulletPrefab, spawnPosition, GunBarrelEnd.transform.rotation) as GameObject;
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                if (bullet != null) {
                    bullet.Damage = gun.DamagePerShot;
                }
            }*/
        //}
    //}   
}
