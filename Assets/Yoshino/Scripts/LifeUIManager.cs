using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeUIManager : MonoBehaviour
{
    public static LifeUIManager Instance { get; private set; }
    [SerializeField, Header("�eCanvas���X�g")] private List<Canvas> m_canvaes = new List<Canvas>();
    [SerializeField, Header("�e�摜���X�g")] private List<Image> m_images = new List<Image>();
    [SerializeField, Header("�e�摜�̈ʒu���X�g")] private List<Vector3> m_TargetPos = new List<Vector3>();
    [SerializeField, Header("�摜�̈ړ��A�g�傪�I������܂ł̎���")] private float m_imageMoveTime = 1f;
    /// <summary>�{�^���������Ă���̌o�ߎ���</summary>
    private float m_elapsedTime = 0f;
    /// <summary>�摜���ړ�����</summary>
    private bool m_isMoving = false;
    //�g����s�����߂̃����o�ϐ�
    private Image m_element0Image = null;
    private Image m_element1Image = null;
    private Image m_element2Image = null;
    //�ړ����s�����߂̃����o�ϐ�
    private RectTransform m_element0RectTf = null;
    private RectTransform m_element1RectTf = null;
    private RectTransform m_element2RectTf = null;

    private PlayerController m_players = null;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_players = FindObjectOfType<PlayerController>();

        m_element0Image = m_images[0] = m_canvaes[0].GetComponentInChildren<Image>();
        m_element1Image = m_images[1] = m_canvaes[1].GetComponentInChildren<Image>();
        m_element2Image = m_images[2] = m_canvaes[2].GetComponentInChildren<Image>();

        m_element0RectTf = m_element0Image.rectTransform;
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
                m_element0RectTf.anchoredPosition = Vector3.Lerp(m_TargetPos[1], m_TargetPos[0], alpha);
                m_element1RectTf.anchoredPosition = Vector3.Lerp(m_TargetPos[2], m_TargetPos[1], alpha);
                m_element2RectTf.anchoredPosition = Vector3.Lerp(m_TargetPos[0], m_TargetPos[2], alpha);
                //�摜�̊g��̏���
                m_element0Image.transform.localScale = Vector3.Lerp(new Vector3(1.5f, 1.5f, 1.5f), new Vector3(2, 2, 2), alpha);
                //�o�ߎ��Ԃ��v�Z
                m_elapsedTime += Time.deltaTime;
            }
            //�o�ߎ��Ԃ𒴂��������̈ʒu�Ƒ傫���ɂ��āA�ړ������̔����ʂ�Ȃ�����
            else
            {
                m_element0RectTf.anchoredPosition = m_TargetPos[0];
                m_element1RectTf.anchoredPosition = m_TargetPos[1];
                m_element2RectTf.anchoredPosition = m_TargetPos[2];

                m_element0Image.transform.localScale = new Vector3(2, 2, 2);

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
        m_element0RectTf.anchoredPosition = m_TargetPos[0];
        m_element1RectTf.anchoredPosition = m_TargetPos[1];
        m_element2RectTf.anchoredPosition = m_TargetPos[2];
        //���X�g�̐擪�̗v�f���Ō���ɓ����
        Canvas headCanvaes = m_canvaes[0];
        m_canvaes.Remove(headCanvaes);
        m_canvaes.Add(headCanvaes);
        //�����o�ϐ��ɑ��
        m_element0Image = m_canvaes[0].GetComponentInChildren<Image>();
        m_element1Image = m_canvaes[1].GetComponentInChildren<Image>();
        m_element2Image = m_canvaes[2].GetComponentInChildren<Image>();
        m_element0RectTf = m_element0Image.rectTransform;
        m_element1RectTf = m_element1Image.rectTransform;
        m_element2RectTf = m_element2Image.rectTransform;
        //element0�ȊO�̉摜������������
        m_element1Image.transform.localScale = m_element2Image.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //�摜�̕`�揇��擪�̉摜���őO�ʂɂȂ�悤�ύX
        m_canvaes[0].sortingOrder = 3;
        m_canvaes[1].sortingOrder = 2;
        m_canvaes[2].sortingOrder = 1;
    }
    public void SetKobutaDamagedImage(MKKobutaType type, bool damaged)
    {
        m_images[(int)type].GetComponentInChildren<Image>().color = damaged ? new Color(1, 1, 1, 0.4f) : new Color(1, 1, 1, 1);
    }
}
