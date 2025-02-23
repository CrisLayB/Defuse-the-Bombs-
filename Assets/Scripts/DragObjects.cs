
using UnityEngine;

public class DragObjects : MonoBehaviour
{
    [SerializeField] private LayerMask _draggableMask;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private GameObject _dragDialog;
    [SerializeField] private float _moveSpeed = 10f;
    private Rigidbody _selectedObject;

    void Update()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        bool isHit = Physics.Raycast(ray, out hit, 4.5f, _draggableMask);
        if (isHit && _selectedObject == null)
            _dragDialog?.SetActive(true);
        else 
            _dragDialog?.SetActive(false);
        

        if (Input.GetMouseButtonDown(0)) {
            if (isHit) {
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
