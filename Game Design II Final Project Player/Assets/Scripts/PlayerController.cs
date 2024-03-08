using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] CharacterController _characterController;

    [SerializeField] LayerMask _groundLayer;
    [SerializeField] LayerMask _edgeLayer;
    [SerializeField] LayerMask _monkeyBarLayer;

    private Vector3 _moveDirection = Vector3.zero;
    private Vector3 _oldMoveDirection;
    private float _gravity = 20.0f;

    private float _horizontalInput;
    private float _verticalInput;
    private float _moveSpeed = 10f;
    private float _jumpHeight = 10f;

    private float _edgeMovementSpeed = 5f;
    private float _edgeDetectionDistance = 20f;
    private float _edgeHangOffset = 0.5f;

    private bool _isWalking;
    private bool _isJumping = false;
    public bool _isDoubleJumping = false;
    private bool _isDashing = false;
    public bool _isGrounded = true;
    private bool _isSpinAttack = false;

    private bool _isHanging = false;
    private bool _isOnMonkeyBar = false;

    public bool IsWalking
    {
        get { return _isWalking; }
        set { _isWalking = value; }
    }
    public bool IsJumping
    {
        get { return _isJumping; }
        set { _isJumping = value; } 
    }
    public bool IsGrounded
    {
        get { return _isGrounded; }
        set { _isGrounded = value; }
    }

    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        _isGrounded = Physics.CheckSphere(transform.position, 0.01f, _groundLayer);

        if (!_isGrounded && !Input.GetButtonDown("Jump"))
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
            Debug.Log("Falling");
        }
        else if (_isHanging)
        {
            _isWalking = false;
            HangingEdge();
        }
        else if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _isWalking = false;
            Jump();
        }
        else if (!_isGrounded && _isJumping && !_isDoubleJumping && Input.GetButtonDown("Jump"))
        {
            _isWalking = false;
            DoubleJump();
        }
        else
        {
            PlayerMovement();
            
        }
        
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        
        _moveDirection = new Vector3(_horizontalInput, 0.0f, _verticalInput);
        if (_moveDirection == Vector3.zero) { _isWalking = false; }
        else { _isWalking = true; }
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= _moveSpeed;
        _oldMoveDirection = _moveDirection;
        _isJumping = false;
        _isDoubleJumping = false;
    }

    private void Jump()
    {
        _moveDirection.y = _jumpHeight;
        _isJumping = true;
        _isGrounded = false;
    }
    private void DoubleJump()
    {
        _moveDirection.y = _jumpHeight;
        _isDoubleJumping = true;
    }

    private void HangingEdge()
    {
        Vector3 currentPos = transform.position;
        Debug.Log("Detected a edge");
        RaycastHit hit;
        // Perform raycast to detect edges below the character
        if (Physics.Raycast(transform.position, -transform.up, out hit, _edgeDetectionDistance, _edgeLayer))
        {

            Physics.Raycast(transform.position, -transform.up, out hit, _edgeDetectionDistance, _edgeLayer);
            Debug.Log("Entered RayCast");
            // Position character at the edge with slight offset
            Vector3 hangPosition = hit.point + transform.up * _edgeHangOffset;
            _characterController.Move(/*hangPosition*/ currentPos - transform.position);

            // Disable movement along y-axis
            _moveDirection.y = 0;

            // Check for lateral movement input
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 lateralMovement = new Vector3(horizontalInput, 0, verticalInput).normalized * _edgeMovementSpeed;
            _characterController.Move(lateralMovement * Time.deltaTime);
        }
        else
        {
            // Stop hanging if no edge is detected
            _isHanging = false;
        }

        // Check for jump input to vault from edge
        if (Input.GetButtonDown("Jump"))
        {

            Debug.Log("let Go from Edge");
            JumpFromEdge();
        }
    }

    private void JumpFromEdge()
    {
        // Apply jump force away from the edge
        _moveDirection = transform.forward * _jumpHeight;
        _moveDirection.y = _jumpHeight;
        _moveDirection.z = transform.forward.z;
        _isHanging = false;
    }

    private void OnMonekyBar()
    {

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (hit.gameObject.CompareTag("edge"))
        {
            _isHanging = true;
        }
        else if (hit.gameObject.CompareTag("monkeyBar"))
        {
            _isOnMonkeyBar = true;
        }
    }

}
