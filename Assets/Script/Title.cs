using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Title : MonoBehaviour
{
    //操作説明のキャンバス
    private GameObject _howToPlayCanvas;

    
    void Start()
    {
        _howToPlayCanvas = GameObject.Find("HowToPlayCanvas");


        _howToPlayCanvas.SetActive(false);
    }

    /// <summary>
    /// ステージ画面を開く
    /// </summary>
    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// 操作説明を開く
    /// </summary>
    public void OpenHowToPlay()
    {
        _howToPlayCanvas.SetActive(true);
    }

    /// <summary>
    /// 操作説明を閉じる
    /// </summary>
    public void CloseHowToPlay()
    {
        _howToPlayCanvas.SetActive(false);
    }
}
