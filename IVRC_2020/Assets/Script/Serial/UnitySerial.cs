using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySerial : MonoBehaviour
{
    //先ほど作成したクラス
    public SerialHandler serialHandler;

    private bool[] s;
    [SerializeField] private int number = 4;
    public bool oneS;

    void Start()
    {
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
        s = new bool[number];
        oneS = false;
    }
    
    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        string[] data = message.Split( new string[] { "," }, System.StringSplitOptions.None);//\tを除外してdateに格納
        if (data.Length < 2) return;
        //Debug.Log(message);

        try
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == "0")//押された
                {
                    s[i] = true;
                }
                else if (data[i] == "1")
                {
                    //s[i] = false;//データ更新しなければOK
                }
                else
                {
                    Debug.LogError("シリアル通信で送られてくるデータに誤りがあります。\nThere is an error in the data sent by serial communication.");
                }
            }


            for (int i = 0; i < data.Length; i++)
            {
                oneS = false;
                if(s[i] == false)
                {
                    break;
                }
                oneS = true;

            }//最終的にすべてtrueならoneSはtrueになる



            if (oneS == true)
            {
                Debug.Log("1週を検知");
                for (int i = 0; i < data.Length; i++)
                {
                    s[i] = false;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}