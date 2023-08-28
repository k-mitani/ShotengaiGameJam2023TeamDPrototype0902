using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ob_rotations : MonoBehaviour
{
    
  [SerializeField]private float Speed;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, Speed);
    }
}
