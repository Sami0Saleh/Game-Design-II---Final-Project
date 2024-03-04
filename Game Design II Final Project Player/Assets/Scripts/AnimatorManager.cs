using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    [SerializeField] Animator _playerAnimator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        { BeginWalkAnim(); }
        if (Input.GetMouseButton(1))
        { EndWalkAnim(); }
    }

    private void BeginWalkAnim()
    {
        _playerAnimator.SetBool("Walking", true);
    }
    private void EndWalkAnim() 
    {
        _playerAnimator.SetBool("Walking", false);
    }
}
