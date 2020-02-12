using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Virtualcam_handler: MonoBehaviour
{
    //public Camera Cam;

    public int resolutionWidth = 426;
    public int resolutionHeight = 426;
    private Texture2D screenShot;
    private Texture2D RscreenShot;
    private Rect rect;

    public Texture2D process_virtualcam(Camera Cam)
    {
       

        RenderTexture rt = new RenderTexture(resolutionWidth, resolutionHeight, 24);
        Cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resolutionWidth, resolutionHeight, TextureFormat.RGB24, false);
        Cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resolutionWidth, resolutionHeight), 0, 0);
        Cam.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        RscreenShot = screenShot;
        Destroy(rt);
        Destroy(screenShot);

        return RscreenShot;


    }

}
