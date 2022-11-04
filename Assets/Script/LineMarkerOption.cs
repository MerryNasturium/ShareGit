using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMarkerOption : MonoBehaviour
{
    //LineRender
    [SerializeField]
    LineRenderer _lineRenderer;

    //描いてる時のカラー
    [SerializeField]
    Material _writingColor;

    //ポジション補正
    private Vector3 Positioning = new Vector3(0,0,-1);

    void Start()
    {
        //色
        _lineRenderer.material = _writingColor;
        //頂点の数 
        _lineRenderer.SetVertexCount(2);
    }
    /// <summary>
    /// ポジションをマーキングする
    /// </summary>
    /// <param name="StartPos"></param>
    /// <param name="EndPos"></param>
    public void PassMarker(Vector3 StartPos,Vector3 EndPos)
    {
        //頂点設定
        _lineRenderer.SetPosition(0, StartPos + Positioning);
        _lineRenderer.SetPosition(1, EndPos + Positioning);
    }

    public void GetWidth(float _setWidth)
    {
        //線の幅
        _lineRenderer.SetWidth(_setWidth, _setWidth);
    }
}
