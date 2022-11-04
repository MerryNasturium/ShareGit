using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageCreate : MonoBehaviour
{
    //�X�N���v�g
    private LineJudge _lineJudge;
    private CanvasOption _canvas;

    //���ݐi�s�`�̃X�e�[�W�ԍ�
    public int _stageNomber;

    //�X�e�[�W�̃��C����
    public int[] StageLineNomber;

    //�X�e�[�W�̐��̐�
    public int[] StageStarNomber;

    //�X�e�[�W�I�u�W�F�N�g
    [SerializeField]
    GameObject[] StageObject;

    //���������X�e�[�W
    private GameObject CreateStage;
    private void Awake()
    {
       
        //�X�e�[�W�����[�h����
        StageRoad();

        _lineJudge = GetComponent<LineJudge>();
        _canvas = FindObjectOfType<CanvasOption>();
        StageCreates();
    }
    void Start()
    {
        

        
    }
    /// <summary>
    /// �X�e�[�W�N���A�������̔ԍ�
    /// </summary>
    public void StageClear()
    {
        _stageNomber++;
        PlayerPrefs.SetInt("StageNomber", _stageNomber);
    }
    /// <summary>
    /// �X�e�[�W�ԍ���ǂݍ���
    /// </summary>
    public void StageRoad()
    {
        _stageNomber = PlayerPrefs.GetInt("StageNomber", 0);
    }
    /// <summary>
    /// �X�e�[�W�쐬
    /// </summary>
    public void StageCreates()
    {
        //�X�e�[�W�̐���
        CreateStage = Instantiate(StageObject[_stageNomber]);
        //���C�����擾������
        _lineJudge.GetStageLine(StageLineNomber[_stageNomber]);
    }
    /// <summary>
    /// ���̃X�e�[�W
    /// </summary>
    public void NextStage()
    {
        StageClear();
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// ����Ԃ�
    /// </summary>
    /// <returns></returns>
    public int StageStarReturn()
    {
        return StageStarNomber[_stageNomber];
    }
}
