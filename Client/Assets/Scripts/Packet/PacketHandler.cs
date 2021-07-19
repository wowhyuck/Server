﻿using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class PacketHandler
{
    public static void S_ChatHandler(PacketSession session, IPacket packet)
    {
        S_Chat chatPacket = packet as S_Chat;
        ServerSession severSession = session as ServerSession;

        //if (chatPacket.playerId == 1)
        {
            Debug.Log(chatPacket.chat);
            GameObject go = GameObject.Find("Player");
            if (go == null)
                Debug.Log("Player not found");
            else
                Debug.Log("Player found");
        }
        //Console.WriteLine(chatPacket.chat);
    }
}
