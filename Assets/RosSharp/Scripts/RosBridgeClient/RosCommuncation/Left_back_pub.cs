/*
© CentraleSupelec, 2017
Author: Dr. Jeremy Fix (jeremy.fix@centralesupelec.fr)

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

// Adjustments to new Publication Timing and Execution Framework 
// © Siemens AG, 2018, Dr. Martin Bischoff (martin.bischoff@siemens.com)

using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;


namespace RosSharp.RosBridgeClient
{
    public class Left_back_pub : UnityPublisher<MessageTypes.Sensor.CompressedImage>
    {
        public Camera cam;
        //public GameObject virtualcamhandler;
        public string FrameId = "Camera";

        private MessageTypes.Sensor.CompressedImage message;
        public int qualityLevel = 100;
        private Virtualcam_handler eye;
        private Texture2D screenShot;

        //Time logging
        //private long subtime;
        //private long pubtime;
        //private long diff;
        //public List<long> pubtimes;
        ////private Datalogwriter write;
        ////public GameObject writer;


        protected override void Start()
        {
            base.Start();
            InitializeMessage();
            eye = gameObject.AddComponent<Virtualcam_handler>();
            ////write = writer.GetComponent<Datalogwriter>();
            //pubtimes = new List<long>();
        }


        public void publisheye()
        {
            //long subtime = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            message.header.Update();
            //message.header.stamp=

            screenShot = eye.process_virtualcam(cam);
            message.data = screenShot.EncodeToJPG(qualityLevel);

            ////byte[] bytes = screenShot.EncodeToJPG(qualityLevel);
            ////System.IO.File.WriteAllBytes("filename.jpg", bytes);
            // long pubtime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            //pubtimes.Add(pubtime);
            //Debug.Log("pubtimme1: " + pubtime);
            ////write.writetofile("pub1.csv", pubtime.ToString());
            ////diff = (pubtime - subtime);
            ////Debug.Log("timediff"+ diff);

            Publish(message);

        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Sensor.CompressedImage();
            message.header.frame_id = FrameId;
            message.format = "jpeg";
        }

    }
}



