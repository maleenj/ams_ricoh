﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Datalogwriter : MonoBehaviour
{
    public GameObject rosconnect;
    public List<long> subtimes;
    public List<long> pubtimes1;
    public List<long> pubtimes2;
    public List<long> pubtimes3;
    public List<long> pubtimes4;


    public void onclicinvoke() {

        subtimes = rosconnect.GetComponent<RosSharp.RosBridgeClient.ImageSubscriber>().subtimes;
        pubtimes1 = rosconnect.GetComponent<RosSharp.RosBridgeClient.Right_front_pub>().pubtimes;
        pubtimes2 = rosconnect.GetComponent<RosSharp.RosBridgeClient.Right_back_pub>().pubtimes;
        pubtimes3 = rosconnect.GetComponent<RosSharp.RosBridgeClient.Left_front_pub>().pubtimes;
        pubtimes4 = rosconnect.GetComponent<RosSharp.RosBridgeClient.Left_back_pub>().pubtimes;

        long[] subs = subtimes.ToArray();
        SaveArrayAsCSV(subs, "subtimes.csv");

        long[] pubs1 = pubtimes1.ToArray();
        SaveArrayAsCSV(subs, "pubtimes1.csv");

        long[] pubs2 = pubtimes2.ToArray();
        SaveArrayAsCSV(subs, "pubtimes2.csv");

        long[] pubs3 = pubtimes3.ToArray();
        SaveArrayAsCSV(subs, "pubtimes3.csv");

        long[] pubs4 = pubtimes4.ToArray();
        SaveArrayAsCSV(subs, "pubtimes4.csv");

    }


    public static void SaveArrayAsCSV<T>(T[] arrayToSave, String MYfilepath)
    {
        using (StreamWriter file = new StreamWriter(MYfilepath))
        {
            foreach (T item in arrayToSave)
            {
                file.Write(item + ",");
            }
        }
    }

    // Oldwriter
    public void writetofile(String MYfilepath, String logtext)
    {
        StreamWriter filewriter = new StreamWriter(MYfilepath, true);
        filewriter.Write(logtext + Environment.NewLine);
        filewriter.Flush();
        filewriter.Close();
    }




}