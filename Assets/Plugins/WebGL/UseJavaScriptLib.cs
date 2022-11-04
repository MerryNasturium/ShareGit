using System;
using System.Runtime.InteropServices;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;
public class UseJavaScriptLib : MonoBehaviour
{ 
    /// <summary>
    /// JavaScriptåƒÇ—èoÇµÅB
    /// </summary>
    [DllImport("__Internal")]
    private static extern void CallBackStart();
    [DllImport("__Internal")]
    private static extern void CallBackEnd(int game_score);
    [DllImport("__Internal")]
    private static extern void CallBackShowReward();
    [DllImport("__Internal")]
    private static extern void CallBackFailShowReward();

    //-----------------------------------------------------------------------------------------------------//

    public GameObject gameStartTestObj;

    public void Start()
    {
        gameStartTestObj = GameObject.Find("GameStartTest");
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CallBackStart();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            int game_score = 5;
            CallBackEnd(game_score);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            CallBackShowReward();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CallBackFailShowReward();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            
        }
    }

    public void GameStart()
    {
        gameStartTestObj.SetActive(false);
    }
    public void GameEnd()
    {
        gameStartTestObj.SetActive(true);
    }
    

}