using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellSolder : EnemyController, IHaveAIUnit, IMovable, IAttackedMelee
{
    [SerializeField]
    protected float _speed = 1.0F;
    [SerializeField]
    private GameObject _objectHavingDamageControllerInUnit;
    [SerializeField]
    private float _meleeAttackDistance=1f;

    private void Start()
    {
        if (_objectHavingDamageControllerInUnit == null)
            _objectHavingDamageControllerInUnit = gameObject;
        DisableMeleeDamageController();
    }

    private void FixedUpdate()
    {
        DistanceDeterminationToPlayer();
        SetAIUnit();
    }

    public void SetAIUnit()
    {
        if (distanceToPlayer < _meleeAttackDistance && player.GetComponent<DamageableController>().CurrentHealth >0)
            stateAI = "meleeAttack";
        LookingForPlayer();
        switch (stateAI)
        {
            case "followToPlayer":
                {
                    FollowToPlayer();
                    break;
                }
            case "meleeAttack":
                {
                    StartCoroutine(MeleeAttack());
                    break;
                }
        }
    }

    public void FollowToPlayer()
    {
        Vector3 direction = GetNormalizedDirectionToPlayer();
        Moving(direction);
    }

    public void Moving(Vector3 direction)
    {
        _unitAnimator.SetFloat("AnimationMovingBlendTree", direction.normalized.x);
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, Time.deltaTime * _speed);
        RollUnit(direction.normalized.x);
    }

    public IEnumerator MeleeAttack()
    {
        _unitAnimator.SetFloat("AnimationMovingBlendTree", 0);
        _unitAnimator.Play("MeleeAttack");
        yield return null;
    }

    private void EnableMeleeDamageController() //Use in animation clip event "MeleeAttack"
    {
        _objectHavingDamageControllerInUnit.GetComponent<DamageController>().EnableDamage(true);
    }

    private void DisableMeleeDamageController() //Use in animation clip event "MeleeAttack"
    {
        _objectHavingDamageControllerInUnit.GetComponent<DamageController>().EnableDamage(false);
    }

    private Vector3 GetNormalizedDirectionToPlayer()
    {
        direction = transform.right * (player.transform.position.x - transform.position.x);
        return direction.normalized;
    }

    private void LookingForPlayer()
    {
        if (distanceToPlayer <= viewRadius && stateAI == null)
            stateAI = "followToPlayer";
    }

    private void SetAIStateInAnimationEvent(string state) //Use in animation clip event MeleeAttack and RangeAttack 
    {
        stateAI = state;
    }
}
