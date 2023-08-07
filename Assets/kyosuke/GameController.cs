using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //ゲームステート
    enum State
    {
        Play,
        Pause,
        //GameOver
    }

    State state;

    public Text TextPause;
    //public Text TextGameOver;

    private void Start()
    {
        state = State.Play;
        TextPause.gameObject.SetActive(false);
        //TextGameOver.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        switch (state)
        {
            case State.Play:
                //ポーズ
                if(Input.GetKeyDown(KeyCode.Space))
                PauseGame();
                /*
                //プレイヤー死亡時にゲームオーバー
                if (Input.GetKeyDown(KeyCode.O))
                    GameOver();
                */
                break;
            case State.Pause:
                //ポーズ解除
                if (Input.GetKeyDown(KeyCode.Space))
                    ResumeGame();

                //最初からやり直す
                if (Input.GetKeyDown(KeyCode.L))
                    Reload();

                //ステージ選択画面に戻る
                if (Input.GetKeyDown(KeyCode.Return))
                    LoadStageSelect();
                break;
                /*
            case State.GameOver:
                //最初からやり直す
                if (Input.GetKeyDown(KeyCode.L))
                    Reload();

                //ステージ選択画面に戻る
                if (Input.GetKeyDown(KeyCode.Return))
                    LoadStageSelect();
                break;
                */
        }

    }


    void PauseGame()
    {
        state = State.Pause;

        //停止
        Time.timeScale = 0;

        //ポーズ画面のUIを有効にする
        TextPause.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        state = State.Play;

        //再開
        Time.timeScale = 1;

        //ポーズ画面のUIを無効にする
        TextPause.gameObject.SetActive(false);
    }

    void Reload()
    {
        //現在のシーンの名前を取得
        string currentSceneName = SceneManager.GetActiveScene().name;
        //再読み込み
        SceneManager.LoadScene(currentSceneName);

        //timescaleを元に戻す
        Time.timeScale = 1;
    }

    void LoadStageSelect()
    {
        //ステージ選択画面に戻る
        SceneManager.LoadScene("StageSelectionScene");

        //timescaleを元に戻す
        Time.timeScale = 1;
    }

    void GameOver()
    {
        //state = State.GameOver;

        //停止
        Time.timeScale = 0;

        //ゲームオーバー画面のUIを表示
        //TextGameOver.gameObject.SetActive(true);
    }
}