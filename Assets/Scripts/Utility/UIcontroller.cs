using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIcontroller : MonoBehaviour {

    public GameObject Player;
    public Text ReloadLabel;
    public Text AmmoLabel;
    public Text FoodLabel;
    public Text HealthLabel;

    public Gun playersGun;    
    HumanGunReloader playersReloader;
    HumanBackpack backpack;
    HumanHealth playerHealth;

    int currentGunAmmo;
    int playersAmmo;

    // Use this for initialization
    void Awake() {
        HumanGunReloader.OnStartedToReload += HumanGunReloader_OnStartedToReload;
        HumanGunReloader.OnReloaded += HumanGunReloader_OnReloaded;
        PlayerShooting.OnPickedUpGun += PlayerShooting_OnPickedUpGun;
        playerHealth = Player.GetComponentInChildren<HumanHealth>();
        playersGun = Player.GetComponentInChildren<Gun>();
        playersReloader = Player.GetComponentInChildren<HumanGunReloader>();
        backpack = Player.GetComponentInChildren<HumanBackpack>();
    }

    void PlayerShooting_OnPickedUpGun(Gun e) {
        playersGun = e;
    }

    void HumanGunReloader_OnStartedToReload(Gun e) {
        if (e == playersGun && ReloadLabel != null) { // проверка на магию
            ReloadLabel.text = "Reloading!";
        }
    }

    void HumanGunReloader_OnReloaded(Gun e) {
        if (e == playersGun && ReloadLabel != null) {
            ReloadLabel.text = "";
        }
    }

    void Start() {
        ReloadLabel.text = "";
        AmmoLabel.text = "";
        if (playersGun != null) {
            currentGunAmmo = playersGun.CurrentClipSize;
        }        
        playersAmmo = playersReloader.backpack.Ammo;
    }

    // Update is called once per frame
    void Update() {
        if (playersGun != null) {
            currentGunAmmo = playersGun.CurrentClipSize;
            playersAmmo = playersReloader.backpack.Ammo;
            AmmoLabel.text = currentGunAmmo + "/" + playersAmmo;
            if (playersReloader.backpack.Ammo == 0) {
                ReloadLabel.text = "";
            }
        }
        if (backpack != null) {
            FoodLabel.text = "Food: " + backpack.Food;
        }
        if (playerHealth != null) {
            HealthLabel.text = "HP: " + playerHealth.currentHealth + "/" + playerHealth.startingHealth;
        }
    }
}
