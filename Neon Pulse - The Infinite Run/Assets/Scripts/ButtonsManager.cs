using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{

    public List<Button> buttons;
    public Button current_button;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (current_button != null)
            {
                current_button.OnDeselect(null);
                if (buttons.IndexOf(current_button) > 0)
                {

                    current_button = buttons[buttons.IndexOf(current_button) - 1];
                }
                else
                {
                    current_button = buttons[buttons.Count - 1];
                    
                }
                current_button.Select();
            }

            else
            {
                current_button = buttons[buttons.Count - 1];
                current_button.Select();
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (current_button != null)
            {
                current_button.OnDeselect(null);
                if (buttons.IndexOf(current_button) < buttons.Count-1)
                {

                    current_button = buttons[buttons.IndexOf(current_button) + 1];
                }
                else
                {
                    current_button = buttons[0];

                }
                current_button.Select();
            }

            else
            {
                current_button = buttons[0];
                current_button.Select();
            }

        }
    }
}
