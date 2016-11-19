using UnityEngine;
using System.Collections;

[RequireComponent(typeof(IShootable))]
public class RangedWeapon : MonoBehaviour {

	IShootable shootable;

    public int DamagePerShot;
    public int ShotsAtOnce;
    public int MaxClipSize;
    public int CurrentClipSize;
    public float TimeBetweenBullets;
    public float Range = 100f;
    public float TimeToReload;
    public float Inaccuracy;
    public float gunSoundDistance;

    public bool Shooting = false;
    float timer;    
    //ParticleSystem gunParticles;    
    //AudioSource gunAudio;
    Light gunLight;
    public float EffectsDisplayTime;

    public delegate void WeaponClipDepletedEventHandler (RangedWeapon e);
    public static event WeaponClipDepletedEventHandler OnWeaponClipDepleted;

    public delegate void WeaponMakeSoundEventHandler (Vector3 position, float MaxSoundDistance);
    public static event WeaponMakeSoundEventHandler OnWeaponMakeSound;

    void Awake() {        // когда игрок движется, линия рисуется не из ствола(
        //gunParticles = GetComponent<ParticleSystem>();        
        //gunAudio = GetComponent<AudioSource>();
		shootable = gameObject.GetComponent<IShootable> ();
        gunLight = GetComponentInChildren<Light> ();
        CurrentClipSize = MaxClipSize;        
    }

    void Update() {
        timer += Time.deltaTime;

        if (CurrentClipSize > 0 && Shooting && timer >= TimeBetweenBullets && Time.timeScale != 0) {
            Shoot();
        }

		if (timer >= TimeBetweenBullets * EffectsDisplayTime) {
            DisableEffects();
        }
    }

    public int Reload(int ammo) {
        if (ammo + CurrentClipSize <= MaxClipSize) {
            CurrentClipSize += ammo;
            return ammo;
        }
        else {
            int currentAmmo = CurrentClipSize;
            CurrentClipSize = MaxClipSize;
            return (MaxClipSize - currentAmmo);
        }
    }

    public void DisableEffects() {
		shootable.StopShooting ();
        gunLight.enabled = false;
    }
    
    void Shoot() {
        timer = 0f;
        CurrentClipSize--;

        if (CurrentClipSize == 0) {
            OnWeaponClipDepleted(this);
        }

        //gunAudio.Play();

        gunLight.enabled = true;

        //gunParticles.Stop();
        //gunParticles.Play();

		shootable.Shoot (Inaccuracy, ShotsAtOnce, Range, DamagePerShot);

        OnWeaponMakeSound(transform.position, gunSoundDistance);
    }

    void OnDestroy() {
        Debug.Log(this + " destroyed");
    }
}
