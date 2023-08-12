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

        // 目標点
        var target = JunkaiPoints[currentIndex];
        // 現在位置
        var position = transform.localPosition;
        // 目標までの距離
        var distance = Vector3.Distance(position, target);
        // 目標までの方向
        var direction = (target - position).normalized;
        // 移動量
        var moveAmount = speed * Time.deltaTime * direction;
        // 目標に到着したかどうかの判定
        if (distance < moveAmount.magnitude)
        {
            // 到着した場合
            // 目標点を次の点に変更
            currentIndex++;
            // 目標点が配列の範囲外になったら最初の点に戻す
            if (currentIndex >= JunkaiPoints.Length)
            {
                currentIndex = 0;
            }
            // 待機を開始する。
            isWaiting = true;
        }
        else
        {
            // 到着していない場合
            // 移動する
            transform.localPosition += moveAmount;
        }
    }
}
