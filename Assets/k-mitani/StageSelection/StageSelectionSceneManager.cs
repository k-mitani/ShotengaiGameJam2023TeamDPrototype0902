using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelectionSceneManager : MonoBehaviour
{
    [SerializeField] private StageInfoList stageInfoList;
    [SerializeField] private StageListItem stageListItemPrefab;
    [SerializeField] private VerticalLayoutGroup stageContainer;

    void Start()
    {
        // リストビューを作る。
        foreach (var stageInfo in stageInfoList.list)
        {
            var item = Instantiate(stageListItemPrefab, stageContainer.transform);
            item.Initialize(this, stageInfo);
        }
        
        // スクロールビューの高さを調整する。
        var childCount = stageContainer.transform.childCount;
        var totalHeight = 0f;
        for (int i = 0; i < childCount; i++)
        {
            var child = stageContainer.transform.GetChild(i);
            var rectTransform = child.GetComponent<RectTransform>();
            totalHeight += rectTransform.rect.height;
        }
        var rectT = stageContainer.GetComponent<RectTransform>();
        var height = totalHeight + childCount * stageContainer.spacing + 40; // 40=下側の余白
        rectT.sizeDelta = new Vector2(rectT.sizeDelta.x, height);
    }

    public void OnClickStageListItem(StageListItem item)
    {
        //OutGameSoundManager.Instance.StopBgm();
        
        var targetScene = item.info.sceneName;
        SceneManager.LoadScene(targetScene);
    }

    public void OnClickTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnClickDebug()
    {
        //ResultViewSceneManager.Parameter = new ResultViewSceneParameter()
        //{
        //    SceneName = "Game Scene",
        //    Score = UnityEngine.Random.value,
        //};
        SceneManager.LoadScene("ResultViewScene");
    }
}
