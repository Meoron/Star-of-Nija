using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeroController))]
public class UserController : MonoBehaviour
{
    private HeroController heroController;
    private WeaponSelectionController weaponSelectionControler;

    // Start is called before the first frame update
    private void Awake()
    {
        heroController = GetComponent<HeroController>();
        weaponSelectionControler = GetComponent<WeaponSelectionController>();
    }

    private void FixedUpdate()
    {
        heroController.SetStateAnimation(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            heroController.Jump();

        if (Input.GetButton("Horizontal") && Input.GetButton("Vertical") == false)
            heroController.Moving(Input.GetAxis("Horizontal"), heroController.Speed);

        if (Input.GetAxis("Vertical")<0)
            heroController.Crouch(Input.GetAxis("Horizontal"));

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            weaponSelectionControler.SelectWeapon(Input.GetAxis("Mouse ScrollWheel"));
    }
}
