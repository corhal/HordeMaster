using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Walker : MonoBehaviour {

    public NavMeshAgent agent; // нужно отделить управляемых зомби от неуправляемых
    public float SightAngle; // каким-то мистическим образом проходят двери насквозь...

    public int xOffset;
    public int zOffset;
    public float AttackDistance;
    float timer;
    public float timeBetweenAttacks;     // The time in seconds between each attack.
    public int attackDamage;               // The amount of health taken away per attack.

    public bool SpottedSomething;
    public bool IsLeader;
    bool isDead = false;

    public List<GameObject> HumanTargets;
    public GameObject Target;

    public int startingHealth;
    public int currentHealth;
    public float StepSoundDistance;
    public float LoudestSoundHeard;

    public float maxHealthVariance;
    public float maxSpeedVariance;

    public delegate void OnWalkerDiedEvent(Walker e);
    public static event OnWalkerDiedEvent OnWalkerDied;

    // public delegate void OnWalkerStepSoundEvent(Vector3 position, float MaxSoundDistance);
    // public static event OnWalkerStepSoundEvent OnStepSound;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        HumanHealth.OnHumanDied += HumanHealth_OnHumanDied;
        float coinToss = Random.Range(0f, 1.0f);        
        if (coinToss < 0.4f) {
            startingHealth += (int)(startingHealth * Random.Range(0.01f, maxHealthVariance));
            agent.speed += agent.speed * Random.Range(0.1f, maxSpeedVariance);
        }
        else {
            startingHealth -= (int)(startingHealth * Random.Range(0.01f, maxHealthVariance));
            agent.speed -= agent.speed * Random.Range(0.1f, maxSpeedVariance);
        }

        transform.Rotate(transform.up, Random.Range(0, 360f));
        currentHealth = startingHealth;
        SoundMaker.OnMakeSound += SoundMaker_OnMakeSound;
        //Walker.OnStepSound += Walker_OnStepSound;
    }

    /*void Walker_OnStepSound(Vector3 position, float MaxSoundDistance) {
        if (Vector3.Distance(transform.position, position) < MaxSoundDistance && agent.velocity.magnitude < 0.5f) {
            if (!SpottedSomething) {
                LoudestSoundHeard = MaxSoundDistance - Vector3.Distance(transform.position, position);
                agent.SetDestination(position);
            }
        }
    }*/

    void SoundMaker_OnMakeSound(Vector3 position, float MaxSoundDistance) {
        if (Vector3.Distance(transform.position, position) < MaxSoundDistance && (MaxSoundDistance - Vector3.Distance(transform.position, position)) > LoudestSoundHeard) {
            if (!SpottedSomething) {
                LoudestSoundHeard = MaxSoundDistance - Vector3.Distance(transform.position, position);
                agent.SetDestination(position);
            }
        }
    }

    public void GetTargetFromAnotherWalker(Walker walker) { // пока не дописано, задумано что зомби будут следовать друг за другом
        if (!SpottedSomething) {
            Debug.Log(walker.agent.destination);
            agent.SetDestination(walker.agent.destination); 
        }               
    }

    void HumanHealth_OnHumanDied(HumanHealth e) {
        HumanTargets.Remove(e.gameObject.GetComponentInChildren<PlayerPhysicalMovement>().gameObject); // ад
        if (e.gameObject.GetComponentInChildren<PlayerPhysicalMovement>().gameObject == Target) {
            Target = null;
            SpottedSomething = false;
        }
    }

    void Start() {        
        xOffset = Random.Range(1, 4);
        zOffset = Random.Range(-1, -4);
    }

    void Update() {
        timer += Time.deltaTime;

        /*if (agent.velocity.magnitude > 4.0f) {
            OnStepSound(transform.position, StepSoundDistance);
        }*/

        if (!SpottedSomething && HumanTargets.Count > 0) {
            CheckTargets();
        }

        if (SpottedSomething) {
            agent.SetDestination(Target.transform.position);
            if (Vector3.Distance(transform.position, Target.transform.position) < AttackDistance && timer >= timeBetweenAttacks) {
                Attack(Target.GetComponentInParent<HumanHealth>());
            }
        }
    }

    public bool LookAtObject(GameObject objectToLookAt) {
        Ray ray = new Ray(transform.position, objectToLookAt.transform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100.0f)) {
            if (hit.collider.gameObject == objectToLookAt) {
                Debug.Log("I can see target");
                SpottedSomething = true;
                Target = objectToLookAt;
                return true;
            }
        }
        return false;
    }

    public void CheckTargets() {
        GameObject closestTarget = null;

        foreach (GameObject humanTarget in HumanTargets) {
            if (Vector3.Angle(humanTarget.transform.position - transform.position, transform.forward) <= SightAngle / 2) {
                Ray ray = new Ray(transform.position, humanTarget.transform.position - transform.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("RealObjects"))) {
                    if (hit.collider.gameObject == humanTarget) {
                        if (closestTarget == null) {
                            closestTarget = humanTarget;
                        }
                        else {
                            if (Vector3.Distance(humanTarget.transform.position, transform.position) < Vector3.Distance(closestTarget.transform.position, transform.position)) {
                                closestTarget = humanTarget;
                            }
                        }
                    }
                }
            }            
        }

        if (closestTarget != null) {
            SpottedSomething = true;
            Target = closestTarget;
        }
    }

    void Attack(HumanHealth humanTarget) {
        // Reset the timer.
        timer = 0f;

        // If the player has health to lose...
        if (humanTarget.currentHealth > 0) {
            // ... damage the player.
            humanTarget.Infected = true;
            humanTarget.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int amount, Vector3 offenderPosition) {
        if (!isDead) {
            if (!SpottedSomething) {
                agent.SetDestination(offenderPosition);
            }

            // Reduce the current health by the damage amount.
            currentHealth -= amount;

            // Play the hurt sound effect.
            //playerAudio.Play();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0) {
                // ... it should die.
                Death();
            }
        }
    }

    void Death() {
        // Set the death flag so this function won't be called again.        
        isDead = true;

        OnWalkerDied(this);

        Destroy(gameObject);

        // Tell the animator that the player is dead.
        //anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        // Turn off the movement and shooting scripts.
        //playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }

    void OnDestroy() {
        HumanHealth.OnHumanDied -= HumanHealth_OnHumanDied;
        SoundMaker.OnMakeSound -= SoundMaker_OnMakeSound;
        //Walker.OnStepSound -= Walker_OnStepSound;
    }
}
