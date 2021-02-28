using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
	public static HealthSystem Instance;

	public Image currentHealthGlobe;
	public float hitPoints = 100f;
	public float maxHitPoints = 100f;

	public Image currentManaGlobe;
	public float manaPoints = 100f;
	public float maxManaPoints = 100f;

	public float manaRegen = 5f;
	private float timeleft = 0.0f;
	public float regenInterval = 1f;

    // Create instance for use in player events script
    private void Awake()
    {
		Instance = this;
    }

    void Start()
	{
		UpdateGraphics();
		timeleft = regenInterval; 
	}

	void Update ()
	{
		RegenMana();
	}

	// Mana Regeneration
	private void RegenMana()
	{
		timeleft -= Time.deltaTime;

		if (timeleft <= 0.0) // Interval ended - update health & mana and start new interval
		{
			RestoreMana(manaRegen);				
			timeleft = regenInterval;

			UpdateGraphics();
		}
	}

	// Health Logic
	private void UpdateHealthGlobe()
	{
		float ratio = hitPoints / maxHitPoints;
		currentHealthGlobe.rectTransform.localPosition = new Vector3(0, currentHealthGlobe.rectTransform.rect.height * ratio - currentHealthGlobe.rectTransform.rect.height, 0);
	}

	public void TakeDamage(float Damage)
	{
		hitPoints -= Damage;
		if (hitPoints < 1)
			hitPoints = 0;

		UpdateGraphics();
	}

	public void HealDamage(float Heal)
	{
		hitPoints += Heal;
		if (hitPoints > maxHitPoints) 
			hitPoints = maxHitPoints;

		UpdateGraphics();
	}

	// Mana Logic
	private void UpdateManaGlobe()
	{
		float ratio = manaPoints / maxManaPoints;
		currentManaGlobe.rectTransform.localPosition = new Vector3(0, currentManaGlobe.rectTransform.rect.height * ratio - currentManaGlobe.rectTransform.rect.height, 0);
	}

	public void UseMana(float Mana)
	{
		manaPoints -= Mana;
		if (manaPoints < 1)
			manaPoints = 0;

		UpdateGraphics();
	}

	public void RestoreMana(float Mana)
	{
		manaPoints += Mana;
		if (manaPoints > maxManaPoints) 
			manaPoints = maxManaPoints;

		UpdateGraphics();
	}

	// Update Health and Mana Globes
	private void UpdateGraphics()
	{
		UpdateHealthGlobe();
		UpdateManaGlobe();
	}
}
