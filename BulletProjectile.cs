using UnityEngine;
using UnityEngine.AI;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    [SerializeField] private AudioSource shootingAudioSource;

    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 40f;
        bulletRigidbody.velocity = transform.forward * speed;

        if (shootingAudioSource != null)
        {
            shootingAudioSource.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is on the "Enemy" layer.
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyAiTutorial enemy = other.GetComponent<EnemyAiTutorial>();

            if (enemy != null)
            {
                enemy.TakeDamage(10); // Apply damage to the enemy.
            }
        }

        // Instantiate hit effects based on whether it hit the enemy or not.
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}