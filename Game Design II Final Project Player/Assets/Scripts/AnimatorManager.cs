using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] Animator _playerAnimator;
    [SerializeField] CharacterController _characterController;

    [SerializeField] PlayerController _playerController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        { BeginWalkAnim(); }
        if (Input.GetMouseButton(1))
        { EndWalkAnim(); }

        if (Input.GetButtonDown("Jump") && _playerController._isGrounded == true)
        { BeginJumpAnim(); Debug.Log("Should Jump"); }       
        else if (_playerController._isGrounded == true)
        { EndJumpAnim(); }
       
        
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
}
