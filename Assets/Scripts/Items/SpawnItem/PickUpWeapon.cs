using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PolygonCollider2D))]
public class PickUpWeapon : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer texture;
    [SerializeField]
    private GameObject weaponPrefab;
    [SerializeField]
    private UnityEvent actionsBeforeTake;
    [SerializeField]
    private UnityEvent actionsAfterTake;
    [SerializeField]
    private float effectsDuration = 1f;
    [SerializeField]
    private int minAmmo = 30;



    private void Start()
    {
        actionsBeforeTake.Invoke();
        if (gameObject.GetComponent<SpriteRenderer>() != null)
            texture = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<HeroController>())
        {
            Animator heroAnimator = collider.gameObject.GetComponent<Animator>();
            Weapon weapon = weaponPrefab.GetComponent<Weapon>();
            WeaponSelectionController heroWeaponSelectionController = collider.gameObject.GetComponent<WeaponSelectionController>();

            
            heroWeaponSelectionController.CurrentSlot = weapon.WeaponSlot; //Add numer slot of weapon in current slot
            heroWeaponSelectionController.AmmoWeapons[weapon.WeaponSlot] += minAmmo;
            heroWeaponSelectionController.SlotsWeapons[weapon.WeaponSlot] = weaponPrefab; //Add in slot weapon in inventory base data
            heroWeaponSelectionController.WeaponEquipment(weaponPrefab, heroWeaponSelectionController.WeaponEquipmentBone); //Equip weapon

            heroAnimator.SetFloat("weaponSlot", weapon.WeaponSlot); //Set rack with weapon
            
            transform.Find("VFXAfterTake").gameObject.SetActive(true);
            transform.Find("VFXBeforeTake").gameObject.SetActive(false);

            actionsAfterTake.Invoke();
            StartCoroutine("DestroyObject");
        }
    }

    private IEnumerator DestroyObject()
    {
        texture.enabled = false;
        yield return new WaitForSeconds(effectsDuration);
        Destroy(gameObject);
    }
}
