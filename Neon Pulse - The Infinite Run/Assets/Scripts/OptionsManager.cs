using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{

    public void GoBackToMainMenu()
    {
        // Torna al menú principal
        SceneManager.LoadScene("MainPrincipal");
    }
}
