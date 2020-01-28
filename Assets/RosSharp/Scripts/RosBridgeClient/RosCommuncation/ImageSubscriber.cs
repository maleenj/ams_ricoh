/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System;
using System.Collections.Generic;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(RosConnector))]
    public class ImageSubscriber : UnitySubscriber<MessageTypes.Sensor.CompressedImage>
    {
        //public MeshRenderer meshRenderer;

        private Texture2D texture2D;
        private byte[] imageData;
        private bool isMessageReceived;

        private MessageTypes.Std.Time stamp;

        //Shphere stuff
        public GameObject sphere1;
        public GameObject sphere2;

        //Unity event trigger
        //public UnityEvent img_recieved;

        //Timing log

        //private long subtime;
        //private long subtime2;
        //public List<long> subtimes;
        //public List<long> subtimes2;
        //public List<long> subdiff;

        ////private Datalogwriter write;
        ////public GameObject writer;

        public Right_front_pub right_Front_Pub;
        public Right_back_pub right_Back_Pub;
        public Left_front_pub left_Front_Pub;
        public Left_back_pub left_Back_Pub;


        protected override void Start()
        {
            
            Screen.SetResolution(640, 480, true);
            base.Start();
            texture2D = new Texture2D(1, 1);

            //subtimes = new List<long>();
            //subtimes2 = new List<long>();
            //subdiff = new List<long>();

            ////write = writer.GetComponent<Datalogwriter>();
            right_Front_Pub = gameObject.GetComponent<Right_front_pub>();
            right_Back_Pub = gameObject.GetComponent<Right_back_pub>();
            left_Front_Pub = gameObject.GetComponent<Left_front_pub>();
            left_Back_Pub = gameObject.GetComponent<Left_back_pub>();


        }

        private void Update()
        {
            if (isMessageReceived == true)
            {
                ProcessMessage();
            }
        }

        protected override void ReceiveMessage(MessageTypes.Sensor.CompressedImage compressedImage)
        {

            //subtime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            ////Debug.Log("subtime: " + subtime);
            ////write.writetofile("subtime1.csv",subtime.ToString());
            //subtimes.Add(subtime);

            if (isMessageReceived == false)
            {
                imageData = compressedImage.data;
                stamp = compressedImage.header.stamp;
                Debug.Log("Stamp: " + stamp.ToString());
                isMessageReceived = true;
            }

            
        }

        private void ProcessMessage()
        {

            texture2D.LoadImage(imageData);
            texture2D.Apply();

            sphere1.GetComponent<Renderer>().material.mainTexture = texture2D;
            sphere2.GetComponent<Renderer>().material.mainTexture = texture2D;

            //subtime2 = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //subtimes2.Add(subtime2);
            //subdiff.Add(subtime2 - subtime);
            //Debug.Log("subdif: " + (subtime2- subtime));

            right_Front_Pub.publisheye(stamp);
            right_Back_Pub.publisheye(stamp);
            left_Front_Pub.publisheye(stamp);
            left_Back_Pub.publisheye(stamp);

            //img_recieved.Invoke();



            isMessageReceived = false;
        }

    }
}

