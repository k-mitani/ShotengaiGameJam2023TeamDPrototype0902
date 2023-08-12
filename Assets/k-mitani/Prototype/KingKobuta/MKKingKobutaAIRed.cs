using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKKingKobutaAIRed : MonoBehaviour
{
    [SerializeField] private Vector3[] JunkaiPoints;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float waitTime = 1f;
    private int currentIndex = 0;
    private float waitTimeCurrent = 0f;
    private bool isWaiting;
    private MKKingKobutaFace m_face;


    void Start()
    {
        TryGetComponent(out m_face);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_face.hp <= 0) return;

        if (isWaiting)
        {
            waitTimeCurrent += Time.deltaTime;
            if (waitTimeCurrent > waitTime)
            {
                isWaiting = false;
                waitTimeCurrent = 0f;
            }
            return;
        }

        // �ڕW�_
        var target = JunkaiPoints[currentIndex];
        // ���݈ʒu
        var position = transform.localPosition;
        // �ڕW�܂ł̋���
        var distance = Vector3.Distance(position, target);
        // �ڕW�܂ł̕���
        var direction = (target - position).normalized;
        // �ړ���
        var moveAmount = speed * Time.deltaTime * direction;
        // �ڕW�ɓ����������ǂ����̔���
        if (distance < moveAmount.magnitude)
        {
            // ���������ꍇ
            // �ڕW�_�����̓_�ɕύX
            currentIndex++;
            // �ڕW�_���z��͈̔͊O�ɂȂ�����ŏ��̓_�ɖ߂�
            if (currentIndex >= JunkaiPoints.Length)
            {
                currentIndex = 0;
            }
            // �ҋ@���J�n����B
            isWaiting = true;
        }
        else
        {
            // �������Ă��Ȃ��ꍇ
            // �ړ�����
            transform.localPosition += moveAmount;
        }
    }
}
