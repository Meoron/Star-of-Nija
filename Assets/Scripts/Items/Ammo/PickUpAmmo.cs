using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpAmmo : MonoBehaviour
{
    [SerializeField]
    private GameObject _weaponPrefab; //The weapon slot for which these ammo are intended
    [SerializeField]
    private int _ammoAmount = 30;
    [SerializeField]
    private float _effectsDuration = 1f; //Visual effects duration after pick up
    [SerializeField]
    private UnityEvent _onGivingAmmo;

    private Weapon _weapon;

    private void Start()
    {
        _weapon = _weaponPrefab.GetComponent<Weapon>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<WeaponSelectionController>())
        {
            WeaponSelectionController heroWeaponController = other.gameObject.GetComponent<WeaponSelectionController>();
            if (heroWeaponController.AmmoWeapons[_weapon.WeaponSlot] != _weapon.MaxAmmo)
            {
                    heroWeaponController.ChangeСurrentValueAmmo(_ammoAmount, _weapon.WeaponSlot);
                    _onGivingAmmo.Invoke();
                    StartCoroutine("DestroyObject");
            }
            transform.Find("Texture").gameObject.SetActive(false);
            transform.Find("VFX").gameObject.SetActive(true);
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(_effectsDuration);
        Destroy(gameObject);
    }
}
