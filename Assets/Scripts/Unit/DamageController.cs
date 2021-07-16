using System;
using UnityEngine;
using UnityEngine.Events;

public class DamageController : MonoBehaviour
{
    [Serializable]
    private class DamagableEvent : UnityEvent<DamageController, DamageableController>
    { }


    [Serializable]
    private class NonDamagableEvent : UnityEvent<DamageController>
    { }

    //call that from inside the onDamageableHIt or OnNonDamageableHit to get what was hit.
    //public Collider2D LastHit { get { return _lastHit; } }
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private Vector2 _offset = new Vector2(0f, 0f);
    [SerializeField]
    private Vector2 _size = new Vector2(2.5f, 1f);
    [SerializeField]
    private LayerMask _hittableLayers;
    [Tooltip("If disabled, damager ignore trigger when casting for damage")]
    [SerializeField]
    private bool _canHitTriggers;
    [SerializeField]
    private bool _disableDamageAfterHit = false;
    [SerializeField]
    private DamagableEvent _onDamageableHit;
    [SerializeField]
    private NonDamagableEvent _onNonDamageableHit;
    private ContactFilter2D _attackContactFilter;

    private Collider2D[] _attackOverlapResults = new Collider2D[10];

    private bool _isBullet = false;
    private bool canDamage = true;
    private Vector2 _scale;
    private Vector2 _facingOffset;
    private Vector2 _scaledSize;

    public delegate void DamageHandler();
    public DamageHandler EventAfterHit;
    public DamageHandler EventAfterTakeDamage;

    public bool IsBullet
       {set{_isBullet = value;} }

    public int Damage
    { get { return _damage; } }

    public Vector2 Offset
    {   set { _size = value; }
        get { return _offset; } }

    public Vector2 Size
    {   set { _size = value; }
        get { return _size; } }

    public ContactFilter2D AttackContactFilter
    { get { return _attackContactFilter; } }

    public bool CanDamage
    { get { return canDamage; } }



    void Awake()
    {
        _attackContactFilter.layerMask = _hittableLayers;
        _attackContactFilter.useLayerMask = true;
        _attackContactFilter.useTriggers = _canHitTriggers;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CalculatingSizeDamageField();
        if (!canDamage)
            return;
        Vector2 pointA = (Vector2)transform.position + _facingOffset + _scaledSize / 2;
        Vector2 pointB = (Vector2)transform.position + _facingOffset - _scaledSize / 2;

        Debug.DrawLine(pointA, pointB, Color.white);
        int hitCount;
        hitCount = Physics2D.OverlapArea(pointA, pointB, _attackContactFilter, _attackOverlapResults);
        for (int i = 0; i < hitCount; i++)
        {
            DamageableController damageable = _attackOverlapResults[i].GetComponent<DamageableController>();
            EventAfterHit?.Invoke();

            if (damageable)
            {
                _onDamageableHit.Invoke(this, damageable);
                damageable.TakeDamage(this);
                EventAfterTakeDamage?.Invoke();
                if (_disableDamageAfterHit)
                    DisableDamage();
                if (_isBullet)
                {
                    gameObject.GetComponent<DamageController>().enabled = false;
                    return;
                }
            }
       
            else
                _onNonDamageableHit.Invoke(this);
            
        }
        
    }



    private void CalculatingSizeDamageField()
    {
       _scale = transform.lossyScale;
       _facingOffset = Vector2.Scale(_offset, _scale);
       _scaledSize = Vector2.Scale(_size, _scale);
    }

    public void EnableDamage()
    {
        canDamage = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
    }
}
