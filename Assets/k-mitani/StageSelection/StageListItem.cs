using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageName;
    [SerializeField] private TextMeshProUGUI mokuhyoJosenritu;
    [SerializeField] private TextMeshProUGUI bestScore;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI clear;

    private StageSelectionSceneManager parent;
    [System.NonSerialized] public StageInfo info;

    public void Initialize(StageSelectionSceneManager parent, StageInfo info)
    {
        this.parent = parent;
        this.info = info;
        stageName.text = info.stageName;
        mokuhyoJosenritu.text = $"{info.mokuhyoJosenritu}";
        bestScore.text = info.bestScore.HasValue ? $"{info.bestScore.Value}" : "---%";
        image.sprite = Sprite.Create(info.stageImage, new Rect(0, 0, info.stageImage.width, info.stageImage.height), Vector2.zero);
        
        var isClear = info.bestScore > info.mokuhyoJosenritu;
        clear.gameObject.SetActive(isClear);
    }

    public void OnClick()
    {
        parent.OnClickStageListItem(this);
    }
}
