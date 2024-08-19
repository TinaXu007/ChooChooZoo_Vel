using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTestKeyboardControls : MonoBehaviour
{
    private Button[] buttons; // List of the possible inputs (malleable size depending on the possible answers for the current screen test questions)
    private bool keyboardEnabled;
    private int selectedButtonIndex; // Represents the current button that is selected (its position in the "buttons" list)
    private float threshold = 0.25f; // Threshold for when a keypress should be registered

    // Start is called before the first frame update
    void Start()
    {
        keyboardEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyboardEnabled)
        {
            /*if (Mathf.Abs(Input.GetAxis("Horizontal")) > threshold)
            {
                if (threshold > 0) // Move the button selection right
                {
                    // Loop the index back to the starting point if it's at the end or just increment it otherwise
                    selectedButtonIndex = (selectedButtonIndex == buttons.Length - 1) ? 0 : selectedButtonIndex + 1; 
                }
                else // Move button selection left
                { // Loop back to the last button if we're at the start
                    selectedButtonIndex = (selectedButtonIndex == 0) ? buttons.Length - 1 : selectedButtonIndex - 1;
                }
                Debug.Log(buttons[selectedButtonIndex].name);
            }*/

            if (Input.GetKeyDown("d") | /*Input.GetKeyDown(KeyCode.RightArrow) |*/ Input.GetKeyDown(KeyCode.DownArrow)) {
                buttons[selectedButtonIndex].image.color = buttons[selectedButtonIndex].colors.normalColor;
                selectedButtonIndex = (selectedButtonIndex == buttons.Length - 1) ? 0 : selectedButtonIndex + 1;
                buttons[selectedButtonIndex].image.color = buttons[selectedButtonIndex].colors.highlightedColor; // Make sure it's selected
            }
            else if (Input.GetKeyDown("a") | Input.GetKeyDown(KeyCode.LeftArrow))
            {
                buttons[selectedButtonIndex].image.color = buttons[selectedButtonIndex].colors.normalColor;
                selectedButtonIndex = (selectedButtonIndex == buttons.Length - 1) ? 0 : selectedButtonIndex + 1;
                buttons[selectedButtonIndex].image.color = buttons[selectedButtonIndex].colors.highlightedColor; // Make sure it's selected
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                buttons[selectedButtonIndex].onClick.Invoke();
                //Debug.Log(buttons[selectedButtonIndex].name);
            }

            //buttons[selectedButtonIndex].Select();
            //buttons[selectedButtonIndex].image.color = buttons[selectedButtonIndex].colors.highlightedColor; // Make sure it's selected
        }
    }

    public void InitiateControls(GameObject[] buttonsInput, int numAnswers, int startingIndex)
    {
        keyboardEnabled = true;
        buttons = new Button[numAnswers]; 

        for (int i = 0; i < numAnswers; i++)
        {
            buttons[i] = buttonsInput[i].GetComponent<Button>();
        }
        selectedButtonIndex = startingIndex;
    }

    public void DeactivateControls()
    {
        keyboardEnabled = false;
        foreach (Button b in buttons)
        {
            b.image.color = b.colors.normalColor;
            //Debug.Log("hit " + b.image.color);
        }
    }


}
