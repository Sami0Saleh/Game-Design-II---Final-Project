using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] Animator _playerAnimator;
    [SerializeField] CharacterController _characterController;

    [SerializeField] PlayerController _PContro;

    private bool _hangingMBStarted = true;

    void Update()
    {
        if (Input.GetMouseButton(0))
        { BeginHangEdgeAnim(); }
        if (Input.GetMouseButton(1))
        { EndHangEdgeAnim(); }
 
        if (_PContro.IsWalking == true)
        { BeginWalkAnim(); }
        if (_PContro.IsWalking == false)
        { EndWalkAnim(); }
        if (Input.GetButtonDown("Jump") && _PContro.IsGrounded == true)
        { BeginJumpAnim(); Debug.Log("Should Jump"); }       
        else if (_PContro.IsGrounded == true)
        { EndJumpAnim(); }


        if (_PContro.IsHangingMB == true)
        {
            BeginHangMBIdleAnim();    
        }
        else if (_hangingMBStarted == true && _PContro.LeavingMB == true)
        {
            EndHangMBIdleAnim();
        }
         
        
       
        
    }

    
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
    }
    private void EndHangEdgeAnim()
    {
        _playerAnimator.SetBool("HangingEdge", false);
    }
}
