using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberDevil : EnemyController, IHaveAIUnit, IMovable, IAttackedMelee, IAttackedRange
{
    [SerializeField]
    protected float _speed = 1.0F;
    [SerializeField]
    private GameObject _objectHavingDamageControllerInUnit;
    [SerializeField]
    private float _meleeAttackDistance = 1f;
    [SerializeField]
    private float _rangeAttackDistance = 10f;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private Transform _muzzleOfRangeWeapon;
    [SerializeField]
    private float _speedBullet = 20f;

    private bool _canRangeHit = true;

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
        LookingForPlayer();

        if (distanceToPlayer < _meleeAttackDistance && player.GetComponent<DamageableController>().CurrentHealth > 0)
            stateAI = "meleeAttack";
        if (_canRangeHit && distanceToPlayer < _rangeAttackDistance && distanceToPlayer > _meleeAttackDistance && player.GetComponent<DamageableController>().CurrentHealth > 0)
            stateAI = "rangeAttack";
        
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
            case "rangeAttack":
                {
                    _canRangeHit = false;
                    StartCoroutine(RangeAttack());
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

    public IEnumerator RangeAttack()
    {
        RollUnit(GetNormalizedDirectionToPlayer().x);
        _unitAnimator.Play("RangeAttack");
        yield return new WaitForSeconds(5);
        _canRangeHit = true;
    }

    public void Shot() //Use in animation clip event "RangeAttack"
    {
        ResetRotationMuzzleWeapon();
        GameObject bullet = Instantiate(_bulletPrefab, _muzzleOfRangeWeapon.position, _bulletPrefab.transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * transform.localScale.x * _speedBullet;
        Destroy(bullet, 3f);
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

    private void ResetRotationMuzzleWeapon()
    {
        Transform pastMuzzleParent = _muzzleOfRangeWeapon.transform.parent;
        _muzzleOfRangeWeapon.transform.parent = null;
        _muzzleOfRangeWeapon.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _muzzleOfRangeWeapon.transform.parent = pastMuzzleParent;
    }

    private void SetAIStateInAnimationEvent(string state) //Use in animation clip event MeleeAttack and RangeAttack 
    {
        stateAI = state;
    }
}
