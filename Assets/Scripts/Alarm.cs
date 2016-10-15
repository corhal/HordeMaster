using UnityEngine;
using System.Collections;

public class Alarm : MonoBehaviour, ITogglable {

    public Light RedLight;
    public Light BlueLight;

    public delegate void OnAlarmSoundEvent(Vector3 position, float MaxSoundDistance);
    public static event OnAlarmSoundEvent OnAlarmSound;

    float timer;
    public float TimeBetweenBlinks;
    public float AlarmSoundDistance;

    public bool isActive;

    public Transform AlarmGroundProjection;    
    
    public void Activate() {                
        RedLight.gameObject.SetActive(true);
        isActive = true;        
    }

    public void Deactivate() {        
        isActive = false;
        RedLight.gameObject.SetActive(false);
        BlueLight.gameObject.SetActive(false);
    }

    void Update() {
        timer += Time.deltaTime;

        if (isActive && timer >= TimeBetweenBlinks) {
            timer = 0f;
            OnAlarmSound(AlarmGroundProjection.position, AlarmSoundDistance);
            if (RedLight.gameObject.activeSelf) {                
                RedLight.gameObject.SetActive(false);
                BlueLight.gameObject.SetActive(true);
            }
            else if (BlueLight.gameObject.activeSelf) {                
                BlueLight.gameObject.SetActive(false);
                RedLight.gameObject.SetActive(true);
            }
        }
    }

    public void Toggle() {
        if (isActive) {
            Deactivate();
        }
        else {
            Activate();
        }
    }
}
