using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MKPopupText : MonoBehaviour
{
    [SerializeField] private Vector3 m_velocity;
    [SerializeField] private float m_durationMax;
    [SerializeField] private float m_duration;

    public void SetText(string text)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_duration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_duration += Time.deltaTime;
        if (m_duration > m_durationMax)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += m_velocity * Time.deltaTime;
        }
    }
}
