using UnityEngine;

public class UserController : MonoBehaviour
{
    private IControlable controlableObject;
    //private WeaponSelectionController weaponSelectionControler;

    private void Awake(){
        var lastControlledObject = Resources.Load<GameObject>("Prefabs/Units/Character/DoomSlayer");
        IControlable controlledObject = Instantiate(lastControlledObject).GetComponent<IControlable>();
        Initialize(controlledObject);
    }

    // Start is called before the first frame update
    private void Initialize(IControlable controlledObject){
        controlableObject = controlledObject;
        
        //weaponSelectionControler = GetComponent<WeaponSelectionController>();
    }

    private void FixedUpdate()
    {
        //heroController.SetStateMovingAnimation(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    // Update is called once per frame
    private void Update()
    {
        //controlableObject.Control();
        /*if (Input.GetButtonDown("Jump"))
            heroController.Jump();

        if (Input.GetButton("Horizontal") && Input.GetButton("Vertical") == false)
            heroController.MovingInUserDirection(Input.GetAxis("Horizontal"));

        if (Input.GetAxis("Vertical")<0)
            heroController.Crouch(Input.GetAxis("Horizontal"));

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            weaponSelectionControler.SelectWeapon(Input.GetAxis("Mouse ScrollWheel"));*/
    }
}
