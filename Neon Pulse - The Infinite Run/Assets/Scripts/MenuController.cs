using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public GameObject firstSelected;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.firstSelectedGameObject = firstSelected;
        EventSystem.current.SetSelectedGameObject(null); // Reinicia focus
        EventSystem.current.SetSelectedGameObject(firstSelected); // Aplica focus de nou
    }

    public void PlayGame() {
        SceneManager.LoadScene(3);
    }

    public void Controls() {
        SceneManager.LoadScene(1);
    }
    public void Options()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit() { 
        Application.Quit();
    }
}
