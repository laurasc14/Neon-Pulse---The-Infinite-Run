using System.Collections;
using System.Collections.Generic;
using System;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera topDownCam;
    public CinemachineVirtualCamera cityCam;
    public CinemachineVirtualCamera poliCam;
    public CinemachineVirtualCamera cityCamCrrils;

    private void Awake()
    {
        SetActiveCamera(poliCam);

        Invoke(nameof(SwitchToMain), 2f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SwitchToMain(); // Vista normal
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SetActiveCamera(topDownCam); // Vista zenital
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SetActiveCamera(cityCam); // Vista fons ciutat
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SetActiveCamera(poliCam); // Vista desde els polis
        }

         if(Input.GetKeyDown(KeyCode.G))
        {
            SetActiveCamera(cityCamCrrils); // Vista desde la ciuatat als carrils
        }
    }

    void SwitchToMain()
    {
        SetActiveCamera(mainCam);
    }

    void SetActiveCamera(CinemachineVirtualCamera activeCam)
    {
        // Primer, posa totes les càmeres amb prioritat baixa
        if (mainCam != null) mainCam.Priority = 5;
        if (topDownCam != null) topDownCam.Priority = 5;
        if (cityCam != null) cityCam.Priority = 5;
        if (poliCam != null) poliCam.Priority = 5;
        if (cityCamCrrils != null) cityCamCrrils.Priority = 5;

        // Ara activa la càmera que vols
        if (activeCam != null) activeCam.Priority = 20;
    }
}
