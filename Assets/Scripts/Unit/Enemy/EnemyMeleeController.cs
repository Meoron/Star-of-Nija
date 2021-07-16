using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeWeapon;
    //[SerializeField]
    //private float timeBetweenAttacks = 0f;      //Adjusts the moment the area in which damage is applied is turned on
    [SerializeField]
    private float waitingMomentAttack = 1f;       //Moment attack on animation
    [SerializeField]
    private float waitingTimeBeforeMoving = 1f;     //So that at the right moment of the attack animation, the unit can move

    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        meleeWeapon.GetComponent<DamageController>().DisableDamage();
    }

    public IEnumerator MeleeAttack()
    {
        if (enemyController.DistanceToPlayer < enemyController.DistanceToMeleeAttack)
        {
            enemyController.UnitAnimator.SetFloat("AnimationMovingBlendTree", 0);
            enemyController.UnitAnimator.Play("MeleeAttack");
            yield return new WaitForSecondsRealtime(waitingMomentAttack); //Moment attack on animation
            meleeWeapon.GetComponent<DamageController>().EnableDamage();
            yield return new WaitForSeconds(waitingTimeBeforeMoving);
            //yield return new WaitForSeconds(timeBetweenAttacks - waitingMomentAttack - waitingTimeBeforeMoving);
            meleeWeapon.GetComponent<DamageController>().DisableDamage();

        }
    }
}
