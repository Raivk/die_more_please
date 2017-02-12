using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextBlinker : MonoBehaviour
{

    Text flashingText;
    //flag to determine if you want the blinking to happen
    bool isBlinking = true;

    public void activate()
    {
        //get the Text component
        flashingText = GetComponent<Text>();
        //Call coroutine BlinkText on Start
        StartCoroutine(BlinkText());
    }

    //function to blink the text 
    public IEnumerator BlinkText()
    {
        //blink it forever. You can set a terminating condition depending upon your requirement. Here you can just set the isBlinking flag to false whenever you want the blinking to be stopped.
        while (isBlinking)
        {
            //set the Text's text to blank
            flashingText.enabled = false;
            //display blank text for 0.5 seconds
            yield return new WaitForSeconds(.5f);
            //display “I AM FLASHING TEXT” for the next 0.5 seconds
            flashingText.enabled = true;
            yield return new WaitForSeconds(.5f);
        }
    }
    //your logic here. I have set the isBlinking flag to false after 5 seconds
    public void StopBlinking()
    {
        //stop the blinking
        isBlinking = false;
        //set a different text just for sake of clarity
        flashingText.enabled = true;
    }
}