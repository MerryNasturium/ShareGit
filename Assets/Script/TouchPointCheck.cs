using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPointCheck : MonoBehaviour
{
    //�X�N���v�g
    private LineJudge _lineJudge;

    private ClearImageAnima _clear;

    private ThinkCountDisplay _countText;

    private StageCreate _stage;

    private ScoreBase _score;

    //�v�l��
    public int _thinkingNomber;

    //�ʂ������C�������郊�X�g
    private List<GameObject> PassLineList = new List<GameObject>();
    //�ʂ������C���̃I�v�V������ۑ����郊�X�g
    private List<WritingPointOption> PassWritingPointList = new List<WritingPointOption>();

    //���C���}�[�J�[�I�u�W�F�N�g
    [SerializeField]
    GameObject _lineMakerObject;

    //�}�E�X�|�W�V����
    private Vector3 _mousePos;

    //����
    private Vector3 _direction = new Vector3(0, 0, 1);

    //���C���[�}�X�N
    [SerializeField]
    LayerMask _layerMask;

    //�M�̏��
    public enum STATE
    {
        SERECT,     //�M�������Ă�����
        WRITING,    //�`���n�߂Ă���
        CLRAR,      //�X�e�[�W�N���A
        POUSE,      //�|�[�Y���
    }
    public STATE _state;

    //�`���n�߃Q�[���I�u�W�F�N�g
    private Vector3 _startWritingPos;

    //LineRender
    private LineRenderer _lineRenderer;

    //�`���Ă鎞�̃J���[
    [SerializeField]
    Material _writingColor;

    //�����I�u�W�F�N�g���̔��f
    private GameObject _sameObjectCheck;

    void Start()
    {
        _lineJudge = FindObjectOfType<LineJudge>();
        _clear = FindObjectOfType<ClearImageAnima>();
        _countText = FindObjectOfType<ThinkCountDisplay>();
        _stage = FindObjectOfType<StageCreate>();
        _score = FindObjectOfType<ScoreBase>();
        Debug.Log(_clear);

        //������
        _countText.TextUpdate(_thinkingNomber);

        //�q�I�u�W�F�N�g��LineRender���擾
        _lineRenderer = transform.GetChild(0).gameObject.GetComponent<LineRenderer>();

        //���̕�
        _lineRenderer.SetWidth(0.2f, 0.2f);
        //�F
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
    /// �`���n�߂�
    /// </summary>
    private void WritingStart()
    {
        //���N���b�N��������
        if (Input.GetMouseButtonDown(0))
        {
            //�}�E�X�|�W�V�����擾
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //���_�̐�      ����
            _lineRenderer.SetVertexCount(0);
            //�擾�����ꏊ��Ray���΂�
            RaycastHit2D hit = Physics2D.Raycast(_mousePos, _direction, 100, _layerMask);

            //Ray�����������Ȃ�
            if (hit)
            {
                //�M�|�C���g�Ȃ��
                if (hit.transform.tag == "WritingPoint")
                {
                    //�X�e�[�^�X��ύX
                    _state = STATE.WRITING;
                    //���_�ő吔
                    _lineRenderer.SetVertexCount(2);
                    //�X�^�[�g�ʒu��ۑ�
                    _startWritingPos = hit.transform.position;
                    //�ʒu�␳
                    Vector3 SetPositon = new Vector3(_startWritingPos.x, _startWritingPos.y, -1);
                    //���_�ݒ�
                    _lineRenderer.SetPosition(0, SetPositon);
                    _lineRenderer.SetPosition(1, _mousePos);

                    //�����I�u�W�F�N�g�ɓ�����Ȃ��悤�ɔ��f������ꕨ�ɓ����
                    _sameObjectCheck = hit.transform.gameObject;
                    //���X�g�ɓ���
                    PassWritingPointList.Add(_sameObjectCheck.GetComponent<WritingPointOption>());

                    //�������̑�������
                    _lineRenderer.SetWidth(PassWritingPointList[0]._lineScale, PassWritingPointList[0]._lineScale);
                }
            }
        }
    }

    /// <summary>
    /// �`��
    /// </summary>
    private void Writing()
    {
        //�����Ă���r���ɗ�������
        if(Input.GetMouseButtonUp(0))
        {
            OneWritingFailure();
            _state = STATE.SERECT;
            return;
        }

        //�}�E�X�|�W�V�����擾
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 MousePos = new Vector3(_mousePos.x, _mousePos.y, -1);
        //���_�ݒ�
        _lineRenderer.SetPosition(1, MousePos);
        WritingPointCheck();
    }
    /// <summary>
    /// �`���Ă���ꏊ��WritingPos���Ȃ����̔��f
    /// </summary>
    private void WritingPointCheck()
    {
        //�擾�����ꏊ��Ray���΂�
        RaycastHit2D hit = Physics2D.Raycast(_mousePos, _direction, 100, _layerMask);

        ///���������ꍇ
        if(hit)
        {
            if(hit.transform.tag == "WritingPoint")
            {
                if (_sameObjectCheck == hit.transform.gameObject) return;
                //��x�`�����ĂȂ����̔��f
                bool DoubleWriting = false;
                //�O��G�ꂽWritingPoint��WritingPointOption���擾
                WritingPointOption SameWritingPoint = hit.transform.GetComponent<WritingPointOption>();
                //�G�ꂽWritingPoint��WritingPointOption���擾
                WritingPointOption HitWritingPoint = _sameObjectCheck.GetComponent<WritingPointOption>();
                //�O��G�ꂽ�I�u�W�F�N�g�`�F�b�N
                if (!HitWritingPoint.ObjectCheck(hit.transform.gameObject))
                {
                    //��x�����`�F�b�N��true
                    DoubleWriting = true;
                }
                //����G�ꂽ���`�F�b�N
                if (!SameWritingPoint.ObjectCheck(_sameObjectCheck))
                {
                    //��x�����`�F�b�N��true
                    DoubleWriting = true;
                }
                //��x�`�����Ă��Ȃ��Ȃ�
                if (!DoubleWriting)
                {
                    //�}�[�L���O����
                    MarkerInstance(_sameObjectCheck.transform.position, hit.transform.position);
                    //�I�u�W�F�N�g�`�F�b�N���X�V
                    _sameObjectCheck = hit.transform.gameObject;
                    //�ʒu�␳
                    Vector3 SetPositon = new Vector3(_sameObjectCheck.transform.position.x, _sameObjectCheck.transform.position.y, -1);
                    //���������ʒu�ɐ����Œ�
                    _lineRenderer.SetPosition(0, SetPositon);
                    //���X�g�ɓ���
                    PassWritingPointList.Add(SameWritingPoint);
                    //�S���ʂ������̔���
                    if (_lineJudge.LineCheck())
                    {
                        //�X�e�[�^�X���N���A�ɂ���
                        _state = STATE.CLRAR;


                        //�X�e�[�W�N���A����
                        _clear.StageClear();

                        //�������������Ă���ʒu���C��
                        _lineRenderer.SetPosition(0, Vector3.zero);
                        _lineRenderer.SetPosition(1, Vector3.zero);
                    }
                }
                else
                {
                    //�M�|�C���g�I���Ɉڍs
                    _state = STATE.SERECT;
                    OneWritingFailure();
                }
            }
        }
    }
    /// <summary>
    /// �ʂ������C���������}�[�J�[���쐬
    /// </summary>
    private void MarkerInstance(Vector3 StartPos,Vector3 EndPos)
    {
        //�}�[�J�[���쐬
        GameObject Marker = Instantiate(_lineMakerObject);
        //�}�[�J�[�̃I�v�V����
        LineMarkerOption MarkerOption = Marker.GetComponent<LineMarkerOption>();
        //�}�[�J�̕�����
        MarkerOption.GetWidth(PassWritingPointList[0]._lineScale);
        //���C���̈ʒu��ݒ肷��
        MarkerOption.PassMarker(StartPos,EndPos);
        //�ʂ������̃}�[�J�[�����
        PassLineList.Add(Marker);
    }
    /// <summary>
    /// ��M�������s
    /// </summary>
    private void OneWritingFailure()
    {
        //�������s�񐔂��O�������ꍇ
        if(_thinkingNomber == 0)
        {
            Debug.Log("�I��");
        }

        //���s�񐔂����炷
        _thinkingNomber--;

        //�������������Ă���ʒu���C��
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);

        //�}�[�J�[������
        MakerErase();
        //�ʂ����񐔂̃��Z�b�g
        _lineJudge.LineNomberReset();

        //�J�E���g�X�V
        _countText.TextUpdate(_thinkingNomber);
    }
    /// <summary>
    /// �}�[�J�[��S�č폜
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
