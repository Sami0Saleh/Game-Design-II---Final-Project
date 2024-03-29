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
    private bool _edgeMovement = false;

    void Update()
    {
        if (Input.GetMouseButton(0))
        { TriggerAttack("Spin"); }
        if (Input.GetMouseButton(1))
        { TriggerAttack("Dash"); }

        // move right
        if (!_edgeMovement && Input.GetKeyDown(KeyCode.D))
        { TriggerHangEdgeMovement(true); }
        else if (_edgeMovement && Input.GetKeyDown(KeyCode.D))
        { TriggerHangEdgeMovement(true); SetMirrorBool(false);  }
        else if (Input.GetKeyUp(KeyCode.D)) { TriggerHangEdgeMovement(false); }

        // move left
        if (!_edgeMovement && Input.GetKeyDown(KeyCode.A))
        { TriggerHangEdgeMovement(true); }
        else if (_edgeMovement && Input.GetKeyDown(KeyCode.A))
        { TriggerHangEdgeMovement(true); SetMirrorBool(true); }
        else if (Input.GetKeyUp(KeyCode.A)) { TriggerHangEdgeMovement(false); }

        // leave edge
        if (_hangingEdgeStarted && Input.GetKey(KeyCode.Space))
        {
            LeaveEdge(true);
        }
        else if (_hangingEdgeStarted && Input.GetKey(KeyCode.S))
        {
            LeaveEdge(false);
        }

        if (_PContro.IsWalking)
        { TriggerWalkAnim(true); }
        if (_PContro.IsWalking)
        { TriggerWalkAnim(false); }

        if (Input.GetButtonDown("Jump") && _PContro.IsGrounded)
        { TriggerJumpAnim(true); Debug.Log("Should Jump"); }       
        else if (_PContro.IsGrounded)
        { TriggerJumpAnim(false); }

        if (_PContro.IsHangingMB)
        { TriggerHangMBIdleAnim(true); }
        else if (_hangingMBStarted && _PContro.LeavingMB)
        { TriggerHangMBIdleAnim(false); }
    } // should convert into lambda

    
    private void TriggerWalkAnim(bool trigger)
    {
        if (trigger)
        { _playerAnimator.SetBool("Walking", true); }
        else
            _playerAnimator.SetBool("Walking", false);
    }
    private void TriggerJumpAnim(bool trigger)
    {
        if (trigger)
        _playerAnimator.SetBool("Jumping", true);
        else
        { _playerAnimator.SetBool("Jumping", false); }
    }

    private void TriggerHangMBIdleAnim(bool trigger)
    {
        if (trigger)
        {
            _hangingMBStarted = true;
            _playerAnimator.SetBool("HangingMB", true);
        }
        else
        {
            _hangingMBStarted = false;
            _playerAnimator.SetBool("HangingMB", false);
        }
       
    }

    // Edge Animations
    private void TriggerHangEdgeAnim(bool trigger)
    {
        if (trigger)
        {
            _playerAnimator.SetBool("HangingEdge", true);
            _hangingEdgeStarted = true;
        }
        else
        {
            _hangingEdgeStarted = true;
            _playerAnimator.SetBool("HangingEdge", false);
        }
    }
    private void TriggerHangEdgeMovement(bool trigger)
    {
        if (trigger)
        {
            _hangingEdgeStarted = false;
            _edgeMovement = true;
            _playerAnimator.SetBool("EdgeMovement", true);
        }
        else
        {
            _hangingEdgeStarted = true;
            _playerAnimator.SetBool("EdgeMovement", false);
        }
    }

    private void LeaveEdge(bool up)
    {
        TriggerHangEdgeAnim(false);
        _hangingEdgeStarted = false;
        if (up)
        { _playerAnimator.SetTrigger("ClimbEdge"); }
        else if (!up)
        { _playerAnimator.SetTrigger("DropEdge"); }
    }
    
    private void TriggerAttack(string attackType)
    {
        _playerAnimator.SetBool("IsAttacking", true);
        _playerAnimator.SetTrigger($"{attackType}Attack");
        Invoke($"Reset{attackType}Trigger", 2);
    }
    private void ResetSpinTrigger()
    {
        _playerAnimator.ResetTrigger("SpinAttack");
        _playerAnimator.SetBool("IsAttacking", false);
    }
    private void ResetDashTrigger()
    {
        _playerAnimator.ResetTrigger("DashAttack");
        _playerAnimator.SetBool("IsAttacking", false);
    }

    private void SetMirrorBool(bool mirror)
    {
        _playerAnimator.SetBool("Mirror", mirror);  
    }
}
