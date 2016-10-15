using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed;
    GameObject victim;
    public int Damage;
    public float Lifetime;
    float timer;
    public float Inaccuracy;
    Vector3 pointOfOrigin;

    void Start() {
        pointOfOrigin = transform.position;
        GetComponent<Rigidbody>().velocity = new Vector3(transform.up.x + Random.Range(-Inaccuracy, Inaccuracy), transform.up.y + Random.Range(-Inaccuracy, Inaccuracy), transform.up.z + Random.Range(-Inaccuracy, Inaccuracy)) * speed;
    }

    void Update() {
        timer += Time.deltaTime;

        if (timer >= Lifetime && Time.timeScale != 0) {
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Human") {
            victim = other.gameObject;
            Walker walker = victim.GetComponent<Walker>();
            if (walker != null) {
                walker.TakeDamage(Damage, pointOfOrigin);
                GameObject.Destroy(this.gameObject);
            }
            else {
                HumanHealth human = victim.GetComponent<HumanHealth>();
                if (human != null) {
                    human.TakeDamage(Damage);
                    GameObject.Destroy(this.gameObject);
                }
                else {
                    DoorHealth door = victim.GetComponent<DoorHealth>();
                    if (door != null) {
                        door.TakeDamage(1);
                    }
                    GameObject.Destroy(this.gameObject);
                }
            }
        }        
    }

}
