using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private float m_scrollSpeed = -0.5f;
    [SerializeField] private float m_scrollStartPosition = 0;
    private float m_scrollEndPosition = 0;

    void Start()
    {
        var sizeX = GetComponent<BoxCollider2D>().size.x;
        var scaleX = transform.localScale.x;
        m_scrollEndPosition = m_scrollStartPosition - (sizeX * scaleX * 2);
    }

    void Update()
    {
        var x = transform.position.x;
        if (x < m_scrollEndPosition)
        {
            var diff = m_scrollEndPosition - x;
            x = m_scrollStartPosition - diff - 0.05f;
        }
        else
        {
            x += m_scrollSpeed * Time.deltaTime;
        }
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
