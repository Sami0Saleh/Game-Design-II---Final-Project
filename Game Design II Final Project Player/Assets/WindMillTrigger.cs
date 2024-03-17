using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMillTrigger : MonoBehaviour
{
    [SerializeField] GameObject windMillTurbine;
    [SerializeField] GameObject Island;
    [SerializeField] GameObject Switch;

    public Vector3 IslandMovement = new Vector3(0,1,0);
    public float WindMilSpeed = 1;
    public bool _windMillTurning = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_windMillTurning)
        {
            Island.transform.Translate(IslandMovement);
            windMillTurbine.transform.Rotate(0,0,WindMilSpeed);
        }
    }
}
