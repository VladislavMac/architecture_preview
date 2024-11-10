using Core.Behaviours.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player;

    [SerializeField] private Transform _playerBody;

    [SerializeField] private Camera _playerCamera;

    private float _cameraSensivity = 100f;
    private float _yRotation = 0f;
    private float _xLookLimit;
    private float _yLookLimit = 90f;
    private float _cameraCrouchPositionY = 0.8f;
    private float _cameraStandPositionY = 1.5f;


    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        this.PlayerCamera();
        if (_player.GetIsPlayerCrouching())
        {
            this.CrouchingCamera();
        }
        else
        {
            this.StandingCamera();
        }
    }
    private void PlayerCamera()
    {
        float _mouseX = Input.GetAxis("Mouse X") * _cameraSensivity * Time.deltaTime;
        float _mouseY = Input.GetAxis("Mouse Y") * _cameraSensivity * Time.deltaTime;

        _yRotation -= _mouseY;
        _yRotation = Mathf.Clamp(_yRotation, -_yLookLimit, _yLookLimit);

        _playerCamera.transform.localRotation = Quaternion.Euler(_yRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * _mouseX);
    }
    private void CrouchingCamera()
    {
        _playerCamera.transform.localPosition = new Vector3(_playerCamera.transform.localPosition.x, _cameraCrouchPositionY, _playerCamera.transform.localPosition.z);
    }

    private void StandingCamera()
    {
        _playerCamera.transform.localPosition = new Vector3(_playerCamera.transform.localPosition.x, _cameraStandPositionY, _playerCamera.transform.localPosition.z);
    }
}
