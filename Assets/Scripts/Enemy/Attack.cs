using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject castPoint;
    public GameObject spell;

    private float spellRange = 10f;
    private float spellSpeed = 10f;

    private Ray enemySight;
    private RaycastHit rayHit;

    public void AttackPlayer(GameObject player)
    {
        enemySight = new Ray(transform.position, player.transform.position - transform.position);

        if (Physics.Raycast(enemySight, out rayHit, spellRange))
        {
            if (rayHit.collider.gameObject.tag == "Player" && rayHit.collider.gameObject.tag != "Enemy")
            {
             var spellObject = Instantiate(spell, castPoint.transform.position, Quaternion.identity) as GameObject;
             spellObject.GetComponent<Rigidbody>().velocity = (enemySight.GetPoint(spellRange) - castPoint.transform.position).normalized * spellSpeed;
            }
        }
    }
}
