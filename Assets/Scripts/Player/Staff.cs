using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour
{
    public GameObject castPoint;
    public GameObject spell;

    private float fireRate = 2f;
    private float timer = 1f;
    private float spellRange = 15f;
    private float spellSpeed = 10f;
    private float spellCost = 15f;

    Ray ray = new Ray();

    void Update()
    {
        timer += Time.deltaTime; // Used for cooldown of attacks (stop spamming)
        
        if (timer >= fireRate)
        {
            // Cast spell and reset timer/charged force on fire up
            if (Input.GetButtonUp("Fire1") && HealthSystem.Instance.manaPoints > spellCost)
            {
                CastSpell();
                HealthSystem.Instance.UseMana(spellCost);
                timer = 0f;
            }
        }
    }

    // Spawn spell projectile
    private void CastSpell() 
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (castPoint != null)
        {
            var spellObject = Instantiate(spell, castPoint.transform.position, Quaternion.identity) as GameObject;
            spellObject.GetComponent<Rigidbody>().velocity = (ray.GetPoint(spellRange) - castPoint.transform.position).normalized * spellSpeed;
        }
    }
}
