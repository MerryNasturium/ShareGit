using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //SEのオーディオクリップ
    public AudioClip[] SE_CLIP;
    //SEの番号
    Dictionary<string, int> _seDictionary = new Dictionary<string, int>();
    //SEのオーディオソース
    public AudioSource _seAudio;
    void Start()
    {
        
    }
    /// <summary>
    /// 効果音を流す
    /// </summary>
    public void PlaySE(string SeName)
    {
        //_seAudio.Play(SE_CLIP[_seDictionary[SeName]]);
    }
}
