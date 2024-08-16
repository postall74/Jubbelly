using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    #region Constants
    #endregion

    #region Components
    private Player _player;
    #endregion

    #region Fields
    #endregion

    #region Properties
    #endregion

    #region Unity Methods
    private void Start()
    {
        _player = GetComponent<Player>();

        _player.PlayerControllers.Character.Fire.performed += ctx => Shoot();
    }
    #endregion

    #region Methods
    private void Shoot()
    {
        _player.Animator.SetTrigger("Fire");
    }
    #endregion

}
