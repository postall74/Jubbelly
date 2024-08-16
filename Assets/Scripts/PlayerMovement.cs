using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    #region Constants
    #endregion

    #region Components
    private Player _player;
    private CharacterController _characterController;
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
    private void Start()
    {
        _player = GetComponent<Player>();
        _characterController = GetComponent<CharacterController>();
        _moveSpeed = _walkSpeed;

        AssignInputEvents();
    }

    private void Update()
    {
        ApplyMovement();
        AimTowardsMouse();
        AnimatorControllers();
    }
    #endregion

    #region Methods
    private void AssignInputEvents()
    {
        _player.PlayerControllers.Character.Movement.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _player.PlayerControllers.Character.Movement.canceled += ctx => _moveInput = Vector2.zero;

        _player.PlayerControllers.Character.Aim.performed += ctx => _aimInput = ctx.ReadValue<Vector2>();
        _player.PlayerControllers.Character.Aim.canceled += ctx => _aimInput = Vector2.zero;

        _player.PlayerControllers.Character.Run.performed += ctx =>
        {
            _moveSpeed = _runSpeed;
            _isRunning = true;
        };

        _player.PlayerControllers.Character.Run.canceled += ctx =>
        {
            _moveSpeed = _walkSpeed;
            _isRunning = false;
        };
    }

    private void AnimatorControllers()
    {
        float xVelocity = Vector3.Dot(_moveDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(_moveDirection.normalized, transform.forward);

        _player.Animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        _player.Animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);

        bool isRunAnimation = _isRunning && _moveDirection.magnitude > 0;
        _player.Animator.SetBool("isRunning", isRunAnimation);
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
