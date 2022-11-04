using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasOption : MonoBehaviour
{
    private TouchPointCheck _player;
    private StageCreate _stage;
    private ScoreBase _score;

    //�ꎞ��~���
    private GameObject _pouseCanvas;

    //�X�e�[�W�N���A���
    private GameObject _clearCanvas;

    //���s���
    private GameObject _fileCanvas;

    //���s�e�L�X�g
    private Text _fileText;
    //�N���A���̃e�L�X�g
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

        _fileText.text = "�X�e�[�W" + _stage._stageNomber + "\n" + "���s";
        _clearText.text = "�X�e�[�W" + _stage._stageNomber + "\n" + "�N���A!!";
        //�N���A�L�����o�X��\��
        _clearCanvas.SetActive(false);
        //���s���̃L�����o�X��\��
        _fileCanvas.SetActive(false);
        //�|�[�Y�L�����o�X���B��
        PouseCanvasClose();
    }

    /// <summary>
    /// �|�[�Y�L�����o�X��������悤�ɂ���
    /// </summary>
    public void PouseCanvasActive()
    {
        if (_player._state != TouchPointCheck.STATE.SERECT) return;
        _player._state = TouchPointCheck.STATE.POUSE;
        _pouseCanvas.SetActive(true);
    }
    /// <summary>
    /// �|�[�Y��ʂ����
    /// </summary>
    public void PouseCanvasClose()
    {
        _player._state = TouchPointCheck.STATE.SERECT;
        _pouseCanvas.SetActive(false);
    }
    /// <summary>
    /// �X�e�[�W���X�L�b�v����
    /// </summary>
    public void StageSkip()
    {
        _stage.StageClear();
    }
    /// <summary>
    /// �X�e�[�W�N���A
    /// </summary>
    public void StageClear()
    {
        _score.StageStarGet(_stage.StageStarNomber[_stage._stageNomber]);
        _clearCanvas.SetActive(true);
        _score.StarSave();
    }
    /// <summary>
    /// �X�e�[�W���s
    /// </summary>
    public void StageFile()
    {
        _fileCanvas.SetActive(true);
    }
}
