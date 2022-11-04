using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOption : MonoBehaviour
{
    private TouchPointCheck _player;
    private StageCreate _stage;
    private ScoreBase _score;

    //一時停止画面
    private GameObject _pouseCanvas;

    //ステージクリア画面
    private GameObject _clearCanvas;

    //失敗画面
    private GameObject _fileCanvas;

    //失敗テキスト
    private Text _fileText;
    //クリア時のテキスト
    private Text _clearText;
    void Start()
    {
        _player = FindObjectOfType<TouchPointCheck>();
        _score = FindObjectOfType<ScoreBase>();
        _pouseCanvas = GameObject.Find("PouseCanvas");
        _clearCanvas = GameObject.Find("StageClearCanvas");
        _fileCanvas = GameObject.Find("StageFailCanvas");
        _stage = FindObjectOfType<StageCreate>();

        _fileText = GameObject.Find("FlieText").GetComponent<Text>();
        _clearText = GameObject.Find("ClearText").GetComponent<Text>();

        _fileText.text = "ステージ" + _stage._stageNomber + "\n" + "失敗";
        _clearText.text = "ステージ" + _stage._stageNomber + "\n" + "クリア!!";
        //クリアキャンバス非表示
        _clearCanvas.SetActive(false);
        //失敗時のキャンバス非表示
        _fileCanvas.SetActive(false);
        //ポーズキャンバスを隠す
        PouseCanvasClose();
    }

    /// <summary>
    /// ポーズキャンバスを見えるようにする
    /// </summary>
    public void PouseCanvasActive()
    {
        if (_player._state != TouchPointCheck.STATE.SERECT) return;
        _player._state = TouchPointCheck.STATE.POUSE;
        _pouseCanvas.SetActive(true);
    }
    /// <summary>
    /// ポーズ画面を閉じる
    /// </summary>
    public void PouseCanvasClose()
    {
        _player._state = TouchPointCheck.STATE.SERECT;
        _pouseCanvas.SetActive(false);
    }
    /// <summary>
    /// ステージをスキップする
    /// </summary>
    public void StageSkip()
    {
        _stage.StageClear();
    }
    /// <summary>
    /// ステージクリア
    /// </summary>
    public void StageClear()
    {
        _score.StageStarGet(_stage.StageStarNomber[_stage._stageNomber]);
        _clearCanvas.SetActive(true);
        _score.StarSave();
    }
    /// <summary>
    /// ステージ失敗
    /// </summary>
    public void StageFile()
    {
        _fileCanvas.SetActive(true);
    }
}
