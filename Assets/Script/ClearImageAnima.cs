using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClearImageAnima : MonoBehaviour
{
    //�摜�����x
    private float _transparency = 0f;

    //�摜
    private SpriteRenderer _image;
    private CanvasOption _canvas;
    private TouchPointCheck _touchPointCheck;

    //�X�e�[�W�N���A�`�F�b�N
    private bool _stageClarCheck;
    void Start()
    {
        //�摜
        _image = GetComponent<SpriteRenderer>();
        _canvas = FindObjectOfType<CanvasOption>();
        //�C���[�W�̐F��ς���
        _image.color = new Color(1, 1, 1, _transparency);

        //�X�e�[�W�N���A�`�F�b�N
        _stageClarCheck = false;
    }
    void Update()
    {
        if(_stageClarCheck)
        {
            _transparency += Time.deltaTime;
            _image.color = new Color(1, 1, 1, _transparency);
            if(_transparency >= 1)
            {
                _stageClarCheck = false;
                _canvas.StageClear();
            }
        }
    }
    /// <summary>
    /// �X�e�[�W�N���A
    /// </summary>
    public void StageClear()
    {
        _stageClarCheck = true;
    }
}
