using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Kirill Kovalevskiy
// Young Chu
// UI

public class GamePlay : MonoBehaviour {

    private static GamePlay _gamePlayInstance;

	GameObject gameManagerObject;
	GameObject difLevelTextObject;
	GameObject timerObject;
	GameObject helpTextObject;
	private GameObject healthTextObj;
	private GameObject miscObj;

	GameManager gameManager;
	private PlayerValues playerVal;

	Text difText;
	Text timerText;
	Text helpText;
	private Text healthText;
	private Text miscText;

	private bool hPressed;
	// bool for if game is over
	private bool end = false;

	// time taken to beat game
	public float endTime;

	// Create a list for help messages
	private string[] helpMsgs = new string[11];

    // Text UI to give notifications
    private GameObject noteObj;
    private Text noteText;




    public static GamePlay gamePlayInstance
    {
        get
        {
            if (_gamePlayInstance == null)
            {
                _gamePlayInstance = GameObject.FindObjectOfType<GamePlay>();
            }

            return _gamePlayInstance;
        }
    }


	// Use this for initialization
	void Start () 
	{
		// keep this throughout scenes
		DontDestroyOnLoad(this.gameObject);

		// Assigning reference variables and acquiring text components
		playerVal = PlayerValues.instance;

		gameManagerObject = GameObject.FindGameObjectWithTag("GameManager");
 		gameManager = gameManagerObject.GetComponent<GameManager>();
		
		timerObject = GameObject.FindGameObjectWithTag("TimerText");
		timerText = timerObject.GetComponent<Text>();

		helpTextObject = GameObject.FindGameObjectWithTag("HelpText");
		helpText = helpTextObject.GetComponent<Text>();

		// assign health text vars
		healthTextObj = GameObject.FindGameObjectWithTag("HealthText");
		healthText = healthTextObj.GetComponent<Text>();

		miscObj = GameObject.FindGameObjectWithTag("Misc");
		miscText = miscObj.GetComponent<Text>();

        // Assigning note vars
        noteObj = GameObject.FindGameObjectWithTag("Notes");
        noteText = noteObj.GetComponent<Text>();


		// ALl messages
		helpMsgs[0] = "Welcome to Twisted World!";
		helpMsgs[1] = "Your goal is to find the exit from this labyrinth, its a glowing sphere";
		helpMsgs[2] = "Use W, A, S, D to move around";
		helpMsgs[3] = "You can do many things by expending artifacts you've collected by using 1, 2, 3, 4";
		helpMsgs[4] = "Use an artifact again to remove its persisting effects, it won't actually cost you anything!";
		helpMsgs[5] = "Now, go ahead and explore!";
		helpMsgs[6] = "";
		helpMsgs[7] = "";
		helpMsgs[8] = "Did you know you can press R to restart a level? Or H to restart the help messages?";
		helpMsgs[9] = "Dont worry, the help messages loop infinitely, so you can wait for that specific tip to come back.";
		helpMsgs[10] = "Press P to return to the main menu";


		StartCoroutine("ShowHelpMsg", helpMsgs);
	}

	void Update()
	{
		// assigning help button bool
		hPressed = Input.GetKeyDown(KeyCode.H);



		// Setting time display as long as game hasn't ended
		if(end != true)
		{
			timerText.text = "Time: " + Time.time;
		}

		// setting health display
		if(playerVal.health > 0 && end == false)
		{
			healthText.text = "Health: " + playerVal.health;
		}
		else if(playerVal.health <= 0 && end == false)
		{
			healthText.text = "GAME OVER: Press R to restart";
		}

		// if h is pressed
		if(hPressed)
		{
			// restart help messages
			StopCoroutine("ShowHelpMsg");
			StartCoroutine("ShowHelpMsg", helpMsgs);
		}

		// if game is over (at end scene)
		if(gameManager.sceneNumber >= gameManager.lastScene && end == false)
		{
			// make so this doesn't run again
			end = true;

			// set final time taken to beat game
			endTime = Time.time;

			// set texts to blank
			timerText.text = "";
			healthText.text = "";
			helpText.text = "";
			miscText.text = "FINAL TIME: " + endTime.ToString();

			// set all help msgs to nothing
			for(int i = 0; i < helpMsgs.Length; i++)
			{
				helpMsgs[i] = "";
			}
		}
	}

	IEnumerator ShowHelpMsg (string[] helpMsgs)
	{
		if(end == false)
		{
			// Iterate trough all messages in the list
			// and display them with given time interval
			foreach (string item in helpMsgs)
			{
				helpText.text = item;
				yield return new WaitForSeconds(5f);
			}
			// loop help messages after coroutine is done
			StartCoroutine("ShowHelpMsg", helpMsgs);
		}
	}

    public IEnumerator DisplayNote(string msg)
    {
        noteText.text = msg;

        yield return new WaitForSeconds(1f);

        noteText.text = " ";
    }

}
