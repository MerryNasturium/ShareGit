using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreBase : MonoBehaviour
{
    //�����Ă��鐯
    private int _star;

    //�X�^�[�̐���\������e�L�X�g
    private Text _starNomberText;

    //�X�e�[�W�N���A�������̃X�^�[����\������e�L�X�g
    private Text _clarStarText;

    //�X�e�[�W���s�������ɕ\������e�L�X�g
    private Text _failureStarText;
    private void Awake()
    {
        _starNomberText = GameObject.Find("StarNomberText").GetComponent<Text>();
        _clarStarText = GameObject.Find("ClearStarsText").GetComponent<Text>();
        _failureStarText = GameObject.Find("FileStarsText").GetComponent<Text>();

        StarGet();
        StarDisPlay();
    }

    //���̐���ۑ�����
    public void StarSave()
    {
        PlayerPrefs.SetInt("Star", _star);
    }
    //�����Ăяo��
    public void StarGet()
    {
        _star = PlayerPrefs.GetInt("Star",0);
    }
    //���̐����ʂ�
    public void StarDisPlay()
    {
        _starNomberText.text = "���~" + _star;
        _clarStarText.text = "���~" + _star;
        _failureStarText.text = "���~" + _star;
    }
    //�������
    public void StageStarGet(int StageStar)
    {
        _star += StageStar;
    }
}
