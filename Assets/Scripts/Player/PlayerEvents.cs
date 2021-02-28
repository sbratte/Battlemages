using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public float bumpDamage;
    public float waterDamage;
    public float fireDamage;
    public float spikeDamage;
    public float knockbackRate;

    private Rigidbody body;

    public void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Collision Events - Damage
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            HealthSystem.Instance.TakeDamage(fireDamage);
            body.AddForce(Vector3.forward * fireDamage * knockbackRate, ForceMode.Force);
        }

        if (collision.gameObject.tag == "Spike")
        {
            HealthSystem.Instance.TakeDamage(spikeDamage);
            body.AddForce(Vector3.up * spikeDamage * knockbackRate, ForceMode.Force);
        }
    }

    // Trigger Stay - Water Damage (Drown over time)
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            HealthSystem.Instance.TakeDamage(waterDamage * Time.deltaTime);
        }
    }

    // Trigger Enter - Health and Mana pick up
    private void OnTriggerEnter(Collider other)
    {
        bool atMaxHealth = HealthSystem.Instance.hitPoints == HealthSystem.Instance.maxHitPoints;
        bool atMaxMana = HealthSystem.Instance.hitPoints == HealthSystem.Instance.maxManaPoints;

        if (other.gameObject.tag == "Health" && !atMaxHealth)
        {
            HealthSystem.Instance.HealDamage(25);
            Destroy(other.gameObject, 0);
        }

        if (other.gameObject.tag == "Mana" && !atMaxMana)
        {
            HealthSystem.Instance.RestoreMana(25);
            Destroy(other.gameObject, 0);
        }
    }
}
