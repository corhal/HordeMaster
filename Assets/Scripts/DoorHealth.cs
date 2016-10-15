using UnityEngine;
using System.Collections;

public class DoorHealth : MonoBehaviour {

    public int StartingHP;
    public int CurrentHP;

    float timer;
    public float MinTimeBetweenAttacks;

    void Awake() {
        CurrentHP = StartingHP;
    }

    void Update() { // Пытался сделать чтобы в дверь можно было стрелять и дамажить. сейчас не особо вышло, потому что не та часть двери относится к RealObjects
        timer += Time.deltaTime;
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Follower") {            
            if (timer >= MinTimeBetweenAttacks) {
                timer = 0f;
                TakeDamage(1);
            }
        }        
    }

    public void TakeDamage(int amount) {
        CurrentHP -= amount;

        if (CurrentHP <= 0) {
            Destroy(gameObject);
        }
    }
}
