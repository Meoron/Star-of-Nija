using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeathDrug : MonoBehaviour
{
    [SerializeField]
    private int healthAmount = 10;
    [SerializeField]
    private float effectsDuration = 1f;
    [SerializeField]
    private UnityEvent OnGivingHealth = null;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<HeroController>())
        {
            DamageableController damageable = other.gameObject.GetComponent<HeroController>().DamageableController;
            if (damageable.CurrentHealth < damageable.MaxHealth)
            {
                damageable.GainHealth(Mathf.Min(healthAmount, damageable.MaxHealth - damageable.CurrentHealth));
                OnGivingHealth.Invoke();
                StartCoroutine("DestroyObject");
            }
        }
    }

    private IEnumerator DestroyObject ()
    {
        yield return new WaitForSeconds(effectsDuration);
        Destroy(gameObject);
    }
}
