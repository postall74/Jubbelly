using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region Constants
    #endregion

    #region Components
    private PlayerControllers _playerControllers;
    private CharacterController _characterController;
    private Animator _animator;
    #endregion

    #region Fields
    [Header("Aim info")]
    [SerializeField] private LayerMask _aimLayer;
    [SerializeField] private Transform _aim;
    private Vector3 _lookDirection;
    [Header("Movement info")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    private float _moveSpeed = 1f;
    private Vector3 _moveDirection;
    private Vector3 _velocity;
    private Vector2 _moveInput;
    private Vector2 _aimInput;
    private bool _isRunning = false;
    #endregion

    #region Properties
    #endregion

    #region Unity Methods
    private void Awake()
    {
        AssignInputEvents();
    }

    private void OnEnable()
    {
        _playerControllers.Enable();
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _moveSpeed = _walkSpeed;
    }

    private void Update()
    {
        ApplyMovement();
        AimTowardsMouse();
        AnimatorControllers();
    }

    private void OnDisable()
    {
        _playerControllers.Disable();
    }
    #endregion

    #region Methods
    private void AssignInputEvents()
    {
        _playerControllers = new PlayerControllers();

        _playerControllers.Character.Movement.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerControllers.Character.Movement.canceled += ctx => _moveInput = Vector2.zero;

        _playerControllers.Character.Aim.performed += ctx => _aimInput = ctx.ReadValue<Vector2>();
        _playerControllers.Character.Aim.canceled += ctx => _aimInput = Vector2.zero;

        _playerControllers.Character.Fire.performed += ctx => Shoot();

        _playerControllers.Character.Run.performed += ctx =>
        {
            _moveSpeed = _runSpeed;
            _isRunning = true;
        };

        _playerControllers.Character.Run.canceled += ctx =>
        {
            _moveSpeed = _walkSpeed;
            _isRunning = false;
        };
    }

    private void AnimatorControllers()
    {
        float xVelocity = Vector3.Dot(_moveDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(_moveDirection.normalized, transform.forward);

        _animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        _animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);

        bool isRunAnimation = _isRunning && _moveDirection.magnitude > 0;
        _animator.SetBool("isRunning", isRunAnimation);
    }

    private void Shoot()
    {
        _animator.SetTrigger("Fire");
    }

    private void AimTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(_aimInput);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _aimLayer))
        {
            _lookDirection = hit.point - transform.position;
            _lookDirection.y = 0;
            _lookDirection.Normalize();

            transform.forward = _lookDirection;

            _aim.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
    }

    private void ApplyMovement()
    {
        _moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);
        ApplyGravity();

        if (_moveDirection.magnitude > 0)
            _characterController.Move(_moveSpeed * Time.deltaTime * _moveDirection);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded)
            _velocity.y = 0;
        else
            _velocity.y += Physics.gravity.y * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }
    #endregion

}
