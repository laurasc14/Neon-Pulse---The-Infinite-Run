using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void GoBackToMainMenu()
    {
        // Torna al menú principal
        SceneManager.LoadScene("MainPrincipal");
    }
}
