using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    public int healAmount = 30;
    public AudioClip pickupSound; // Assign the pickup sound clip in the Inspector.
    public float volume = 0.1f; // Adjust the volume as needed.

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component of the player
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Play the pickup sound if an audio clip is assigned
                if (pickupSound != null)
                {
                    // Create a new GameObject to play the audio clip
                    GameObject audioObject = new GameObject("PickupAudio");
                    AudioSource audioSource = audioObject.AddComponent<AudioSource>();
                    audioSource.clip = pickupSound;

                    // Set the volume to the desired value
                    audioSource.volume = volume;

                    audioSource.Play();

                    // Destroy the audio GameObject after playing the sound
                    Destroy(audioObject, pickupSound.length);
                }

                // Heal the player by the specified amount
                playerHealth.Heal(healAmount);

                // Destroy the Medkit GameObject after it's picked up
                Destroy(gameObject);
            }
        }
    }
}