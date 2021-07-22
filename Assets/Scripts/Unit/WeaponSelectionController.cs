using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponSelectionController : MonoBehaviour
{
    [Serializable]
    public class PickUpAmmoEvent : UnityEvent<WeaponSelectionController>
    { }

    [SerializeField]
    public Transform WeaponEquipmentBone = null;
    [SerializeField]
    private int _maxSlotsWeapons = 9;
    [SerializeField]
    private UnityEvent _onSelectingWeapon;
    [SerializeField]
    private GameObject[] _slotsWeapons;
    [SerializeField]
    private int[] _ammoWeapons;
    [SerializeField]
    private PickUpAmmoEvent _onPickUpAmmo;

    public delegate void EquipWeaponHandler(int currentSlot, int valueAmmo);
    public EquipWeaponHandler InformationAboutCurrentWeapon;

    public delegate void AmmoWeaponHandler(int valueAmmo);
    public AmmoWeaponHandler AmmoInformation;

    private GameObject _currentWeapon;
    private int _currentSlot=0;
    private Animator _heroAnimator;
    private HeroController _heroController;


    public GameObject CurrentWeapon
    { get { return _currentWeapon; } }

    public GameObject[] SlotsWeapons
    { set { _slotsWeapons = value; }
      get { return _slotsWeapons; } }

    public int[] AmmoWeapons
    { set { _ammoWeapons = value; }
      get { return _ammoWeapons; } }

    public int CurrentSlot
    { set { _currentSlot = value; }
      get { return _currentSlot; } }



    void Awake()
    {
        _slotsWeapons = new GameObject[_maxSlotsWeapons];
        _ammoWeapons = new int[_maxSlotsWeapons];
        _heroAnimator = GetComponent<Animator>();
        _heroController = GetComponent<HeroController>();
        ResetAngleObjectFromWorld(WeaponEquipmentBone.gameObject);
    }

    public void SelectWeapon(float mouseWheelAxis)
    {
        _heroController.UnitAnimator.SetBool("ActiavationRealoadWeaponAnimation", false);
        int step = (int)(mouseWheelAxis * 10f);
        _currentSlot = _currentSlot + step; //10f for axis mouse wheel, because step mouse wheel = 0.1

        if (_currentSlot >= SlotsWeapons.Length)
            _currentSlot = 0;

        if (_currentSlot < 0)
            _currentSlot = SlotsWeapons.Length-1;

        if (SlotsWeapons[_currentSlot] != null)
        {
            _onSelectingWeapon.Invoke();
            SetStateWeaponRack(_currentSlot);
            WeaponEquipment(SlotsWeapons[_currentSlot], WeaponEquipmentBone);
        }

        else
            Debug.Log(_currentSlot + "Current weapon slot is empty");
    }

    public void WeaponEquipment(GameObject nextWeapon, Transform weaponEquipmentBone)
    {
        if (weaponEquipmentBone.transform.childCount != 0)
        {
            var children = (from Transform child in weaponEquipmentBone.transform select child.gameObject).ToList();
            children.ForEach(Destroy);
        }
        nextWeapon = Instantiate(nextWeapon, weaponEquipmentBone);
        _currentWeapon = nextWeapon;
        ResetAngleObjectFromWorld(nextWeapon);
        SetCurrectPositionWeaponInArm(nextWeapon);
        InformationAboutCurrentWeapon(CurrentSlot, AmmoWeapons[CurrentSlot]);
    }

    public void ChangeСurrentValueAmmo(int valueAmmoToChange, int weaponSlot)
    {
        if (AmmoWeapons[weaponSlot] + valueAmmoToChange >= SlotsWeapons[weaponSlot].GetComponent<Weapon>().MaxAmmo)
            AmmoWeapons[weaponSlot] = SlotsWeapons[weaponSlot].GetComponent<Weapon>().MaxAmmo;
        AmmoWeapons[weaponSlot] = AmmoWeapons[weaponSlot] + valueAmmoToChange;
        AmmoInformation(AmmoWeapons[weaponSlot]);
    }


    private void ResetAngleObjectFromWorld(GameObject currentObject)
    {
        Transform oldParent = currentObject.transform.parent;
        currentObject.transform.SetParent(null);
        currentObject.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        currentObject.transform.SetParent(oldParent);
        currentObject.transform.localPosition=Vector3.zero;
    }

    private void SetCurrectPositionWeaponInArm(GameObject weaponInArm)
    {
        Transform muzzleOfWeapon = weaponInArm.GetComponent<Weapon>().MuzzleOfGun;
        GameObject pointForRollWeapon = new GameObject("PointForRollWeapon");
        pointForRollWeapon.transform.SetParent(weaponInArm.transform);
        pointForRollWeapon.transform.localPosition = Vector3.zero;
        Transform oldParentOfMuzzleObject = muzzleOfWeapon.transform.parent;
        muzzleOfWeapon.transform.SetParent(weaponInArm.transform);
        pointForRollWeapon.transform.localPosition = new Vector3(0f, muzzleOfWeapon.localPosition.y, 0f);
        pointForRollWeapon.AddComponent<MovingForMouse>().IsHandOrWeapon = true;
        muzzleOfWeapon.transform.SetParent(oldParentOfMuzzleObject);
        weaponInArm.transform.GetChild(0).SetParent(pointForRollWeapon.transform);
    }

    public void SetStateWeaponRack(float weaponSlot)
    {
        _heroAnimator.SetFloat("weaponSlot", weaponSlot);
    }
}
