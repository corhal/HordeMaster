using UnityEngine;
using System.Collections;

public class HumanHealth : MonoBehaviour {

    public int startingHealth;
    public int currentHealth;
    //public AudioClip deathClip;

    Animator anim;                                              // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    public bool isDead;                                                // Whether the player is dead.
    public bool Infected;
    public bool IsShooter = false;

    public Material DamageMaterial;
    public Material DefaultMaterial;
    public Renderer Rend;
    bool damaged;                                               // True when the player gets damaged.

    public delegate void OnHumanDiedEvent(HumanHealth e);
    public static event OnHumanDiedEvent OnHumanDied;

    void Awake() {
        // Setting up the references.
        //anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        //playerMovement = GetComponent<PlayerMovement>();        

        // Set the initial health of the player.
        currentHealth = startingHealth;
        Rend = GetComponentInChildren<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
        // If the player has just been damaged...
        if (damaged) {
            // ... set the colour of the damageImage to the flash colour.
            Rend.material = DamageMaterial;
        }
        // Otherwise...
        else {
            // ... transition the colour back to clear.
            Rend.material = DefaultMaterial;
        }

        // Reset the damaged flag.
        damaged = false;	
	}

    public void TakeDamage(int amount) {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Play the hurt sound effect.
        //playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead) {
            // ... it should die.
            Death();
        }
    }

    void Death() {
        // Set the death flag so this function won't be called again.
        isDead = true;

        if (IsShooter) {
             // Turn off any remaining shooting effects.
        }        
        OnHumanDied(this); // На самом деле человек может случайно застрелить другого. Но в этом случае зомби не должен появиться
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
}
