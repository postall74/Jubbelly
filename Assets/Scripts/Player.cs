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
    [Header("Tower info")]
    [SerializeField] private Transform _tankTowerTransform;
    [SerializeField] private float _speedRotation;
    [Header("Aim info")]
    [SerializeField] private LayerMask _aimMask;
    [SerializeField] private Transform _aimTransform;
    [Space]
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
        UpdateAim();
        CheckInput();
    }


    private void FixedUpdate()
    {
        Movement();
        BodyRotation();
        TowerRotation();
    }

    private void CheckInput()
    {
        _verticalInput = Input.GetAxis(Vertical);
        _horizontalInput = Input.GetAxis(Horizontal);

        if (_verticalInput < 0)
            _horizontalInput = -Input.GetAxis(Horizontal);
    }

    private void TowerRotation()
    {
        Vector3 direction = _aimTransform.position - _tankTowerTransform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _tankTowerTransform.rotation = Quaternion.RotateTowards(_tankTowerTransform.rotation, targetRotation, _speedRotation);
    }

    private void BodyRotation()
    {
        transform.Rotate(0, _horizontalInput * _rotationSpeed, 0);
    }

    private void Movement()
    {
        Vector3 movement = _moveSpeed * _verticalInput * transform.forward;
        _rb.velocity = movement;
    }

    private void UpdateAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, _aimMask))
        {
            float fixedY = _aimTransform.position.y;
            _aimTransform.position = new Vector3(hit.point.x, fixedY, hit.point.z);
        }
    }
}
