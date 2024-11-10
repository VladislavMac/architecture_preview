
using UnityEngine;

namespace Core.Behaviours.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private float _normalMoveSpeed = 20f;
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _crouchingMoveSpeed = 5f;
        [SerializeField] private float _sprintMoveSpeed = 20f;
        [SerializeField] private float _jumpHight = 100f;

        [SerializeField] private LayerMask _terrain;

        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _cam;

        [SerializeField] private GameObject _body;

        [SerializeField] private Rigidbody _capsule;

        [SerializeField] private CharacterController _characterController;

        Vector3 velocity = new Vector3 (0,0,0);
        Vector3 moveDir = new Vector3(0,0,0);

        
        

        private bool _canmove;
        private bool _isGrounded;
        private bool _pressedMove = false;
        private bool _isCrouching = false;
        private bool _isCrouchingPressed => Input.GetKeyDown(KeyCode.C);


        private float _playerRadius = 0.6f;
        private float _rotateSpeed = 10f;
        private float _playerHeight = 2f;
        private float _playerCrouchHeight = 1f;
        private float _gravity = -9.8f;
        private float _jumpVelocity = 10f;

        Vector3 currentMoveDir = Vector3.zero;
        
      
        private void Update()
        {
            this.IsPlayerGrounded();
            this.HandleGravity();
            this.HandleMovement();
            this.PlayerCrouch();
            this.HandleJump();
        }
        public void HandleMovement()
        {
           
            bool isSprinting = Input.GetKey(KeyCode.LeftShift);
            
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.z = Input.GetAxisRaw("Vertical");

            if (_isCrouchingPressed) 
            {
                _isCrouching = !_isCrouching;
            }

            if (isSprinting)
            {
                _moveSpeed = _sprintMoveSpeed;
            }
            else if (_isCrouching)
            {
                _moveSpeed = _crouchingMoveSpeed;
            }
            else
            {
                 _moveSpeed = _normalMoveSpeed;
            }

            if (moveDir.x != 0 || moveDir.z != 0)
            { 
                _pressedMove = true;
            }
            else
            {
                _pressedMove = false;
            }
            
            moveDir = moveDir.normalized;
            float moveDistance = _moveSpeed * Time.deltaTime;
           
            if (_pressedMove)
            { 
                moveDir = transform.right * moveDir.x + transform.forward * moveDir.z;
                _characterController.Move(moveDir.normalized * moveDistance);
                
            }
            else if (!IsPlayerGrounded())
            {
                _characterController.Move((moveDir.normalized/(3)).normalized * moveDistance);
            }
        }
        
        private void HandleJump() 
        {
            if (Input.GetKeyDown(KeyCode.Space) && IsPlayerGrounded())
            {
                velocity.y = Mathf.Sqrt(_jumpHight * -2 * _gravity * Time.deltaTime);
                _characterController.Move(velocity * Time.deltaTime);
                currentMoveDir = moveDir;
            }
        }
        private void HandleGravity()
        {
            if (!IsPlayerGrounded())
            {
                velocity.y += _gravity * Time.deltaTime;
                _characterController.Move(velocity * Time.deltaTime);
            }else
            {
                velocity.y = -10f;
            }
        }
        private bool IsPlayerGrounded()
        {
            Vector3 position = _playerTransform.position;
            
            if (Physics.Raycast(_playerTransform.position, Vector3.down,  0.1f ) || _characterController.isGrounded)
            {
                return _isGrounded = true;
            }
            else
            {
                return _isGrounded = false;
            }
        }
        private void PlayerCrouch()
        {

            if (_isCrouching && IsPlayerGrounded())
            {
                _characterController.height = _playerCrouchHeight;
                _body.transform.localScale = new Vector3(1,_playerCrouchHeight / _playerHeight,1);

                _characterController.center = new Vector3(0, (_playerCrouchHeight / 2), 0);
                _body.transform.localPosition = new Vector3(0, (_playerCrouchHeight / 2), 0);

            }
            else
            {
                _characterController.height = _playerHeight;
                _body.transform.localScale = new Vector3(1, 1, 1);

                _characterController.center = new Vector3(0, 1, 0);
                _body.transform.localPosition = new Vector3(0, 1, 0);
            }
        }
        public bool GetIsPlayerCrouching()
        {
            return _isCrouching;
        }
        
    }
}
