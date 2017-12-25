﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using Common;
using System;

/// <summary>
/// 管理和服务器端的socket连接
/// </summary>
public class ClientManager :BaseManager {

    private const string IP = "127.0.0.1";
    private const int PORT = 6688;
    private Socket client;
    private Message msg = new Message();


    public ClientManager(GameFacade facade) : base(facade) { }

    public override void OnInit()
    {
        base.OnInit();

        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            client.Connect(IP, PORT);
        }
        catch (System.Exception e)
        {
            Debug.Log("无法连接到服务器" + e);
        }

    }

    private void Start()
    {
        client.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, null);
    }

    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int count = client.EndReceive(ar);
            msg.ReadMessage(count, OnProcessDataCallBack);
            Start();
        }
        catch (Exception e)
        {
            Debug.Log("接收数据异常" + e);
        }

    }

    private void OnProcessDataCallBack(RequestCode requestCode, string data)
    {
        facade.HandleReponse(requestCode, data);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        client.Send(bytes);
    }

    public override void OnDestory()
    {
        base.OnDestory();
        try
        {
            client.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log("无法关闭跟服务器端的链接" + e);
        }
    }


}