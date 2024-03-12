using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] Animator _playerAnimator;
    [SerializeField] CharacterController _characterController;
    [SerializeField] PlayerController _PContro;


    private bool _hangingMBStarted = true;
    private bool _hangingEdgeStarted = false;

    void Update()
    {
        if (Input.GetMouseButton(0))
        { BeginHangEdgeAnim(); }
        if (Input.GetMouseButton(1))
        { EndHangEdgeAnim(); }
        if (_hangingEdgeStarted && Input.GetKey(KeyCode.D))
        { BeginHangEdgeMovement(); SetMirrorBool(false); Debug.Log("Should Move Edge right"); }
        else if (_hangingEdgeStarted && Input.GetKey(KeyCode.A))
        { BeginHangEdgeMovement(); SetMirrorBool(true); Debug.Log("Should Move Edge right"); }
        else if (_hangingEdgeStarted) { EndHangEdgeMovement(); }
 
        if (_PContro.IsWalking)
        { BeginWalkAnim(); }
        if (_PContro.IsWalking)
        { EndWalkAnim(); }

        if (Input.GetButtonDown("Jump") && _PContro.IsGrounded)
        { BeginJumpAnim(); Debug.Log("Should Jump"); }       
        else if (_PContro.IsGrounded)
        { EndJumpAnim(); }

        if (_PContro.IsHangingMB)
        { BeginHangMBIdleAnim(); }
        else if (_hangingMBStarted && _PContro.LeavingMB)
        { EndHangMBIdleAnim(); }
    } // should convert into lambda

    
    private void BeginWalkAnim()
    {
        _playerAnimator.SetBool("Walking", true);
    }
    private void EndWalkAnim() 
    {
        _playerAnimator.SetBool("Walking", false);
    }

    private void BeginJumpAnim()
    {
        _playerAnimator.SetBool("Jumping", true);
    }
    private void EndJumpAnim()
    {
        _playerAnimator.SetBool("Jumping", false);
    }
    
    private void BeginHangMBIdleAnim()
    {
        _hangingMBStarted = true;
        _playerAnimator.SetBool("HangingMB", true);
    }
    private void EndHangMBIdleAnim()
    {
        _hangingMBStarted = false;
        _playerAnimator.SetBool("HangingMB", false);
    }

    private void BeginHangEdgeAnim()
    {
        _playerAnimator.SetBool("HangingEdge", true);
        _hangingEdgeStarted = true;
    }
    private void EndHangEdgeAnim()
    {
        _hangingEdgeStarted = true;
        _playerAnimator.SetBool("HangingEdge", false);
    }
    private void BeginHangEdgeMovement()
    {
        _hangingEdgeStarted = false;
        _playerAnimator.SetBool("EdgeMovement", true);
    }
    private void EndHangEdgeMovement()
    {
        _playerAnimator.SetBool("EdgeMovement", false);
    }


    private void SetMirrorBool(bool boolean)
    {
        _playerAnimator.SetBool("Mirror", boolean);  
    }
}
