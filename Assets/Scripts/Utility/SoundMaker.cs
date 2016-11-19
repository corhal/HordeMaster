using UnityEngine;
using System.Collections;

public class SoundMaker : MonoBehaviour {

    public delegate void OnMakeSoundEvent(Vector3 position, float MaxSoundDistance);
    public static event OnMakeSoundEvent OnMakeSound;

	void Awake () {        
        PlayerPhysicalMovement.OnHumanStepSound += PlayerPhysicalMovement_OnHumanStepSound;
        RangedWeapon.OnWeaponMakeSound += Gun_OnGunShootSound;
        SoundMaker.OnMakeSound += SoundMaker_OnMakeSound;
        Alarm.OnAlarmSound += Alarm_OnAlarmSound;
	}

    void Alarm_OnAlarmSound(Vector3 position, float MaxSoundDistance) {
        OnMakeSound(position, MaxSoundDistance);
    }

    void SoundMaker_OnMakeSound(Vector3 position, float MaxSoundDistance) {
        // очередной слушатель, потому что я не знаю как сделать нормально
    }    

    void Gun_OnGunShootSound(Vector3 position, float MaxSoundDistance) {
        OnMakeSound(position, MaxSoundDistance);
    }

    void PlayerPhysicalMovement_OnHumanStepSound(Vector3 position, float MaxSoundDistance) {
        OnMakeSound(position, MaxSoundDistance);
    }


    
}
