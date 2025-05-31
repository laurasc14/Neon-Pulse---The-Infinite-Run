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

    private void Awake()
    {
        SetActiveCamera(poliCam);

        Invoke(nameof(SwitchToMain), 1.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SetActiveCamera(mainCam); // Vista normal
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
    }

    void SwitchToMain()
    {
        SetActiveCamera(mainCam);
    }

    void SetActiveCamera(CinemachineVirtualCamera activeCam)
    {
        // Primer, posa totes les c�meres amb prioritat baixa
        if (mainCam != null) mainCam.Priority = 5;
        if (topDownCam != null) topDownCam.Priority = 5;
        if (cityCam != null) cityCam.Priority = 5;
        if (poliCam != null) poliCam.Priority = 5;

        // Ara activa la c�mera que vols
        if (activeCam != null) activeCam.Priority = 20;
    }
}
