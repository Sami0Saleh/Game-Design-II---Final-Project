using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] GameObject Object;
    [SerializeField] Transform BluePoint;
    [SerializeField] Transform RedPoint;

    [SerializeField] MeshRenderer BluePointMR;
    [SerializeField] MeshRenderer RedPointMR;

    private Vector3 _close;
    private Vector3 _distance;
    private Vector3 _move;
    public bool _moveToRed = false;
    public float _speed;

    private void Start()
    {
        BluePointMR.enabled = false;
        RedPointMR.enabled = false;
    }
    void Update()
    {
        if (_moveToRed)
        {
            CreateVector(Object.transform.position, RedPoint.position);
            Object.transform.Translate(_move * _speed * Time.deltaTime);
            _close = Object.transform.position - RedPoint.position;
            if (ReachedTarget())
            { _moveToRed = false; }
        }
        else 
        {
            CreateVector(Object.transform.position, BluePoint.position);
            Object.transform.Translate(_move * _speed * Time.deltaTime);
            if (ReachedTarget())
            { _moveToRed = true; }
        }

    }

    public void CreateVector(Vector3 objectPosition, Vector3 TargetPosition)
    {
        _move = TargetPosition - objectPosition;
        Debug.Log(_move);
    }
    public bool ReachedTarget()
    {
        if (_move.x < 0.2f && _move.y < 0.2f && _move.z < 0.2f && !_moveToRed)
        {
            return true;
        }
        else if (_move.x > -0.2f && _move.y > -0.2f && _move.z > -0.2f && _moveToRed)
        {
            return true;
        }
        else return false;
    }
   
}
