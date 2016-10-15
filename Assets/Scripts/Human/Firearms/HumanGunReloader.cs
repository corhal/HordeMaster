using UnityEngine;
using System.Collections;

public class HumanGunReloader : MonoBehaviour {
    
    public Gun gun;
    public HumanBackpack backpack;
    
    public float reloadTime;
    //public int Ammo;

    public delegate void OnStartedToReloadEvent(Gun e);
    public static event OnStartedToReloadEvent OnStartedToReload;

    public delegate void OnReloadedEvent(Gun e);
    public static event OnReloadedEvent OnReloaded;
   
    void Awake() {              
        gun = GetComponentInChildren<Gun>();
        backpack = GetComponentInChildren<HumanBackpack>();
        Gun.OnGunClipDepleted += Gun_OnGunClipDepleted;
        PlayerShooting.OnPickedUpGun += PlayerShooting_OnPickedUpGun;
        //PlayerPhysicalMovement.OnCollidedWithSomething += PlayerPhysicalMovement_OnCollidedWithSomething;
        HumanBackpack.OnReceivedAmmo += HumanBackpack_OnReceivedAmmo;
    }

    void HumanBackpack_OnReceivedAmmo(HumanBackpack e) {
        if (e == backpack) {
            ManualReload();
        }
    }

    void Update() {
        if (Input.GetKeyDown("r"))
            ManualReload();
    }

    void PlayerShooting_OnPickedUpGun(Gun e) {
        if (GetComponentInParent<PlayerShooting>() != null) {
            gun = e;
        }        
    }

    public void ManualReload() {
        if (backpack.Ammo > 0) {
            if (gun.gameObject.GetComponentInParent<PlayerMovement>() != null) {
                OnStartedToReload(gun);
            }
            StartCoroutine(Reload());
        }
    }

    void Gun_OnGunClipDepleted(Gun e) {
        if (backpack.Ammo > 0 && e == gun) {
            if (gun.gameObject.GetComponentInParent<PlayerMovement>() != null) {
                OnStartedToReload(gun); // каким-то невероятным образом пытается передать событие в другую сцену
            }            
            StartCoroutine(Reload());
        }
    }
    
    IEnumerator Reload() {
        yield return new WaitForSeconds(reloadTime);
        if (gun != null) {
            backpack.Ammo -= gun.Reload(backpack.Ammo);
            if (gun.gameObject.GetComponentInParent<PlayerMovement>() != null) {
                OnReloaded(gun);
            }
        }        
    }    
}
