using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKKobunMoveStraight : MonoBehaviour
{
    [SerializeField] private float m_speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += m_speed * Time.deltaTime * Vector3.left;
    }
}
