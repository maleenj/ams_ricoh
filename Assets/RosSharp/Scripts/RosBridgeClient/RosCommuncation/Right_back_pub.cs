﻿/*
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


namespace RosSharp.RosBridgeClient
{
    public class Right_back_pub : UnityPublisher<MessageTypes.Sensor.CompressedImage>
    {

        public Camera cam;
        //public GameObject virtualcamhandler;
        public string FrameId = "Camera";

        private MessageTypes.Sensor.CompressedImage message;
        public int qualityLevel = 100;
        private Virtualcam_handler eye;
        private Texture2D screenShot;

        protected override void Start()
        {
            base.Start();
            InitializeMessage();
            eye = gameObject.AddComponent<Virtualcam_handler>();
        }


        public void publisheye()
        {

            //eye = new Virtualcam_handler();

            message.header.Update();

            cam.enabled = true;
            screenShot = eye.process_virtualcam(cam);
            cam.enabled = false;
            message.data = screenShot.EncodeToJPG(qualityLevel);

            //byte[] bytes = screenShot.EncodeToJPG(qualityLevel);
            //System.IO.File.WriteAllBytes("filename.jpg", bytes);

            Publish(message);
            //Destroy(eye);
            //Destroy(screenShot);
        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Sensor.CompressedImage();
            message.header.frame_id = FrameId;
            message.format = "jpeg";
        }

    }
}