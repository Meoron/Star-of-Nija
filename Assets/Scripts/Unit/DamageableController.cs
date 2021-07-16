using System;
using UnityEngine;
using UnityEngine.Events;


public class DamageableController : MonoBehaviour
{
    [Serializable]
    public class HealthEvent : UnityEvent<DamageableController>
    { }

    [Serializable]
    public class DamageEvent : UnityEvent<DamageController, DamageableController>
    { }

    [Serializable]
    public class HealEvent : UnityEvent<int, DamageableController>
    { }

    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _currentHealth;
    [Tooltip("An _offset from the obejct position used to set from where the distance to the damager is computed")]
    [SerializeField]
    private Vector2 _centreOffset = new Vector2(0f, 0f);
    [SerializeField]
    private GameObject _vfxOnDie;

    private Vector2 _damageDirection;
    
    public HealthEvent OnHealthSet;
    public DamageEvent OnTakeDamage;
    public DamageEvent OnDie;
    public HealEvent OnGainHealth;

    public event Action<float> ChangeValueHealth = delegate { };


    public int MaxHealth
    { get { return _maxHealth; } }

    public int CurrentHealth
    { get { return _currentHealth; } }



    void OnEnable()
    {
        _currentHealth = _maxHealth;

        OnHealthSet.Invoke(this);
    }

    public Vector2 GetDamageDirection()
    {
        return _damageDirection;
    }

    public void TakeDamage(DamageController damageController)
    {
        _currentHealth -= damageController.Damage;
        ChangeValueHealth(_currentHealth);
        OnHealthSet.Invoke(this);

        _damageDirection = transform.position + (Vector3)_centreOffset - damageController.transform.position;

        if (_currentHealth<=0)
        {
            Die(damageController);
        }
    }

    public void GainHealth(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;

        ChangeValueHealth(_currentHealth);
        OnHealthSet.Invoke(this);
        OnGainHealth.Invoke(amount, this);
    }

    public void Die(DamageController damageController)
    {
        ChangeValueHealth(0f);
        GameObject tempVisualObj = Instantiate(_vfxOnDie);
        tempVisualObj.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+0.6f,tempVisualObj.transform.position.z);
        tempVisualObj.transform.rotation = Quaternion.Euler(0f,0f,0f);
        OnDie.Invoke(damageController, this);
        Destroy(gameObject);
        Destroy(tempVisualObj, 0.5f);
    }
}
