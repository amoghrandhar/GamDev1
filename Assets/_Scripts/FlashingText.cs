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

			flashingText.text = blankText;
			yield return new WaitForSeconds(.5f);
			flashingText.text = "3";
			yield return new WaitForSeconds(.5f);
			flashingText.text = blankText;
			yield return new WaitForSeconds(.5f);
			flashingText.text = "2";
			yield return new WaitForSeconds(.5f); 
			flashingText.text = blankText;
			yield return new WaitForSeconds(.5f);
			flashingText.text = "1";
			yield return new WaitForSeconds(.5f); 
			flashingText.text = blankText;
			yield return new WaitForSeconds(.5f);
			flashingText.text = "Go !";
			yield return new WaitForSeconds(.5f); 
			flashingText.text = blankText;
			yield return new WaitForSeconds(.5f);

	}

	void stopBlinking() {
		isBlinking = false;
		flashingText.text = blankText;
	}

}