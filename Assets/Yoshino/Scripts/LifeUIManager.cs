using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LifeUIManager : MonoBehaviour
{
    [SerializeField, Header("各Canvasリスト(0=真ん中、1=左上、2=右上)")] private List<Canvas> m_canvaes = new List<Canvas>();
    [SerializeField, Header("各画像の位置リスト")] private List<Vector3> m_TargetPos = new List<Vector3>();
    [SerializeField, Header("画像の移動、拡大が終了するまでの時間")] private float m_imageMoveTime = 1f;
    /// <summary>ボタンを押してからの経過時間</summary>
    private float m_elapsedTime = 0f;
    /// <summary>画像が移動中か</summary>
    private bool m_isMoving = false;
    //拡大を行うためのメンバ変数
    private Image m_playerImage = null;
    private Image m_element1Image = null;
    private Image m_element2Image = null;
    //移動を行うためのメンバ変数
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
            //経過時間が画像移動時間を超えるかの判定
            if (m_elapsedTime < m_imageMoveTime)
            {
                //画像の移動の線形補間処理
                float alpha = m_elapsedTime / m_imageMoveTime;
                m_playerRectTf.anchoredPosition = Vector3.Lerp(m_TargetPos[1], m_TargetPos[0], alpha);
                m_element1RectTf.anchoredPosition = Vector3.Lerp(m_TargetPos[2], m_TargetPos[1], alpha);
                m_element2RectTf.anchoredPosition = Vector3.Lerp(m_TargetPos[0], m_TargetPos[2], alpha);
                //画像の拡大の処理
                m_playerImage.transform.localScale = Vector3.Lerp(new Vector3(1.5f, 1.5f, 1.5f), new Vector3(2, 2, 2), alpha);
                //経過時間を計算
                m_elapsedTime += Time.deltaTime;
            }
            //経過時間を超えたら既定の位置と大きさにして、移動中かの判定を通らなくする
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
        //ボタンを押した時の初期化処理+画像の移動処理をスタートする
        UISetUp();
        m_isMoving = true;
        m_elapsedTime = 0f;
    }

    private void UISetUp()
    {
        //各UI画像の位置を既定の位置に初期化
        m_playerRectTf.anchoredPosition = m_TargetPos[0];
        m_element1RectTf.anchoredPosition = m_TargetPos[1];
        m_element2RectTf.anchoredPosition = m_TargetPos[2];
        //リストの先頭の要素を最後尾に入れる
        Canvas head = m_canvaes[0];
        m_canvaes.Remove(head);
        m_canvaes.Add(head);
        //メンバ変数に代入
        m_playerImage = m_canvaes[0].GetComponentInChildren<Image>();
        m_element1Image = m_canvaes[1].GetComponentInChildren<Image>();
        m_element2Image = m_canvaes[2].GetComponentInChildren<Image>();
        m_playerRectTf = m_playerImage.rectTransform;
        m_element1RectTf = m_element1Image.rectTransform;
        m_element2RectTf = m_element2Image.rectTransform;
        //playerImage以外の画像を小さくする
        m_element1Image.transform.localScale = m_element2Image.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        //画像の描画順をプレイヤーの画像が最前面になるよう変更
        m_canvaes[0].sortingOrder = 3;
        m_canvaes[1].sortingOrder = 2;
        m_canvaes[2].sortingOrder = 1;
    }
}
