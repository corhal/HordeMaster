using UnityEngine;
using System.Collections;

public class ShotgunShoot : MonoBehaviour {

    Gun gun;
    public GameObject GunBarrelEnd;
    public GameObject BulletPrefab;

    void Awake() {                
        gun = GetComponentInParent<Gun>();        
        Gun.OnGunShoot += Gun_OnGunShoot;
        Gun.OnDisableEffects += Gun_OnDisableEffects;
        Walker.OnWalkerDied += Walker_OnWalkerDied;
    }

    void Walker_OnWalkerDied(Walker e) {
        // same shit
    }

    void Gun_OnDisableEffects(Gun e) {
        // Если этого не сделать, Gun пугается
    }

    void Gun_OnGunShoot(Gun e) {        
        if (e == gun) {
            for (int i = 0; i < gun.ShotsAtOnce; i++) {
                Vector3 spawnPosition = new Vector3(GunBarrelEnd.transform.position.x + Random.Range(-gun.Inaccuracy, gun.Inaccuracy), GunBarrelEnd.transform.position.y + Random.Range(-gun.Inaccuracy, gun.Inaccuracy), GunBarrelEnd.transform.position.z + Random.Range(-gun.Inaccuracy, gun.Inaccuracy));
                GameObject bulletObject = GameObject.Instantiate(BulletPrefab, spawnPosition, GunBarrelEnd.transform.rotation) as GameObject;
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                if (bullet != null) {
                    bullet.Damage = gun.DamagePerShot;
                }
            }
        }
    }   
}
