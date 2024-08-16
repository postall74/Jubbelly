using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Constants
    #endregion

    #region Components
    private PlayerControllers _playerControllers;
    private Animator _animator;
    #endregion

    #region Fields
    #endregion

    #region Properties
    public PlayerControllers PlayerControllers => _playerControllers;
    public Animator Animator => _animator;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _playerControllers = new PlayerControllers();
    }

    private void OnEnable()
    {
        _playerControllers.Enable();
    }

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnDisable()
    {
        _playerControllers.Disable();
    }
    #endregion

    #region Methods
    #endregion
}
