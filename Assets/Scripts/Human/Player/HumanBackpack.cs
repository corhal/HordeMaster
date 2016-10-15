using UnityEngine;
using System.Collections;

public class HumanBackpack : MonoBehaviour {

    public int Food;
    public int Ammo;

    public delegate void OnReceivedAmmoEvent(HumanBackpack e);
    public static event OnReceivedAmmoEvent OnReceivedAmmo;

    void Awake() {
        PlayerPhysicalMovement.OnCollidedWithSomething += PlayerPhysicalMovement_OnCollidedWithSomething;
    }

    void PlayerPhysicalMovement_OnCollidedWithSomething(GameObject e) {
        AmmoBox box = e.GetComponent<AmmoBox>();
        FoodCrate foodCrate = e.GetComponent<FoodCrate>();
        if (box != null) {
            bool needsToReload = false;
            if (Ammo == 0) {
                needsToReload = true;
            }
            Ammo += box.Ammo;
            GameObject.Destroy(box.gameObject);
            if (needsToReload) {
                OnReceivedAmmo(this);
            }
        }
        if (foodCrate != null) {
            Food += foodCrate.Food;
            GameObject.Destroy(foodCrate.gameObject);
        }
    }
}
