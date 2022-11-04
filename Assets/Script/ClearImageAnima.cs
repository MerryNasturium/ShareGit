using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ClearImageAnima : MonoBehaviour
{
    //画像透明度
    private float _transparency = 0f;

    //画像
    private SpriteRenderer _image;
    private CanvasOption _canvas;
    private TouchPointCheck _touchPointCheck;

    //ステージクリアチェック
    private bool _stageClarCheck;
    void Start()
    {
        //画像
        _image = GetComponent<SpriteRenderer>();
        _canvas = FindObjectOfType<CanvasOption>();
        //イメージの色を変える
        _image.color = new Color(1, 1, 1, _transparency);

        //ステージクリアチェック
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
    /// ステージクリア
    /// </summary>
    public void StageClear()
    {
        _stageClarCheck = true;
    }
}
