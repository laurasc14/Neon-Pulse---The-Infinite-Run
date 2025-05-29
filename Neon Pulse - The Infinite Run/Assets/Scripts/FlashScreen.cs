using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FlashScreen : MonoBehaviour
{
    public Image blackScreen;
    public GameObject caughtUI; 
    public float fadeDuration = 0.4f;
    public float displayTime = 2f; // temps abans de reiniciar

    public void FlashAndRestart()
    {
        StartCoroutine(FlashThenShowCaught());
    }

    IEnumerator FlashThenShowCaught()
    {
        float t = 0;

        // Fes el flaix negre
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            blackScreen.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Mostra la UI
        if (caughtUI != null)
            caughtUI.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        // Reinicia
        SceneManager.LoadScene(0);
    }
}