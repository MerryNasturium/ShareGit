using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPointCheck : MonoBehaviour
{
    //スクリプト
    private LineJudge _lineJudge;

    private ClearImageAnima _clear;

    private ThinkCountDisplay _countText;

    private StageCreate _stage;

    private ScoreBase _score;

    //思考回数
    public int _thinkingNomber;

    //通ったラインを入れるリスト
    private List<GameObject> PassLineList = new List<GameObject>();
    //通ったラインのオプションを保存するリスト
    private List<WritingPointOption> PassWritingPointList = new List<WritingPointOption>();

    //ラインマーカーオブジェクト
    [SerializeField]
    GameObject _lineMakerObject;

    //マウスポジション
    private Vector3 _mousePos;

    //方向
    private Vector3 _direction = new Vector3(0, 0, 1);

    //レイヤーマスク
    [SerializeField]
    LayerMask _layerMask;

    //筆の状態
    public enum STATE
    {
        SERECT,     //筆を持っている状態
        WRITING,    //描き始めてる状態
        CLRAR,      //ステージクリア
        POUSE,      //ポーズ画面
    }
    public STATE _state;

    //描き始めゲームオブジェクト
    private Vector3 _startWritingPos;

    //LineRender
    private LineRenderer _lineRenderer;

    //描いてる時のカラー
    [SerializeField]
    Material _writingColor;

    //同じオブジェクトかの判断
    private GameObject _sameObjectCheck;

    void Start()
    {
        _lineJudge = FindObjectOfType<LineJudge>();
        _clear = FindObjectOfType<ClearImageAnima>();
        _countText = FindObjectOfType<ThinkCountDisplay>();
        _stage = FindObjectOfType<StageCreate>();
        _score = FindObjectOfType<ScoreBase>();
        Debug.Log(_clear);

        //初期化
        _countText.TextUpdate(_thinkingNomber);

        //子オブジェクトのLineRenderを取得
        _lineRenderer = transform.GetChild(0).gameObject.GetComponent<LineRenderer>();

        //線の幅
        _lineRenderer.SetWidth(0.2f, 0.2f);
        //色
        _lineRenderer.material = _writingColor;
    }
    void Update()
    {
        if(_state == STATE.SERECT)
        {
            WritingStart();
        }
        else if(_state == STATE.WRITING)
        {
            Writing();
        }
    }
    /// <summary>
    /// 描き始める
    /// </summary>
    private void WritingStart()
    {
        //左クリックした時に
        if (Input.GetMouseButtonDown(0))
        {
            //マウスポジション取得
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //頂点の数      初期
            _lineRenderer.SetVertexCount(0);
            //取得した場所にRayを飛ばす
            RaycastHit2D hit = Physics2D.Raycast(_mousePos, _direction, 100, _layerMask);

            //Rayが当たったなら
            if (hit)
            {
                //筆ポイントならば
                if (hit.transform.tag == "WritingPoint")
                {
                    //ステータスを変更
                    _state = STATE.WRITING;
                    //頂点最大数
                    _lineRenderer.SetVertexCount(2);
                    //スタート位置を保存
                    _startWritingPos = hit.transform.position;
                    //位置補正
                    Vector3 SetPositon = new Vector3(_startWritingPos.x, _startWritingPos.y, -1);
                    //頂点設定
                    _lineRenderer.SetPosition(0, SetPositon);
                    _lineRenderer.SetPosition(1, _mousePos);

                    //同じオブジェクトに当たらないように判断する入れ物に入れる
                    _sameObjectCheck = hit.transform.gameObject;
                    //リストに導入
                    PassWritingPointList.Add(_sameObjectCheck.GetComponent<WritingPointOption>());

                    //引く線の太さ調整
                    _lineRenderer.SetWidth(PassWritingPointList[0]._lineScale, PassWritingPointList[0]._lineScale);
                }
            }
        }
    }

    /// <summary>
    /// 描く
    /// </summary>
    private void Writing()
    {
        //書いている途中に離したら
        if(Input.GetMouseButtonUp(0))
        {
            OneWritingFailure();
            _state = STATE.SERECT;
            return;
        }

        //マウスポジション取得
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 MousePos = new Vector3(_mousePos.x, _mousePos.y, -1);
        //頂点設定
        _lineRenderer.SetPosition(1, MousePos);
        WritingPointCheck();
    }
    /// <summary>
    /// 描いている場所にWritingPosがないかの判断
    /// </summary>
    private void WritingPointCheck()
    {
        //取得した場所にRayを飛ばす
        RaycastHit2D hit = Physics2D.Raycast(_mousePos, _direction, 100, _layerMask);

        ///当たった場合
        if(hit)
        {
            if(hit.transform.tag == "WritingPoint")
            {
                if (_sameObjectCheck == hit.transform.gameObject) return;
                //二度描きしてないかの判断
                bool DoubleWriting = false;
                //前回触れたWritingPointのWritingPointOptionを取得
                WritingPointOption SameWritingPoint = hit.transform.GetComponent<WritingPointOption>();
                //触れたWritingPointのWritingPointOptionを取得
                WritingPointOption HitWritingPoint = _sameObjectCheck.GetComponent<WritingPointOption>();
                //前回触れたオブジェクトチェック
                if (!HitWritingPoint.ObjectCheck(hit.transform.gameObject))
                {
                    //二度書きチェックをtrue
                    DoubleWriting = true;
                }
                //今回触れた物チェック
                if (!SameWritingPoint.ObjectCheck(_sameObjectCheck))
                {
                    //二度書きチェックをtrue
                    DoubleWriting = true;
                }
                //二度描きしていないなら
                if (!DoubleWriting)
                {
                    //マーキングする
                    MarkerInstance(_sameObjectCheck.transform.position, hit.transform.position);
                    //オブジェクトチェックを更新
                    _sameObjectCheck = hit.transform.gameObject;
                    //位置補正
                    Vector3 SetPositon = new Vector3(_sameObjectCheck.transform.position.x, _sameObjectCheck.transform.position.y, -1);
                    //当たった位置に線を固定
                    _lineRenderer.SetPosition(0, SetPositon);
                    //リストに導入
                    PassWritingPointList.Add(SameWritingPoint);
                    //全部通ったかの判定
                    if (_lineJudge.LineCheck())
                    {
                        //ステータスをクリアにする
                        _state = STATE.CLRAR;


                        //ステージクリア処理
                        _clear.StageClear();

                        //自分が今書いている位置を修正
                        _lineRenderer.SetPosition(0, Vector3.zero);
                        _lineRenderer.SetPosition(1, Vector3.zero);
                    }
                }
                else
                {
                    //筆ポイント選択に移行
                    _state = STATE.SERECT;
                    OneWritingFailure();
                }
            }
        }
    }
    /// <summary>
    /// 通ったラインを引くマーカーを作成
    /// </summary>
    private void MarkerInstance(Vector3 StartPos,Vector3 EndPos)
    {
        //マーカーを作成
        GameObject Marker = Instantiate(_lineMakerObject);
        //マーカーのオプション
        LineMarkerOption MarkerOption = Marker.GetComponent<LineMarkerOption>();
        //マーカの幅調整
        MarkerOption.GetWidth(PassWritingPointList[0]._lineScale);
        //ラインの位置を設定する
        MarkerOption.PassMarker(StartPos,EndPos);
        //通った道のマーカーを入手
        PassLineList.Add(Marker);
    }
    /// <summary>
    /// 一筆書き失敗
    /// </summary>
    private void OneWritingFailure()
    {
        //もし試行回数が０だった場合
        if(_thinkingNomber == 0)
        {
            Debug.Log("終了");
        }

        //試行回数を減らす
        _thinkingNomber--;

        //自分が今書いている位置を修正
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);

        //マーカーを消す
        MakerErase();
        //通った回数のリセット
        _lineJudge.LineNomberReset();

        //カウント更新
        _countText.TextUpdate(_thinkingNomber);
    }
    /// <summary>
    /// マーカーを全て削除
    /// </summary>
    private void MakerErase()
    {
        for(int i = 0;i < PassLineList.Count;i++)
        {
            Destroy(PassLineList[i]);
        }
        for(int i = 0;i < PassWritingPointList.Count;i++)
        {
            PassWritingPointList[i].FailureCheckReset();
        }
        PassLineList.Clear();
        PassWritingPointList.Clear();
    }

}
