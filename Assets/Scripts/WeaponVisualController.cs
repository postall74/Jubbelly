using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisualController : MonoBehaviour
{
    #region Constants
    #endregion

    #region Fields
    [SerializeField] private Transform[] _gunTransforms;
    [SerializeField] private Transform _pistol, _revolver, _rifle, _shotgun_1, _shotgun_2, _smg, _sniperRifle;
    #endregion

    #region Properties
    #endregion

    #region Unity Methods
    private void Start()
    {
        SwitchOffGuns();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            SwitchOn(_pistol);

        if (Input.GetKey(KeyCode.Alpha2))
            SwitchOn(_revolver);
        
        if (Input.GetKey(KeyCode.Alpha3))
            SwitchOn(_rifle);
        
        if (Input.GetKey(KeyCode.Alpha4))
            SwitchOn(_shotgun_1);
        
        if (Input.GetKey(KeyCode.Alpha5))
            SwitchOn(_shotgun_2);
        
        if (Input.GetKey(KeyCode.Alpha6))
            SwitchOn(_smg);
        
        if (Input.GetKey(KeyCode.Alpha7))
            SwitchOn(_sniperRifle);
    }

    #endregion

    #region Methods
    private void SwitchOffGuns()
    {
        foreach (Transform gun in _gunTransforms)
        {
            gun.gameObject.SetActive(false);
        }
    }

    private void SwitchOn(Transform gunTransform)
    {
        SwitchOffGuns();
        gunTransform.gameObject.SetActive(true);
    }
    #endregion
}



