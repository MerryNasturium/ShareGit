using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageCreate : MonoBehaviour
{
    //スクリプト
    private LineJudge _lineJudge;
    private CanvasOption _canvas;

    //現在進行形のステージ番号
    public int _stageNomber;

    //ステージのライン数
    public int[] StageLineNomber;

    //ステージの星の数
    public int[] StageStarNomber;

    //ステージオブジェクト
    [SerializeField]
    GameObject[] StageObject;

    //生成したステージ
    private GameObject CreateStage;
    private void Awake()
    {
       
        //ステージをロードする
        StageRoad();

        _lineJudge = GetComponent<LineJudge>();
        _canvas = FindObjectOfType<CanvasOption>();
        StageCreates();
    }
    void Start()
    {
        

        
    }
    /// <summary>
    /// ステージクリアした時の番号
    /// </summary>
    public void StageClear()
    {
        _stageNomber++;
        PlayerPrefs.SetInt("StageNomber", _stageNomber);
    }
    /// <summary>
    /// ステージ番号を読み込む
    /// </summary>
    public void StageRoad()
    {
        _stageNomber = PlayerPrefs.GetInt("StageNomber", 0);
    }
    /// <summary>
    /// ステージ作成
    /// </summary>
    public void StageCreates()
    {
        //ステージの生成
        CreateStage = Instantiate(StageObject[_stageNomber]);
        //ラインを取得させる
        _lineJudge.GetStageLine(StageLineNomber[_stageNomber]);
    }
    /// <summary>
    /// 次のステージ
    /// </summary>
    public void NextStage()
    {
        StageClear();
        SceneManager.LoadScene("GameScene");
    }
    /// <summary>
    /// 星を返す
    /// </summary>
    /// <returns></returns>
    public int StageStarReturn()
    {
        return StageStarNomber[_stageNomber];
    }
}
