using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    [SerializeField] private bool m_isFilp = false;
    [SerializeField] private float m_speed = 0f;
    [SerializeField] private BulletType m_bulletType = BulletType.None;
    private Rigidbody2D rb = null;
    private PlayerController m_playerController = null;
    private Transform m_playerTf = null;
    private bool m_isInCamera = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_playerTf = m_playerController.GetPlayerTf;
        if (m_isFilp)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rb.velocity = transform.right * m_speed;


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void Damage(BulletType type)
    {
        if (type == m_bulletType)
        {

        }
        else
        {

        }
    }
    private void OnBecameInvisible()
    {
        m_isInCamera = false;
    }
    private void OnBecameVisible()
    {
        m_isInCamera = true;
    }
}
