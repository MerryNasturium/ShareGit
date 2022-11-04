using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineJudge : MonoBehaviour
{
    //自分が通った線の数
    private int _passLineNomber;

    //ステージの線の数
    private int _stageLineNomber;

    /// <summary>
    /// ステージのライン数を取得
    /// 
    /// </summary>
    /// <param name="StageLineNomber"></param>
    public void GetStageLine(int StageLineNomber)
    {
        _stageLineNomber = StageLineNomber;
    }
    /// <summary>
    /// ステージラインを判定
    /// </summary>
    public bool LineCheck()
    {
        //線を引いたら増やす
        _passLineNomber++;
        bool ClearCheck = false;
        //線を全て通ったならクリア
        if(_stageLineNomber == _passLineNomber) ClearCheck = true;
        return ClearCheck;
    }
    /// <summary>
    /// 失敗した時にステージの線をリセット
    /// </summary>
    public void LineNomberReset()
    {
        _passLineNomber = 0;
    }
}
