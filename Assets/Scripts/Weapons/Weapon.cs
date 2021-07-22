using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(AudioClip))]

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    private int _weaponSlot = 0; //Weapon slot in the inventory (using PickUpWeapon.cs)
    [SerializeField]
    private int _maxAmmo = 999;
    [SerializeField]
    private Sprite _uiIconAmmoSprite;
    [SerializeField]
    protected float fireRate = 1f;
    [SerializeField]
    private float _bulletScatter = 1.5f;
    [SerializeField]
    protected int numberOfBullets = 1;
    [SerializeField]
    protected int reduceNumberBulletsPerShot = 1;
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected UnityEvent actionAfterShot;
    [SerializeField]
    private float _alternativeFireRate = 100f;
    [SerializeField]
    private float _alternativeBulletScatter = 1.5f;
    [SerializeField]
    protected int alternativeNumberOfBullets = 1;
    [SerializeField]
    protected int alternativeReduceNumberBulletsPerShot = 1;
    [SerializeField]
    protected GameObject alternativeBulletPrefab;
    [SerializeField]
    protected UnityEvent actionAfterAlternativeShot;
    [SerializeField]                      // It is worth creating an empty object and placing it
    private Transform _muzzleOfGun = null; // at the end of the trunk of the texture. It object is "_muzzleOfGun"
    [SerializeField]
    private float _cameraShakeDuration = 0.3f;
    [SerializeField]
    private float _cameraShakePower = 0.4f;
    [SerializeField]
    protected string folderEventSoundShot;
    [SerializeField]
    protected string alternatineFolderEventSoundShot;

    private float _delayToShot;
    private float _currentTimeToStartShoot;
    private Animator _weaponAnimator;

    private WeaponSelectionController _heroSelectionWeaponController;
    protected WeaponController weaponController;
    private GameObject _heroObject;
    private HeroController _heroController;



    public Transform MuzzleOfGun
    { set { value = _muzzleOfGun; }
        get { return _muzzleOfGun; } }

    public Sprite UIIconAmmoSprite
    { get { return _uiIconAmmoSprite; } }

    public int WeaponSlot
    { get { return _weaponSlot; } }

    public int MaxAmmo
    { get { return _maxAmmo; } }

    public GameObject HeroObject
    { get { return _heroObject; } }

    public WeaponSelectionController HeroSelectionWeaponController
    { get { return _heroSelectionWeaponController; } }

    protected float alternativeFireRate
    { get { return _alternativeFireRate; } }

    protected float bulletScatter
    { get { return _bulletScatter; } }

    protected float alternativeBulletScatter
    { get { return _alternativeBulletScatter; } }

    protected float cameraShakeDuration
    { get { return _cameraShakeDuration; } }

    protected float cameraShakePower
    { get { return _cameraShakePower; } }

    protected HeroController heroController
    { get { return _heroController; } }

    protected float currentTimeToStartShoot
    { get { return _currentTimeToStartShoot; } }

    protected Animator weaponAnimator
    { get { return _weaponAnimator; } }



    private void Awake()
    {
        _delayToShot = Mathf.Max(CalculationDelayTimeBetweenShots(fireRate), CalculationDelayTimeBetweenShots(_alternativeFireRate));
        _currentTimeToStartShoot = _delayToShot+Time.deltaTime;
        _heroObject = transform.root.gameObject;
        _heroController = _heroObject.GetComponent<HeroController>();
        _heroSelectionWeaponController = _heroObject.GetComponent<WeaponSelectionController>();
        _weaponAnimator = GetComponent<Animator>();  
    }

    //Main method for shoting
    protected virtual void StartShooting(ref int ammo)
    {
        if (Input.GetButton("Fire1") && !Input.GetButton("Fire2") && ammo > 0)
        {
            Shoot(CalculationDelayTimeBetweenShots(fireRate), reduceNumberBulletsPerShot);
        }

        if (Input.GetButton("Fire2") && !Input.GetButton("Fire1") && ammo > 0)
        {
            AlternativeShoot(CalculationDelayTimeBetweenShots(_alternativeFireRate), alternativeReduceNumberBulletsPerShot);
        }
    }

    protected virtual void Shoot(float timeToStartShoot, int reduceNumberAmmoPerShot)
    {
        if (_currentTimeToStartShoot > timeToStartShoot && HeroSelectionWeaponController.AmmoWeapons[WeaponSlot] > 0)
        {
            Shot(bulletPrefab, numberOfBullets, _bulletScatter,fireRate, actionAfterShot,folderEventSoundShot);
            _heroSelectionWeaponController.ChangeСurrentValueAmmo(-reduceNumberAmmoPerShot, WeaponSlot);
        }
    }

    protected virtual void AlternativeShoot(float timeToStartShoot, int reduceNumberAmmoPerShot)
    {
        if (_currentTimeToStartShoot > timeToStartShoot && HeroSelectionWeaponController.AmmoWeapons[WeaponSlot] > 0)
        {
            Shot(alternativeBulletPrefab, alternativeNumberOfBullets, _alternativeBulletScatter, _alternativeFireRate, actionAfterAlternativeShot,alternatineFolderEventSoundShot);
            _heroSelectionWeaponController.ChangeСurrentValueAmmo(-reduceNumberAmmoPerShot, WeaponSlot);
        }
    }

    protected void Shot(GameObject bullet, int numberOfBullets, float bulletScatterOnShot,float fireRate, UnityEvent actionAfterShot, string folderEventSound)
    {
        _currentTimeToStartShoot = 0;
        bullet.GetComponent<Bullet>().InstantiateBullets(bullet, numberOfBullets, bulletScatterOnShot, _muzzleOfGun.transform, _heroController.GetComponent<Transform>());
 
        CameraShake.Shake(_cameraShakeDuration, _cameraShakePower, CameraShake.ShakeMode.OnlyX);
        actionAfterShot.Invoke();
        StartCoroutine(CalculateTimeToShot(fireRate));
        FMODUnity.RuntimeManager.PlayOneShot(folderEventSound);
    }

    //This method is needed for the delay between shots and for single shooting
    private IEnumerator CalculateTimeToShot(float fireRate)
    {
        yield return new WaitForSeconds(CalculationDelayTimeBetweenShots(fireRate));
        _currentTimeToStartShoot = _delayToShot + Time.deltaTime;
    }

    protected float CalculationDelayTimeBetweenShots(float fireRate) 
    {
        if(fireRate==0)
            fireRate=1;
        return 60 / fireRate;
    }

    public void PlayingHeroReloadAnimation() //Use in unity event of the weapon
    {
        StartCoroutine(heroController.StartPlayingReloadHeroAnimation());
    }

    
}
