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
    public class Original_publish : UnityPublisher<MessageTypes.Sensor.CompressedImage>
    {
        //public Camera Cam;
        public string FrameId = "Camera";
        public int resolutionWidth = 480;
        public int resolutionHeight = 480;
        [Range(0, 100)]
        public int qualityLevel = 100;

        private MessageTypes.Sensor.CompressedImage message;
        private Texture2D texture2D;
        private Rect rect;
        private float starttime;

        public GameObject rosconnect;
        private bool msgrecieve;


        public void publisheye(Camera Cam)
        {
            //msgrecieve = rosconnect.GetComponent<ImageSubscriber>().isMessageReceived;


            //if (msgrecieve == true)
            //{
            InitializeMessage();


            //Camera Cam = Camera.main;

            RenderTexture rt = new RenderTexture(resolutionWidth, resolutionHeight, 24);
            Cam.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resolutionWidth, resolutionHeight, TextureFormat.RGB24, false);
            Cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resolutionWidth, resolutionHeight), 0, 0);
            Cam.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);



            message.header.Update();

            message.data = screenShot.EncodeToJPG(qualityLevel);
            Publish(message);
            //}




        }

        private void InitializeMessage()
        {
            message = new MessageTypes.Sensor.CompressedImage();
            message.header.frame_id = FrameId;
            message.format = "jpeg";
        }

        //private void UpdateMessage()
        //{
        //    message.header.Update();
        //    texture2D.ReadPixels(rect, 0, 0);
        //    message.data = texture2D.EncodeToJPG(qualityLevel);
        //    Publish(message);
        //}

    }
}


//namespace RosSharp.RosBridgeClient
//{
//    public class ImagePublisher : UnityPublisher<MessageTypes.Sensor.CompressedImage>
//    {
//        public Camera ImageCamera;
//        public string FrameId = "Camera";
//        public int resolutionWidth = 640;
//        public int resolutionHeight = 480;
//        [Range(0, 100)]
//        public int qualityLevel = 50;

//        private MessageTypes.Sensor.CompressedImage message;
//        private Texture2D texture2D;
//        private Rect rect;

//        protected override void Start()
//        {
//            base.Start();
//            InitializeGameObject();
//            InitializeMessage();
//            Camera.onPostRender += UpdateImage;
//        }

//        private void UpdateImage(Camera _camera)
//        {
//            if (texture2D != null && _camera == this.ImageCamera)
//                UpdateMessage();
//        }

//        private void InitializeGameObject()
//        {
//            texture2D = new Texture2D(resolutionWidth, resolutionHeight, TextureFormat.RGB24, false);
//            rect = new Rect(0, 0, resolutionWidth, resolutionHeight);
//            ImageCamera.targetTexture = new RenderTexture(resolutionWidth, resolutionHeight, 24);
//        }

//        private void InitializeMessage()
//        {
//            message = new MessageTypes.Sensor.CompressedImage();
//            message.header.frame_id = FrameId;
//            message.format = "jpeg";
//        }

//        private void UpdateMessage()
//        {
//            message.header.Update();
//            texture2D.ReadPixels(rect, 0, 0);
//            message.data = texture2D.EncodeToJPG(qualityLevel);
//            Publish(message);
//        }

//    }
//}



