using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FlashingText : MonoBehaviour {

	public Text flashingText;
	string textToFlash = "Click to Launch";
	string blankText = "";
	//flag to determine if you want the blinking to happen
	bool isBlinking = true;

	void Start(){
		//get the Text component
		flashingText = GetComponent<Text>();
		//Call coroutine BlinkText on Start
		StartCoroutine(BlinkText());
	}

	//function to blink the text 
	public IEnumerator BlinkText(){
		//blink it forever. You can set a terminating condition depending upon your requirement. Here you can just set the isBlinking flag to false whenever you want the blinking to be stopped.
		while(isBlinking){
			//set the Text's text to blank
			flashingText.text = blankText;
			//display blank text for 0.5 seconds
			yield return new WaitForSeconds(.4f);
			//display “I AM FLASHING TEXT” for the next 0.5 seconds
			flashingText.text = textToFlash;
			yield return new WaitForSeconds(.4f); 
		}
	}

	void stopBlinking() {
		isBlinking = false;
		flashingText.text = blankText;
	}

}