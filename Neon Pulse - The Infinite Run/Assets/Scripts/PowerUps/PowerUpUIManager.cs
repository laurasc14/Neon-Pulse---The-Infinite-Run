using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class PowerUpUIManager : MonoBehaviour
{
    public TMP_Text powerUpText; // Referència al text de la UI
    public Image iconImage;
    public Animator powerUpAnimator;
    public GameObject powerUI;
    public GameObject powerUPInfoHolder;

    public Sprite speedIcon;
    public Sprite jumpIcon;
    public Sprite shieldIcon;
    public Sprite magnetIcon;

    private float powerUpEndTime; // Temps quan s'acaba el power-up
    private bool powerUpActive = false; // Indica si un power-up està actiu

    struct uipowerUPData {
        public float time;
        public int index;
        public GameObject obj;
        public string name;
    }

    private List<uipowerUPData> displayData = new List<uipowerUPData>(); 


    public Dictionary<string, Sprite> powerUpIcons;


    private void Awake()
    {
        powerUpIcons = new Dictionary<string, Sprite>()
    {
        { "speed", speedIcon },
        { "jump", jumpIcon },
        { "shield", shieldIcon },
        { "magnet", magnetIcon }
    };
    }
    void Start()
    {

        //DisplayPowerUp("shield_powerup", 5f);

        // Comença amb el text ocult.
        //powerUpText.gameObject.SetActive(false);

        if (powerUpText != null)
            powerUpText.gameObject.SetActive(false);

        if (iconImage != null)
            iconImage.enabled = false;
    }

    void Update()
    {
        // Si hi ha un power-up actiu, actualitza la seva durada
        if (displayData.Count > 0)
        {

            List<uipowerUPData> removeList = new List<uipowerUPData>();

            foreach (uipowerUPData data in displayData) { 
                if(Time.time > data.time)
                {

                    Destroy(data.obj);
                    removeList.Add(data);

                }

            }

            foreach (uipowerUPData data in removeList)
            {
                displayData.Remove(data);

            }

            removeList.Clear();

            //powerUpText.gameObject.SetActive(false); // Amaga el text quan acabi
            //iconImage.enabled = false;
            powerUpActive = false;
        }
    }

    public void DisplayPowerUp(string powerUpName, float duration)
    {
        powerUpName = powerUpName.ToLower();

        // Mostrar el nom del power-up
        if (powerUpText != null)
        {
            powerUpText.text = $"{powerUpName} activat!";
            powerUpText.gameObject.SetActive(true); // Fa visible el text
        }


        if ( powerUpIcons.ContainsKey(powerUpName.ToLower()))
        {
            //iconImage.sprite = powerUpIcons[powerUpName.ToLower()];
            //iconImage.enabled = true;

            uipowerUPData temp = new uipowerUPData();
            bool existe = false;


            foreach (uipowerUPData data in displayData)
            {
                if (data.name == powerUpName.ToLower())
                {
                    temp = data;
                    existe = true;
                }

            }
            Debug.Log("hasta aqui");

            if (existe)
            {
                uipowerUPData aux2;
                aux2.time = Time.time + duration;
                aux2.index = temp.index;
                aux2.obj = temp.obj;
                aux2.name = temp.name;
                displayData[displayData.IndexOf(temp)] = aux2;
            }
            else
            {

                GameObject uiImagen = Instantiate(powerUI, powerUPInfoHolder.transform);
                uiImagen.GetComponent<Image>().sprite = powerUpIcons[powerUpName.ToLower()];
                uipowerUPData aux;
                aux.time = Time.time + duration;
                aux.index = displayData.Count;
                aux.obj = uiImagen;
                aux.name = powerUpName.ToLower();

                displayData.Add(aux);
            }
        }


        // Activar animació i control de duració
        powerUpEndTime = Time.time + duration; // Estableix quan ha de desaparèixer
        powerUpActive = true;

        if (powerUpAnimator != null)
        {
            powerUpAnimator.SetTrigger("Show");
        }
    }

    public void HideUI()
    {
        if (powerUpText != null)
            powerUpText.gameObject.SetActive(false);

        if (iconImage != null)
            iconImage.enabled = false;

        powerUpActive = false;
    }


}
