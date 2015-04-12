using UnityEngine;
using System.Collections;

public class NumberWizards : MonoBehaviour
{
	int max;
	int min;
	int guess;	

		void Start()
	{
		StartGame();
	}
	
	void StartGame(){
		max = 1000;
		min = 1;
		guess = Random.Range(min, max);
		print ("8=============================D\n");
		print ("\tWelcome to Number Wizard\n");
		print ("Pick a number in your head between " + min + " and " + max +"\n");
		print ("Press up if above " + guess + ", down if below " + guess + ", return for equal\n");
		max = max + 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			min = guess;
			NextGuess();
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			max = guess;
			NextGuess();
		} else if (Input.GetKeyDown (KeyCode.Return)) {
			print ("I won!");
			StartGame();
		}
	}
	
	void NextGuess(){
		guess = (max + min) / 2;
		print("Higher or lower than " + guess);
		print("Press up if above " + guess + ", down if below " + guess + ", return for equal");
	}
}
