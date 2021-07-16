using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RotationForTarget : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float offset = 0f;

    private Transform targetTransform;
    private Vector3 targetPositionRegardingObject;

    private void Start()
    {
        targetTransform = target.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        targetPositionRegardingObject = targetTransform.position - transform.position;
        Rotation(targetPositionRegardingObject);
    }

    private void Rotation(Vector3 targetPositionOnWorld)
    {
        var angle = Vector2.Angle(Vector2.right, targetPositionOnWorld);//угол между вектором от объекта к мыше и осью х
        transform.eulerAngles = new Vector3(0f, 0f, targetPositionOnWorld.y > 0f ? (angle + offset) : (-angle + offset));//немного магии на последок
    }
}
