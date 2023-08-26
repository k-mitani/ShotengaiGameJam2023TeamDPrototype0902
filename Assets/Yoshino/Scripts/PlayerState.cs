using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerColor
{
    Red,
    Green,
    Blue
}
public class PlayerState : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_playerSR = null;
    [SerializeField] private Image m_uiImage = null;
    [SerializeField] private Color m_damagedColor = Color.gray;
    [SerializeField] private bool m_isDamage = false;

    public bool GetIsDamage
    {
        get { return m_isDamage; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ダメージが入ったらコブタのキャラクターとUIの色を変える
        m_playerSR.color = m_uiImage.color = m_isDamage ? m_damagedColor : Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void Damage()
    {
        m_isDamage = true;
    }
}
