using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendlerInformationInUI : MonoBehaviour
{
    [SerializeField]
    private Image _activeHealthBar;
    [SerializeField]
    private Image _uiAmmoIconBar;
    [SerializeField]
    private Text _uiAmmoTextBar;
    [SerializeField]
    private float _updateHealthPerSecond = 0.2f;

    private GameObject weapon;

    private DamageableController _damageableController;
    private WeaponSelectionController _weaponSelectionController;
    // Start is called before the first frame update
    void Start()
    {
        _damageableController = GetComponent<DamageableController>();
        _weaponSelectionController = GetComponent<WeaponSelectionController>();
        _weaponSelectionController.InformationAboutCurrentWeapon += ReceivingInformationAboutAmmoInUI;
        _weaponSelectionController.AmmoInformation += SendInUIValueAmmo;
        _damageableController.ChangeValueHealth += HandleHealthChanged;
        _activeHealthBar.fillAmount = 1f;
    }

    private void HandleHealthChanged (float _currentHealth)
    {
        StartCoroutine(ChangedPictureActiveHealthBarOnUI(_currentHealth));
    }

    private IEnumerator ChangedPictureActiveHealthBarOnUI(float _currentHealth)
    {
        float preChagedFillAmount=_activeHealthBar.fillAmount;
        float timeToChengeFillAmount=0f;
        float nextValueFillAmount = ((_currentHealth * 100) / _damageableController.MaxHealth)/100;
        while(timeToChengeFillAmount < _updateHealthPerSecond)
        {
            timeToChengeFillAmount += Time.deltaTime;
            _activeHealthBar.fillAmount = Mathf.Lerp(preChagedFillAmount, nextValueFillAmount, timeToChengeFillAmount / _updateHealthPerSecond);
            yield return null;
        }
    }

    private void ReceivingInformationAboutAmmoInUI(int currentSlotWeapon, int currentAmmoValue)
    {
        weapon = GetComponent<WeaponSelectionController>().SlotsWeapons[currentSlotWeapon];
        _uiAmmoIconBar.sprite = weapon.GetComponent<Weapon>().UIIconAmmoSprite;
        _uiAmmoIconBar.color = new Color(_uiAmmoIconBar.color.r, _uiAmmoIconBar.color.g, _uiAmmoIconBar.color.b, 1f);
        SendInUIValueAmmo(currentAmmoValue);
    }

    private void SendInUIValueAmmo(int ammoValue)
    {
        _uiAmmoTextBar.text = ammoValue.ToString();
    }

}
