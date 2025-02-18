
using UnityEngine;

public class DragObjects : MonoBehaviour
{
    [SerializeField] private LayerMask _draggableMask;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _moveSpeed = 10f;
    private Rigidbody _selectedObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 4.5f, _draggableMask))
            {
                _selectedObject = hit.rigidbody;
                _selectedObject.useGravity = false;
            }
        }

        if (_selectedObject) {
            Vector3 targetPosition = _cameraTransform.position + _cameraTransform.forward * 2.5f;
            Vector3 direction = targetPosition - _selectedObject.position;
            _selectedObject.velocity = direction * _moveSpeed;
        }

        if (Input.GetMouseButtonUp(0)) {
            if (_selectedObject) {
                _selectedObject.useGravity = true;
                // _selectedObject.velocity = Vector3.zero;
                _selectedObject = null;
            }
        }
    }
}
