using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    //��������̃L�����o�X
    private GameObject _howToPlayCanvas;

    
    void Start()
    {
        _howToPlayCanvas = GameObject.Find("HowToPlayCanvas");


        _howToPlayCanvas.SetActive(false);
    }

    /// <summary>
    /// �X�e�[�W��ʂ��J��
    /// </summary>
    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// ����������J��
    /// </summary>
    public void OpenHowToPlay()
    {
        _howToPlayCanvas.SetActive(true);
    }

    /// <summary>
    /// ������������
    /// </summary>
    public void CloseHowToPlay()
    {
        _howToPlayCanvas.SetActive(false);
    }
}
