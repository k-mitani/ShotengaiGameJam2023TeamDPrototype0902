using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    private static List<ScrollObject> scrollObjects = new List<ScrollObject>();
    public static void SetEnableAll(bool isEnable)
    {
        //全てスクロールを再開(または停止)させる
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
        //毎フレームxポジションを移動
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);

        //目標ポイントまでスクロールしたかをチェック
        if (transform.position.x <= endPosition)
            ScrollEnd();
    }

    private void ScrollEnd()
    {
        //通り過ぎた分
        float diff = transform.position.x - endPosition;
        //ポジションを再設定
        Vector3 restartPosition = transform.position;
        restartPosition.x = startPosition + diff;
        transform.position = restartPosition;

        //同じゲームオブジェクトにアタッチされているコンポーネントにメッセージを送る
        //SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }
}