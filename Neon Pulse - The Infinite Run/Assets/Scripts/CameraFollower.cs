using UnityEngine;
using Cinemachine;

public class CameraFollower : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;

    void Start()
    {
        Invoke(nameof(AssignPlayer), 0.2f); 
    }

    void AssignPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && virtualCam != null)
        {
            virtualCam.Follow = player.transform;
            virtualCam.LookAt = player.transform;
            Debug.Log("C�mera assignada al jugador instanciat.");
        }
        else
        {
            Debug.LogWarning("Player no trobat o c�mera no assignada!");
        }
    }
}