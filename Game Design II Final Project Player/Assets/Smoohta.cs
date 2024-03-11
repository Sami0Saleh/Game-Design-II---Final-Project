using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoohta : MonoBehaviour
{
    [SerializeField] Rigidbody _rbSmoohta;
    private float _spitForwardForce = 20f;
    private float _spitUpForce = 8f;

    private void Start()
    {
        _rbSmoohta.AddForce(transform.forward * _spitForwardForce, ForceMode.Impulse);
        _rbSmoohta.AddForce(transform.up * _spitUpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground") // sami says you can remove the if and destroy in each collision
        {
            Destroy(this.gameObject);
        }    
    }
}
