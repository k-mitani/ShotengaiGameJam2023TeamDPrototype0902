using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MKRankingRow : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI rank;
    [SerializeField] public TextMeshProUGUI userName;
    [SerializeField] public TextMeshProUGUI score;

    public void SetData(int rank, string userName, int score, Color color)
    {
        this.rank.text = rank.ToString() + "ˆÊ";
        this.userName.text = userName;
        this.score.text = score.ToString("#,0");
        this.rank.color = color;
        this.userName.color = color;
        this.score.color = color;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
