using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoohta : MonoBehaviour
{
    [SerializeField] Rigidbody RB_Spit;
    public float Spit_ForwardForce = 20f;
    public float Spit_UpForce = 8f;

    private void Start()
    {
        RB_Spit.AddForce(transform.forward * Spit_ForwardForce, ForceMode.Impulse);
        RB_Spit.AddForce(transform.up * Spit_UpForce, ForceMode.Impulse);
    }
    void Update()
    {
           
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            Destroy(this.gameObject);
        }    
    }
}
