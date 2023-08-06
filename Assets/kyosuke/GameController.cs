using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //�Q�[���X�e�[�g
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
                //�|�[�Y
                if(Input.GetKeyDown(KeyCode.Space))
                PauseGame();
                /*
                //�v���C���[���S���ɃQ�[���I�[�o�[
                if (Input.GetKeyDown(KeyCode.O))
                    GameOver();
                */
                break;
            case State.Pause:
                //�|�[�Y����
                if (Input.GetKeyDown(KeyCode.Space))
                    ResumeGame();

                //�ŏ������蒼��
                if (Input.GetKeyDown(KeyCode.L))
                    Reload();

                //�X�e�[�W�I����ʂɖ߂�
                if (Input.GetKeyDown(KeyCode.Return))
                    LoadStageSelect();
                break;
                /*
            case State.GameOver:
                //�ŏ������蒼��
                if (Input.GetKeyDown(KeyCode.L))
                    Reload();

                //�X�e�[�W�I����ʂɖ߂�
                if (Input.GetKeyDown(KeyCode.Return))
                    LoadStageSelect();
                break;
                */
        }

    }


    void PauseGame()
    {
        state = State.Pause;

        //��~
        Time.timeScale = 0;

        //�|�[�Y��ʂ�UI��L���ɂ���
        TextPause.gameObject.SetActive(true);
    }

    void ResumeGame()
    {
        state = State.Play;

        //�ĊJ
        Time.timeScale = 1;

        //�|�[�Y��ʂ�UI�𖳌��ɂ���
        TextPause.gameObject.SetActive(false);
    }

    void Reload()
    {
        //���݂̃V�[���̖��O���擾
        string currentSceneName = SceneManager.GetActiveScene().name;
        //�ēǂݍ���
        SceneManager.LoadScene(currentSceneName);

        //timescale�����ɖ߂�
        Time.timeScale = 1;
    }

    void LoadStageSelect()
    {
        //�X�e�[�W�I����ʂɖ߂�
        SceneManager.LoadScene("StageSelectionScene");

        //timescale�����ɖ߂�
        Time.timeScale = 1;
    }

    void GameOver()
    {
        //state = State.GameOver;

        //��~
        Time.timeScale = 0;

        //�Q�[���I�[�o�[��ʂ�UI��\��
        //TextGameOver.gameObject.SetActive(true);
    }
}