using UnityEngine;

public class PositionLock : MonoBehaviour
{
    [SerializeField] private bool _lockX;
    [SerializeField] private bool _lockY;
    [SerializeField] private bool _lockZ;

    private Vector3 _defaultPosition;

    private void Awake()
    {
        _defaultPosition = transform.localPosition;
    }

    private void Update()
    {
        var position = transform.localPosition;
        transform.localPosition = new Vector3(
            _lockX ? _defaultPosition.x : position.x,
            _lockY ? _defaultPosition.y : position.y,
            _lockZ ? _defaultPosition.z : position.y);
    }
}