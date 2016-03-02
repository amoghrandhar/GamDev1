using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas playerNumberMenu;
	public Button startText;
	public Button exitText;

	static public int playerCount;

	// Use this for initialization
	void Start () {
		playerCount = -1;
		quitMenu = quitMenu.GetComponent<Canvas> ();
		playerNumberMenu = playerNumberMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;
		playerNumberMenu.enabled = false;
	}

	// When player presses "Exit"
	public void ExitPress(){
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}

	// Player pressed Play
	public void PlayPress(){
		playerNumberMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}

	// Called when player presses no on confirm, or when player cancels out of choosing number of players
	public void NoPress(){
		quitMenu.enabled = false;
		playerNumberMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void OnePlayer(){
		playerCount = 1;
		StartLevel ();
	}

	public void TwoPlayer(){
		playerCount = 2;
		StartLevel ();
	}

	public void ThreePlayer(){
		playerCount = 3;
		StartLevel ();
	}

	public void FourPlayer(){
		playerCount = 4;
		StartLevel ();
	}


	private void StartLevel(){
		SceneManager.LoadScene ("OrbitScene");
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
