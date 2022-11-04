using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingPointOption : MonoBehaviour
{
    //いくつの線と繋がっているのか
    private int _writingPointNomber;

    //線を通過しているかのチェック
    public bool[] _linePassCheck;

    //線を引く道具
    private LineRenderer _lineRenderer;

   　//ポイントの最大大きさ
    private const float POINT_MAX_SCALE = 0.5f;

    //線の最大太さ
    private const float LINE_MAX_SCALE = 0.2f;

    //線の円の半径
    private float POINT_RADIUS;

    //線の長さ
    public float _lineScale;

    //マテリアル
    [SerializeField]
    Material _lineNoPassColor;

    [Header("このオブジェクトから線を繋ぐオブジェクトを入れる")]
    //このオブジェクトから繋ぐオブジェクト
    [SerializeField]
    GameObject[] _lineConnectPoint;


    void Start()
    {
        //半径取得*2
        POINT_RADIUS = transform.localScale.x;

        //線の長さを決める
        _lineScale = (POINT_RADIUS / POINT_MAX_SCALE) * LINE_MAX_SCALE;

        //線を繋ぐオブジェクトの数
        _writingPointNomber = _lineConnectPoint.Length;

        //通っているかの判断
        _linePassCheck = new bool[_writingPointNomber];

        //線を繋ぐオブジェクト分配列を空ける
        _lineRenderer = transform.GetChild(0).gameObject.GetComponent<LineRenderer>();

        //線の幅
        _lineRenderer.SetWidth(_lineScale, _lineScale);
        //頂点の数           元の頂点数　＊　繋ぐオブジェクトの数
        _lineRenderer.SetVertexCount(2 * _writingPointNomber);
        //色
        _lineRenderer.material = _lineNoPassColor;
        for (int i = 0;i < _writingPointNomber;i++)
        {
            //頂点設定
            _lineRenderer.SetPosition(0 + (i * 2), transform.position);
            _lineRenderer.SetPosition(1 + (i * 2), _lineConnectPoint[i].transform.position);
        }
    }
    /// <summary>
    /// 一度通ったオブジェクトか通ってないオブジェクトかの判断
    /// </summary>
    public bool ObjectCheck(GameObject PassObject)
    {
        //返すbool
        bool PassCheck = false;
        //繋がっているオブジェクト分回す
        for(int i = 0;i < _lineConnectPoint.Length;i++)
        {
            //繋がっているかの判断
            if(_lineConnectPoint[i] == PassObject)
            {
                //そのオブジェクトが既に線が引かれていないかの判断
                if(_linePassCheck[i] == false)
                {
                    PassCheck = true;
                    _linePassCheck[i] = PassCheck;
                }
            }
        }
        return PassCheck;
    }
    /// <summary>
    /// 失敗した時に通ったかのチェックをリセット
    /// </summary>
    public void FailureCheckReset()
    {
        for(int i = 0; i < _linePassCheck.Length;i++)
        {
            _linePassCheck[i] = false;
        }
    }

}
