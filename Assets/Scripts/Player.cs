using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    #region Constants
    public const string Vertical = "Vertical";
    public const string Horizontal = "Horizontal";
    #endregion

    #region Components
    private Rigidbody _rb;
    #endregion

    #region Fields
    [Header("Movement info")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    private float _verticalInput;
    private float _horizontalInput;
    #endregion

    #region Properties
    #endregion


    private void Start() =>
        _rb = GetComponent<Rigidbody>();

    private void Update()
    {
        _verticalInput = Input.GetAxis(Vertical);
        _horizontalInput = Input.GetAxis(Horizontal);

        if (_verticalInput < 0 )
            _horizontalInput = -Input.GetAxis(Horizontal);
    }

    private void FixedUpdate()
    {
        Vector3 movement = _moveSpeed * _verticalInput * transform.forward;
        _rb.velocity = movement;
        transform.Rotate(0, _horizontalInput * _rotationSpeed, 0);
    }
}
