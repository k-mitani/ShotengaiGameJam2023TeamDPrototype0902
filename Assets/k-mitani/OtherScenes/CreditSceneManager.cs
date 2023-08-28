using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform creditPanel;
    [SerializeField]
    private float creditPanelMoveSpeed = 1.0f;
    [SerializeField]
    private float creditPanelMoveDuration = 1.0f;
    [SerializeField]
    private float startYPosition = 0;

    private float creditPanelMoveTimer = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        creditPanel.position = new Vector3(creditPanel.transform.position.x, startYPosition, creditPanel.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // 設定時間の間、クレジットを上に移動させる。
        if (creditPanelMoveDuration > creditPanelMoveTimer)
        {
            creditPanelMoveTimer += Time.deltaTime;
            creditPanel.position += new Vector3(0, creditPanelMoveSpeed * Time.deltaTime, 0);
        }
    }

    public void OnGameStartClick()
    {
        SceneManager.LoadScene("StageSelectionScene");
    }
}
