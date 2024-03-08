using UnityEngine;
using System.Collections;

public class SlowTimeSuperPower : MonoBehaviour
{
    public float timeScaleDuringSlowTime = 0.5f; // Time scale during slow time (0.5 means 2x slower)
    public float slowTimeDuration = 4f; // Duration of the slow-time effect in seconds

    private bool isSlowTimeActive = false; // Flag to track if slow time is active

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSlowTimeActive)
        {
            // Deactivate the renderer and collider of the sphere immediately
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
            // Activate the slow-time effect
            ActivateSlowTime();
        }
    }

    private void ActivateSlowTime()
    {
        // Set the flag to indicate that slow time is active
        isSlowTimeActive = true;

        // Store the original time scale
        float originalTimeScale = Time.timeScale;

        // Adjust the time scale to create a slow-motion effect
        Time.timeScale = timeScaleDuringSlowTime;

        // Start a coroutine to revert time back to normal after the duration
        StartCoroutine(RevertTimeScale(originalTimeScale, slowTimeDuration));
    }

    private IEnumerator RevertTimeScale(float originalTimeScale, float duration)
    {
        yield return new WaitForSeconds(duration);

        // Revert time scale to the original value
        Time.timeScale = originalTimeScale;

        // Set the flag to indicate that slow time is no longer active
        isSlowTimeActive = false;

        // Destroy the item after the slow-time effect is over
        Destroy(gameObject);
    }
}