using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeUIManager : MonoBehaviour
{
    [SerializeField, Header("�eCanvas���X�g(0=�^�񒆁A1=����A2=�E��)")] private List<Canvas> m_canvaes = new List<Canvas>();
    [SerializeField, Header("�e�摜�̈ʒu���X�g")] private List<Vector3> m_TargetPos = new List<Vector3>();
    [SerializeField, Header("�摜�̈ړ��A�g�傪�I������܂ł̎���")] private float m_imageMoveTime = 1f;
    /// <summary>�{�^���������Ă���̌o�ߎ���</summary>
    private float m_elapsedTime = 0f;
    /// <summary>�摜���ړ�����</summary>
    private bool m_isMoving = false;
    //�g����s�����߂̃����o�ϐ�
    private Image m_playerImage = null;
    private Image m_element1Image = null;
    private Image m_element2Image = null;
    //�ړ����s�����߂̃����o�ϐ�
    private RectTransform m_playerRectTf = null;
    private RectTransform m_element1RectTf = null;
    private RectTransform m_element2RectTf = null;

    // Start is called before the first frame update
    void Start()
    {
        m_playerImage = m_canvaes[0].GetComponentInChildren<Image>();
        m_element1Image = m_canvaes[1].GetComponentInChildren<Image>();
        m_element2Image = m_canvaes[2].GetComponentInChildren<Image>();

        m_playerRectTf = m_playerImage.rectTransform;
        m_element1RectTf = m_element1Image.rectTransform;
        m_element2RectTf = m_element2Image.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isMoving)
        {
            //�o�ߎ��Ԃ��摜�ړ����Ԃ𒴂��邩�̔���
            if (m_elapsedTime < m_imageMoveTime)
            {
                //�摜�̈ړ��̐��`��ԏ���
                float alpha = m_elapsedTime / m_imageMoveTime;
                m_playerRectTf.anchoredPosition = Vector3.Lerp(m_TargetPos[1], m_TargetPos[0], alpha);
                m_element1RectTf.anchoredPosition = Vector3.Lerp(m_TargetPos[2], m_TargetPos[1], alpha);
                m_element2RectTf.anchoredPosition = Vector3.Lerp(m_TargetPos[0], m_TargetPos[2], alpha);
                //�摜�̊g��̏���
                m_playerImage.transform.localScale = Vector3.Lerp(new Vector3(1.5f, 1.5f, 1.5f), new Vector3(2, 2, 2), alpha);
                //�o�ߎ��Ԃ��v�Z
                m_elapsedTime += Time.deltaTime;
            }
            //�o�ߎ��Ԃ𒴂��������̈ʒu�Ƒ傫���ɂ��āA�ړ������̔����ʂ�Ȃ�����
            else
            {
                m_playerRectTf.anchoredPosition = m_TargetPos[0];
                m_element1RectTf.anchoredPosition = m_TargetPos[1];
                m_element2RectTf.anchoredPosition = m_TargetPos[2];

                m_playerImage.transform.localScale = new Vector3(2, 2, 2);

                m_isMoving = false;
            }
        }
    }
    public void LifeUISetUp()
    {
        //�{�^�������������̏���������+�摜�̈ړ��������X�^�[�g����
        UISetUp();
        m_isMoving = true;
        m_elapsedTime = 0f;
    }

    private void UISetUp()
    {
        //�eUI�摜�̈ʒu������̈ʒu�ɏ�����
        m_playerRectTf.anchoredPosition = m_TargetPos[0];
        m_element1RectTf.anchoredPosition = m_TargetPos[1];
        m_element2RectTf.anchoredPosition = m_TargetPos[2];
        //���X�g�̐擪�̗v�f���Ō���ɓ����
        Canvas head = m_canvaes[0];
        m_canvaes.Remove(head);
        m_canvaes.Add(head);
        //�����o�ϐ��ɑ��
        m_playerImage = m_canvaes[0].GetComponentInChildren<Image>();
        m_element1Image = m_canvaes[1].GetComponentInChildren<Image>();
        m_element2Image = m_canvaes[2].GetComponentInChildren<Image>();
        m_playerRectTf = m_playerImage.rectTransform;
        m_element1RectTf = m_element1Image.rectTransform;
        m_element2RectTf = m_element2Image.rectTransform;
        //playerImage�ȊO�̉摜������������
        m_element1Image.transform.localScale = m_element2Image.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //�摜�̕`�揇���v���C���[�̉摜���őO�ʂɂȂ�悤�ύX
        m_canvaes[0].sortingOrder = 3;
        m_canvaes[1].sortingOrder = 2;
        m_canvaes[2].sortingOrder = 1;
    }
}
