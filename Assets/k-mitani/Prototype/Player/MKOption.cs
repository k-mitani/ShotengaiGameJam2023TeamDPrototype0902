using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKOption : MKPlayerFormationUnit
{
    [SerializeField] Transform m_followObject;
    [SerializeField] float m_offsetMin;
    [SerializeField] float m_speed;

    void Update()
    {
        if (m_followObject != null)
        {
            var xDiff = transform.position.x - m_followObject.position.x;
            var yDiff = transform.position.y - m_followObject.position.y;
            // コブタは横長なので、Y方向はより接近させる。
            var distance = Mathf.Sqrt(xDiff * xDiff + (yDiff * yDiff) * 2f);
            if (distance > m_offsetMin)
            {
                var direction = (m_followObject.position - transform.position).normalized;
                transform.position += m_speed * Time.deltaTime * direction;
            }
        }
    }
}
