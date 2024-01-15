using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Script;
using UnityEngine;

public class Cameracontroller : Singleton<Cameracontroller>
{
    public CinemachineVirtualCamera firstCamera;
    public CinemachineVirtualCamera secondCamera;
    
    private void Start()
    {
        MainCam();
    }

    public void MainCam()
    {
        firstCamera.gameObject.SetActive(true);
        secondCamera.gameObject.SetActive(false);
    }

    public void SnakeCam()
    {
        secondCamera.gameObject.SetActive(true);
        firstCamera.gameObject.SetActive(false);
    }
}
