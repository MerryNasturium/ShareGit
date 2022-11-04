using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMarkerOption : MonoBehaviour
{
    //LineRender
    [SerializeField]
    LineRenderer _lineRenderer;

    //�`���Ă鎞�̃J���[
    [SerializeField]
    Material _writingColor;

    //�|�W�V�����␳
    private Vector3 Positioning = new Vector3(0,0,-1);

    void Start()
    {
        //�F
        _lineRenderer.material = _writingColor;
        //���_�̐� 
        _lineRenderer.SetVertexCount(2);
    }
    /// <summary>
    /// �|�W�V�������}�[�L���O����
    /// </summary>
    /// <param name="StartPos"></param>
    /// <param name="EndPos"></param>
    public void PassMarker(Vector3 StartPos,Vector3 EndPos)
    {
        //���_�ݒ�
        _lineRenderer.SetPosition(0, StartPos + Positioning);
        _lineRenderer.SetPosition(1, EndPos + Positioning);
    }

    public void GetWidth(float _setWidth)
    {
        //���̕�
        _lineRenderer.SetWidth(_setWidth, _setWidth);
    }
}
