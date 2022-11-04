using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingPointOption : MonoBehaviour
{
    //�����̐��ƌq�����Ă���̂�
    private int _writingPointNomber;

    //����ʉ߂��Ă��邩�̃`�F�b�N
    public bool[] _linePassCheck;

    //������������
    private LineRenderer _lineRenderer;

   �@//�|�C���g�̍ő�傫��
    private const float POINT_MAX_SCALE = 0.5f;

    //���̍ő呾��
    private const float LINE_MAX_SCALE = 0.2f;

    //���̉~�̔��a
    private float POINT_RADIUS;

    //���̒���
    public float _lineScale;

    //�}�e���A��
    [SerializeField]
    Material _lineNoPassColor;

    [Header("���̃I�u�W�F�N�g��������q���I�u�W�F�N�g������")]
    //���̃I�u�W�F�N�g����q���I�u�W�F�N�g
    [SerializeField]
    GameObject[] _lineConnectPoint;


    void Start()
    {
        //���a�擾*2
        POINT_RADIUS = transform.localScale.x;

        //���̒��������߂�
        _lineScale = (POINT_RADIUS / POINT_MAX_SCALE) * LINE_MAX_SCALE;

        //�����q���I�u�W�F�N�g�̐�
        _writingPointNomber = _lineConnectPoint.Length;

        //�ʂ��Ă��邩�̔��f
        _linePassCheck = new bool[_writingPointNomber];

        //�����q���I�u�W�F�N�g���z����󂯂�
        _lineRenderer = transform.GetChild(0).gameObject.GetComponent<LineRenderer>();

        //���̕�
        _lineRenderer.SetWidth(_lineScale, _lineScale);
        //���_�̐�           ���̒��_���@���@�q���I�u�W�F�N�g�̐�
        _lineRenderer.SetVertexCount(2 * _writingPointNomber);
        //�F
        _lineRenderer.material = _lineNoPassColor;
        for (int i = 0;i < _writingPointNomber;i++)
        {
            //���_�ݒ�
            _lineRenderer.SetPosition(0 + (i * 2), transform.position);
            _lineRenderer.SetPosition(1 + (i * 2), _lineConnectPoint[i].transform.position);
        }
    }
    /// <summary>
    /// ��x�ʂ����I�u�W�F�N�g���ʂ��ĂȂ��I�u�W�F�N�g���̔��f
    /// </summary>
    public bool ObjectCheck(GameObject PassObject)
    {
        //�Ԃ�bool
        bool PassCheck = false;
        //�q�����Ă���I�u�W�F�N�g����
        for(int i = 0;i < _lineConnectPoint.Length;i++)
        {
            //�q�����Ă��邩�̔��f
            if(_lineConnectPoint[i] == PassObject)
            {
                //���̃I�u�W�F�N�g�����ɐ���������Ă��Ȃ����̔��f
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
    /// ���s�������ɒʂ������̃`�F�b�N�����Z�b�g
    /// </summary>
    public void FailureCheckReset()
    {
        for(int i = 0; i < _linePassCheck.Length;i++)
        {
            _linePassCheck[i] = false;
        }
    }

}
