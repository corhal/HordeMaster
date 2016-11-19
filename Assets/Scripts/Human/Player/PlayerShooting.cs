using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public GameObject gunSpawnPoint;
    public RangedWeapon gun;
    HumanHealth myHealth;

    public delegate void OnPickedUpGunEvent(RangedWeapon e);
    public static event OnPickedUpGunEvent OnPickedUpGun;

    void Awake() {        
        gun = GetComponentInChildren<RangedWeapon>();
        myHealth = GetComponentInParent<HumanHealth>();        
        HumanHealth.OnHumanDied += HumanHealth_OnHumanDied;
        PlayerPhysicalMovement.OnCollidedWithSomething += PlayerPhysicalMovement_OnCollidedWithSomething;
    }

    void PlayerPhysicalMovement_OnCollidedWithSomething(GameObject e) {
        GunCrate crate = e.GetComponent<GunCrate>();
        if (crate != null) {
            if (gun != null) {
                GameObject.Destroy(gun);
                GameObject.Destroy(gun.gameObject);
                //gun = null;
            }
            GameObject gunObject = Instantiate(crate.StoredGun, gunSpawnPoint.transform.position, gunSpawnPoint.transform.rotation) as GameObject;

            //GameObject bulletObject = GameObject.Instantiate(BulletPrefab, spawnPosition, GunBarrelEnd.transform.rotation) as GameObject;
            gunObject.transform.SetParent(gunSpawnPoint.transform);

            gun = gunObject.GetComponent<RangedWeapon>(); // Нужно рассказывать UIcontroller о произошедших изменениях            
            OnPickedUpGun(gun);
            GameObject.Destroy(crate.gameObject);
        }
    }

    void HumanHealth_OnHumanDied(HumanHealth e) {
        if (e == myHealth) {
            gun.DisableEffects();
        }
    }

    void Update() {        
        if (gun != null) {
            if (Input.GetButton("Fire1")) {
                gun.Shooting = true;
            }
            else {
                gun.Shooting = false;
            }  
        }             
    }
}
