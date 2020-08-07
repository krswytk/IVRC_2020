using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySerial : MonoBehaviour
{
    //先ほど作成したクラス
    public SerialHandler serialHandler;

    private bool[] s;
    [SerializeField] private int number = 4;
    
  void Start()
    {
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
        s = new bool[number];
    }
    
    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        string[] data = message.Split( new string[] { "\t" }, System.StringSplitOptions.None);//\tを除外してdateに格納
        if (data.Length < 2) return;

        try
        {
            Debug.Log(message + "+" + data[0]);


            for(int i = 0; i < data.Length; i++)
            {
                if (data[i] == "1")
                {
                    s[i] = true;
                }
                else if (data[i] == "0")
                {
                    s[i] = false;
                }
                else
                {
                    Debug.LogError("シリアル通信で送られてくるデータに誤りがあります。\nThere is an error in the data sent by serial communication.");
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
}