using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineJudge : MonoBehaviour
{
    //�������ʂ������̐�
    private int _passLineNomber;

    //�X�e�[�W�̐��̐�
    private int _stageLineNomber;

    /// <summary>
    /// �X�e�[�W�̃��C�������擾
    /// 
    /// </summary>
    /// <param name="StageLineNomber"></param>
    public void GetStageLine(int StageLineNomber)
    {
        _stageLineNomber = StageLineNomber;
    }
    /// <summary>
    /// �X�e�[�W���C���𔻒�
    /// </summary>
    public bool LineCheck()
    {
        //�����������瑝�₷
        _passLineNomber++;
        bool ClearCheck = false;
        //����S�Ēʂ����Ȃ�N���A
        if(_stageLineNomber == _passLineNomber) ClearCheck = true;
        return ClearCheck;
    }
    /// <summary>
    /// ���s�������ɃX�e�[�W�̐������Z�b�g
    /// </summary>
    public void LineNomberReset()
    {
        _passLineNomber = 0;
    }
}
