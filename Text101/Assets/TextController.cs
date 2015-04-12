using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TextController : MonoBehaviour
{
	public Text textbox;
	
	private enum States
	{
		cell,
		mirror,
		toilet,
		talk_cellmate,
		wall_lockpick,
		hallway,
		death,
		freedom
	}
	;
	
	private States myState; //Current state variable
	private int toilet_count = 0; //Number of times player uses the toilet
	private int talk_count = 0;	//Number of times the player talks to cellmate
	private static bool intel = false; //intel gained by talking to the cellmate
	private string custom_death_message; //Allows the user to know how they died
	
	// Use this for initialization
	void Start ()
	{
		myState = States.cell;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (myState == States.cell) {
			cell ();
		} else if (myState == States.toilet) {
			toilet ();
		} else if (myState == States.death) {
			death ();
		} else if (myState == States.mirror) {
			mirror ();
		} else if (myState == States.talk_cellmate) {
			talk_cellmate ();	
		} else if (myState == States.wall_lockpick) {
			check_wall ();
		} else if (myState == States.hallway) {
			hallway ();
		} else if (myState == States.freedom) {
			freedom ();
		}
	}
	
	void cell ()
	{
		textbox.color = Color.white;
		textbox.text = "You are in a cell.\n" +
			"1 = Use the toilet\n" +
			"2 = Look in mirror\n" +
			"3 = Talk to cellmate";
		//Intel from cellmate => lockpick
		if (intel) {
			textbox.text = textbox.text + "\n4 = Check the brick in the wall";
		}
		
		if (Input.GetKeyDown (KeyCode.Keypad1)) {
			myState = States.toilet;
		} else if (Input.GetKeyDown (KeyCode.Keypad2)) {
			myState = States.mirror;
		} else if (Input.GetKeyDown (KeyCode.Keypad3)) {
			myState = States.talk_cellmate;
		} else if (intel && Input.GetKeyDown (KeyCode.Keypad4)) {
			myState = States.wall_lockpick;
		} 
		
	}
	
	void mirror ()
	{
		textbox.text = "You look in the mirror...you handsome devil you!";
		press_space();
		if (Input.GetKeyDown (KeyCode.Space)) {
			myState = States.cell;
		}
	}
	void toilet ()
	{
		if (toilet_count == 0) {
			textbox.text = "You use the toilet";
		} else if (toilet_count == 1) {
			textbox.text = "You use the toilet again...";
		} else if (toilet_count == 2) {
			textbox.text = "Must've been those beans last night...ugh";
		} else if (toilet_count >= 3) {
			textbox.text = "oh man...I better see a doctor soon...";
		}
		press_space();
		if (Input.GetKeyDown (KeyCode.Space)) {
			toilet_count++;
			myState = States.cell;
		}
	}
	void talk_cellmate ()
	{
		if (talk_count == 0) {
			textbox.text = "\"Hey fuck off mate...\"";
			textbox.color = Color.yellow;
		} else if (talk_count == 1) {
			textbox.text = "\"Ok ok listen, the last guy in your cell spent months crafting a lockpick. He hid " +
				"it in the wall behind that brick there.\"";
			textbox.color = Color.yellow;
		} else {
			textbox.text = "\"Didn't you hear me? Check behind that brick ya twat. Then free me.\"";
			textbox.color = Color.yellow;
		}
		press_space();
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (talk_count == 1) {
				intel = true;
			}
			talk_count++;
			myState = States.cell;
		}
	}
	void check_wall ()
	{
		textbox.text = "You found a lockpick\n" +
		"1 = Use the lockpick on the cell door\n";
		if (Input.GetKeyDown (KeyCode.Keypad1)) {
			myState = States.hallway;
		}
	}
	void death ()
	{
		textbox.text = "You Died\n" + custom_death_message;
		press_space();
		textbox.color = Color.red;
		//Reset all the state variables
		toilet_count = 0;
		talk_count = 0;
		intel = false;
		if (Input.GetKeyDown (KeyCode.Space)) {
			myState = States.cell;
		}
	}
	void hallway (){
		textbox.text = "You are in the hallway.\n" +
		"1 = Use the lockpick to free your cellmate\n" +
		"2 = Make a run for it\n" +
		"3 = Get the guards attention";
		if (Input.GetKeyDown (KeyCode.Keypad1)) {
			myState = States.freedom;
		} else if (Input.GetKeyDown (KeyCode.Keypad2)) {
			myState = States.death;
			custom_death_message = "The guard spotted you and opened fire";
		} else if (Input.GetKeyDown (KeyCode.Keypad3)) {
			myState = States.death;
			custom_death_message = "The guard sees you and throws you back in the "+
								   "cell promising never to bring you food again";
		}
	}
	//Final State
	void freedom (){
		textbox.text = "Your cellmate makes a break for it distracting the " +
		"guard long enough for you to escape that terrible prison";
		textbox.color = Color.blue;
	}
	
	//A user prompt that is used often
	void press_space(){
		textbox.text = textbox.text + "\nPress space to continue";
	}
	
	
	
}
