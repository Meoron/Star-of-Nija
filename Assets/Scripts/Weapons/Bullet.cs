using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(DamageController))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 35.0F;
    [SerializeField]
    private string _folderHitSoundFMOD;

    private static float _bulletScatter=0;
    private static Transform _unitTransform;
    private Rigidbody2D _rigidbody;
    private DamageController _damageController;
 


    private void Awake()
    {
        gameObject.layer = 14; // Set layer "Bullet";
        _rigidbody = GetComponent<Rigidbody2D>();
        _damageController = GetComponent<DamageController>();
        _damageController.IsBullet = true;
    }

    private void Start()
    {
        _damageController.EventAfterHit += StopBullet;
        _damageController.EventAfterTakeDamage += PlaySoundAfterHit;
        AddForceBullet(_bulletScatter, _speed, _unitTransform.localScale.x);
    }

    public void InstantiateBullets(GameObject bulletPrefab, int countOfBullet, float scatterOnShot, Transform muzzleOfGun, Transform heroTransform)
    {
        for (int i = 0; i < countOfBullet; i++)
        {
            _bulletScatter = scatterOnShot;
            _unitTransform = heroTransform;
            GameObject bullet = Instantiate(bulletPrefab, muzzleOfGun.position, muzzleOfGun.rotation);
            Destroy(bullet, 2F);
        }
    }

    private void AddForceBullet(float scatter, float speedBullet, float scaleXOfHero)
    {
        _rigidbody.velocity = transform.right * scaleXOfHero * speedBullet + transform.up* Random.Range(-scatter, scatter);
    }

    private void StopBullet()
    {
        _rigidbody.Sleep();
        _damageController.EventAfterHit -= StopBullet;
    }

    private void PlaySoundAfterHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(_folderHitSoundFMOD);
    }
}
