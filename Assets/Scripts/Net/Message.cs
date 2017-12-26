﻿using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Message
{
    private byte[] data = new byte[1024];
    private int startIndex = 0; //存了多少字节在数组

    public Byte[] Data
    {
        get { return data; }
    }

    public int StartIndex
    {
        get { return startIndex; }
    }

    public int RemainSize
    {
        get { return data.Length - startIndex; }
    }

    //public void AddCount(int count)
    //{
    //    startIndex += count;
    //}

    /// <summary>
    /// 解析数据 and 读取数据
    /// </summary>
    public void ReadMessage(int newDataAmount, Action<ActionCode, string> processDataCallBack)
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4) return;
            int count = BitConverter.ToInt32(data, 0); // 传过来的占位符数据长度, 读取长度
            if (startIndex - 4 > count) //实际的数据长度
            {
                //string s = Encoding.UTF8.GetString(data, 4, count); //读取内容
                //Console.WriteLine("解析出来的数据:" + s);
                ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 4);
                string s = Encoding.UTF8.GetString(data, 8, count - 4);

                processDataCallBack(actionCode, s);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count); //移动数组
                startIndex -= (count + 4);
            }
            else
            {
                return;
            }
        }
    }

    //public static byte[] PackData(RequestCode requestCode, string data)
    //{
    //    byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
    //    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
    //    int dataAmount = requestCodeBytes.Length + dataBytes.Length;
    //    byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
    //    return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();
    //}

    public static byte[] PackData(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] requestCodeBytes = BitConverter.GetBytes((int)requestCode);
        byte[] actionCodeBytes = BitConverter.GetBytes((int)actionCode);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        int dataAmount = requestCodeBytes.Length + actionCodeBytes.Length + dataBytes.Length;
        byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
        return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>()
            .Concat(actionCodeBytes).ToArray<byte>()
            .Concat(dataBytes).ToArray<byte>();
    }
}
