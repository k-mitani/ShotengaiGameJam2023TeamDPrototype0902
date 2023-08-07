using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    private static List<ScrollObject> scrollObjects = new List<ScrollObject>();
    public static void SetEnableAll(bool isEnable)
    {
        //�S�ăX�N���[�����ĊJ(�܂��͒�~)������
        foreach (ScrollObject so in scrollObjects) so.enabled = isEnable;
    }

    public float speed = 1.0f;
    public float startPosition;
    public float endPosition;

    private void Awake()
    {
        scrollObjects.Add(this);
    }

    private void OnDestroy()
    {
        scrollObjects.Remove(this);
    }

    void Update()
    {
        //���t���[��x�|�W�V�������ړ�
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);

        //�ڕW�|�C���g�܂ŃX�N���[�����������`�F�b�N
        if (transform.position.x <= endPosition)
            ScrollEnd();
    }

    private void ScrollEnd()
    {
        //�ʂ�߂�����
        float diff = transform.position.x - endPosition;
        //�|�W�V�������Đݒ�
        Vector3 restartPosition = transform.position;
        restartPosition.x = startPosition + diff;
        transform.position = restartPosition;

        //�����Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă���R���|�[�l���g�Ƀ��b�Z�[�W�𑗂�
        //SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }
}