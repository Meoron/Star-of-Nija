using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MovingForMouse : MonoBehaviour
{
    [SerializeField]
    private float _offset = 0f;
    [SerializeField]
    private bool _isHandOrWeapon = false;

    private Transform _pointOfObjForRoll = null; //Use to rotate the object beyond another point and not beyond the center. 
                                                 //It is worth creating a dummy, and child objects that I will rotate, attach to the dumz
    private float _divider;
    private float _increaseInOffset;
    private AimOnGUI _aimOnGUI;
    private GameObject _aimPrefab;
    private Transform _unitTransform;
    private HeroController _heroController;
    private Vector3 _pastEleuerAngleObject;

    public Vector3 MousePositionOnCamera;
    
    public bool IsHandOrWeapon { set { _isHandOrWeapon = value; } }

    private void Awake()
    {
        _aimPrefab = GameObject.Find("Aim");
        _aimOnGUI = _aimPrefab.GetComponent<AimOnGUI>();
    }

    private void Start()
    {
        _pastEleuerAngleObject = transform.eulerAngles;
        _heroController = (HeroController)FindObjectOfType(typeof(HeroController));
        _unitTransform = _heroController.gameObject.transform;
        _pointOfObjForRoll = transform;
    }

    private void FixedUpdate()
    { 
        MousePositionOnCamera = _aimOnGUI.MouseWorldPosition - _pointOfObjForRoll.position;
        MovingBone(MousePositionOnCamera, transform);
    }

    private void MovingBone (Vector3 mousePositionOnCamera, Transform objectForRoll)
    {
        if (_isHandOrWeapon == true)
        {
            if (_unitTransform.localScale.x < 0)
            {
                _increaseInOffset = 180f;
            }
            else
                _increaseInOffset = 0f;
        }
        var angle = Vector2.Angle(Vector2.right, mousePositionOnCamera);//Angle between the vector from the object to the mouse and the x-axis
        objectForRoll.eulerAngles = new Vector3(0f, 0f, mousePositionOnCamera.y>0f ? (angle + _offset+ _increaseInOffset) : (-angle + _offset+ _increaseInOffset)) + _pastEleuerAngleObject;
    }
}

