using UnityEngine;

public class CameraMovementController : MonoBehaviour
{
    [SerializeField]
    private Transform _target = null;
    [SerializeField]
    private float _smoothingVector = 1000f;

    // Update is called once per frame
    void Update()
    {
        //transform.eulerAngles = new Vector3(transform.rotation.x, _smoothingRotateCamera * Input.GetAxis("Horizontal"), transform.rotation.z);
        Vector3 DestPoint = new Vector3(_target.position.x, _target.position.y, -13);
        transform.position = Vector3.Lerp(transform.position, DestPoint, Time.deltaTime * _smoothingVector);
    }
}
