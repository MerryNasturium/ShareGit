using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ThinkCountDisplay : MonoBehaviour
{
    //テキスト
    private Text _countText;

    
    // Start is called before the first frame update
    void Awake()
    {
        _countText = GameObject.Find("CountText").GetComponent<Text>();
    }

    //テキストの更新
    public void TextUpdate(int ThinkNomber)
    {
        _countText.text = ThinkNomber.ToString();
    }
}
