using UnityEngine;
using Cinemachine;

public class CameraFollower : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    void Start()
    {
        Invoke(nameof(AssignTarget), 0.2f); // espera que el jugador aparegui
    }

    void AssignTarget()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && virtualCam != null)
        {
            virtualCam.Follow = player.transform;
            virtualCam.LookAt = player.transform;
        }
    }
}