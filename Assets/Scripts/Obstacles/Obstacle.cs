using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void FixedUpdate()
    {
        //StartCoroutine (OnTriggerEnter2D(collider));
    }
    /*
    private IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
       Unit unit = collider.GetComponent<Unit>();
       if (unit && unit is CharacterControls)
        {
           unit.ReceiveDamage();
           yield return new WaitForSeconds(0.5f);
        }
       yield return OnTriggerEnter2D(collider);
    }
    */
}
