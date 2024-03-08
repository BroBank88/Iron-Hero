using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Scene to Load on Death")]
    public string sceneToLoadOnDeath; // Name of the scene to load on player death

    private void Start()
    {
        currentHealth = maxHealth;
        GetComponent<Animator>().enabled = true;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            Debug.Log("Player Health: " + currentHealth);
            // Check if the player is still alive
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth > 0)
        {
            currentHealth += amount;

            // Ensure health doesn't exceed the maximum
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }
    }

    private void Die()
    {
        GetComponent<Animator>().enabled = false;

        // Implement actions to handle player death, such as game over logic.
        // You can also deactivate the player GameObject or play death animations.

        // Start a coroutine to delay loading the scene after 3 seconds
        StartCoroutine(LoadSceneDelayed(3.0f));
    }

    private IEnumerator LoadSceneDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Load the specified scene when the player dies
        if (!string.IsNullOrEmpty(sceneToLoadOnDeath))
        {
            SceneManager.LoadScene(sceneToLoadOnDeath);
        }
    }
  

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}