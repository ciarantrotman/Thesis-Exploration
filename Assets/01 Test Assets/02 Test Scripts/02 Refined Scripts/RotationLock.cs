using UnityEngine;

public class RotationLock : MonoBehaviour
{
    [SerializeField] private bool _lockX;
    [SerializeField] private bool _lockY;
    [SerializeField] private bool _lockZ;

    private Vector3 _defaultRotation;

    private void Awake()
    {
        _defaultRotation = transform.eulerAngles;
    }

    private void Update()
    {
        var rotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(
            _lockX ? _defaultRotation.x : rotation.x,
            _lockY ? _defaultRotation.y : rotation.y,
            _lockZ ? _defaultRotation.z : rotation.y);
    }
}