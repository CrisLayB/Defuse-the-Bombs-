using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowBomb : MonoBehaviour
{
    [Header("Parabola Configuration")]
    [SerializeField] private int _parabolaResolution = 30;
    [SerializeField] private float _throwAngle = 45f;
    [SerializeField] private float _throwPower = 10f;
    [SerializeField] private float _horizontalRange = 2f;
    [SerializeField] private float _verticalRange = 20f;

    [Space(1)]
    [Header("Bomb Throwing Configuration")]
    [SerializeField] private KeyCode _shootKey = KeyCode.Space;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _throwForce = 10f;

    [Space(1)]
    [Header("Unity Events")]
    [SerializeField] private UnityEvent _lauchedEvent;
    [SerializeField] private UnityEvent _winEvent;

    private bool _isThrowed = false;
    private Rigidbody _rb;
    private float _horizontalOffset = 0f;
    private float _verticalOffset = 0f;
    private Vector3[] _trajectoryPoints;
    private bool _winnedCounted = false;

    private void Start() 
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        _trajectoryPoints = new Vector3[_parabolaResolution];
        DrawParabola();
    }

    void Update()
    {
        if(!_isThrowed)
        {
            AdjustParabolaWithMouse();
            CheckForThrow();
        }
    }

    private void AdjustParabolaWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _horizontalOffset += mouseX * _horizontalRange * Time.deltaTime;
        _verticalOffset -= mouseY * _verticalRange * Time.deltaTime; 

        _horizontalOffset = Mathf.Clamp(_horizontalOffset, -_horizontalRange, _horizontalRange);
        _verticalOffset = Mathf.Clamp(_verticalOffset, -_verticalRange, _verticalRange);

        DrawParabola();
    }

    private void DrawParabola()
    {
        Vector3 startPosition = transform.position;
        
        Vector3 direction = Quaternion.Euler(0, _horizontalOffset * 30f, 0) * transform.forward;
        Vector3 launchVelocity = Quaternion.AngleAxis(-_throwAngle + _verticalOffset, transform.right) * direction * _throwPower;

        for (int i = 0; i < _parabolaResolution; i++)
        {
            float time = (i / (float)_parabolaResolution) * 2f;
            _trajectoryPoints[i] = startPosition + (launchVelocity * time) + 0.5f * Physics.gravity * (time * time);
        }

        _lineRenderer.positionCount = _parabolaResolution;
        _lineRenderer.SetPositions(_trajectoryPoints);
    }

    private void CheckForThrow()
    {
        if (Input.GetKeyDown(_shootKey))
        {
            ThrowObject();
        }
    }

    private void ThrowObject()
    {
        _lauchedEvent?.Invoke();
        _isThrowed = true;
        _rb.isKinematic = false;

        Vector3 direction = Quaternion.Euler(0, _horizontalOffset * 30f, 0) * transform.forward;
        Vector3 launchVelocity = Quaternion.AngleAxis(-_throwAngle + _verticalOffset, transform.right) * direction * _throwForce;

        _rb.velocity = launchVelocity;

        _lineRenderer.enabled = false;
        Destroy(this, 3f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(_winnedCounted) return;

        if(collision.gameObject.GetComponent<Goal>())
        {
            _winEvent?.Invoke();
            _winnedCounted = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(_winnedCounted) return;

        if(other.gameObject.GetComponent<Goal>())
        {
            _winEvent?.Invoke();
            _winnedCounted = true;
        }
    }
}
