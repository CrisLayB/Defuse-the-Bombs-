using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatMichil : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _maxAngle = 45f;

    [Header("Shooting")]
    [SerializeField] private float _launchForce = 10f;
    [SerializeField] private KeyCode _shootKey = KeyCode.Space;

    [Header("Unity Events")]
    [SerializeField] private UnityEvent _lauchedEvent;
    [SerializeField] private UnityEvent _winEvent;

    private float _rotationTime = 0f;
    private bool _isLaunched = false;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    private void Update()
    {
        if (!_isLaunched)
        {
            RotateBarrel();
            CheckForLaunch();
        }
    }

    private void RotateBarrel()
    {
        _rotationTime += Time.deltaTime * _rotationSpeed;
        float angle = Mathf.Sin(_rotationTime) * _maxAngle;

        transform.rotation = Quaternion.Euler(angle, 0, 0);
    }

    private void CheckForLaunch()
    {
        if (Input.GetKeyDown(_shootKey))
        {
            LaunchObject();
        }
    }

    private void LaunchObject()
    {
        _lauchedEvent?.Invoke();
        _isLaunched = true;
        _rb.isKinematic = false;
        
        _rb.AddForce(transform.forward * _launchForce, ForceMode.Impulse);

        Destroy(this, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<BombGameEvent>())
        {
            _winEvent?.Invoke();
        }
    }
}
