using UnityEngine;
using System;
using System.Collections;
using GameDevLog;
using LuaLogger;
using LuaSave;

public class LuaConsole : MonoBehaviour
{
	// private member variables
	private string myConsole = ""; // holds text entered into the console text field
	private string myConsoleDefault = "Start typing here..."; // this is the default text for the console
	private string myDisplay = "Welcome to the Lua Command Line!"; // this displays entered commands from the console
	private string myMultiLineCommand = ""; // holds a multi-line command for execution
	private float textHeight = 21;
	private Vector2 scrollPosition = Vector2.zero;
	private bool showConsole = false;
	private LuaAPI myLua;
	private string[] toolbarStrings = new string[] {"Single", "Multi-line", "Lua Script"};
	private int toolbarInt = 0;
	private bool newScript = true;
	// Use this for initialization
	void Start ()
	{
		// pass boat reference to Lua API
		myLua = new LuaAPI();
		GDLog.log("LuaConsole.cs: Lua instance created.");
	}
	
	// Here is where we can present the console and handle input
	void OnGUI()
	{
		// button to toggle on/off the console
		if (GUI.Button(new Rect(0, 0, 110, 25), "Toggle Console"))
		{
			showConsole = !showConsole;
			GDLog.log("LuaConsole.cs: showConsole = " + showConsole.ToString());
		}
		
		if (showConsole)
		{
			toolbarInt = GUI.Toolbar(new Rect(0, 30, 230, 25), toolbarInt, toolbarStrings);
			
			bool timeToExecute = false;
			// Check to see if user has pressed the "return" key
			if (Event.current.Equals(Event.KeyboardEvent("return")))
				timeToExecute = true;
			
			float displayHeight = 0; // height of display
			float scrollHeight = 0;  // height of scrollbar
			
			// calculate height of display and scrollbar based on
			// how many commands have been entered
			if (textHeight < 170)
			{
				displayHeight = textHeight;
				scrollHeight = 0;
			}
			else
			{
				displayHeight = 170;
				scrollHeight = textHeight;
			}
			
			// Create display area with (or possibly without) scrollbar
			scrollPosition = GUI.BeginScrollView(new Rect(5, 60, 500, displayHeight), scrollPosition, new Rect(0,0,0,scrollHeight));
			GUI.TextArea(new Rect(0,0,500,textHeight), myDisplay);
			GUI.EndScrollView();
			
			// Create the console for entering in commands
			GUI.SetNextControlName("the_console");
			myConsole = GUI.TextField(new Rect(5, 60 + displayHeight, 500, 22), myConsole);
			
			// If nothing is typed in console, and console is out of focus,
			// display the default text
			if (UnityEngine.Event.current.type == EventType.Repaint)
			{
				if (GUI.GetNameOfFocusedControl() == "the_console")
				{
					if (myConsole == myConsoleDefault) myConsole = "";
				}
				else
				{
					if (myConsole == "") myConsole = myConsoleDefault;
				}
			}
			
			// Ensures commands are only executed when "enter" is pressed
			// and the user has focus on the console
			if (timeToExecute && GUI.GetNameOfFocusedControl() != "the_console")
				timeToExecute = false;
			
			switch (toolbarInt)
			{
				// Single mode
			case 0:
				// detect if we are now exiting Lua Script mode
				if (!newScript)
				{
					myDisplay += "\n" + "**********EXITED: LUA SCRIPT NOT SAVED***********";
					myMultiLineCommand = "";
					textHeight += 15;
					scrollPosition = new Vector2(0, Mathf.Infinity);
					newScript = true;
				}
				
				if (timeToExecute)
				{
					myDisplay += "\n" + myConsole;
					if (!myLua.ExecuteCommand(myConsole))
					{
						myDisplay += "\n" + "ERROR: INVALID COMMAND. REFER TO LOG FOR MORE DETAILS.";
						textHeight += 15;
						scrollPosition = new Vector2(0, Mathf.Infinity);
						myLua = new LuaAPI();
						myConsole = "";
					}
					else
					{
						myConsole = "";
					}
					textHeight += 15;
					scrollPosition = new Vector2(0, Mathf.Infinity);
				}
				break;
			case 1:
				// detect if we are now exiting Lua Script mode
				if (!newScript)
				{
					myDisplay += "\n" + "**********EXITED: LUA SCRIPT NOT SAVED***********";
					GDLog.log("LuaConsole.cs: Lua Script Not Saved");
					myMultiLineCommand = "";
					textHeight += 15;
					scrollPosition = new Vector2(0, Mathf.Infinity);
					newScript = true;
				}
				
				if (timeToExecute)
				{
					myDisplay += "\n" + myConsole;
					if (myConsole == "Execute()")
					{
						GDLog.log("LuaConsole.cs:" + myDisplay + " " + myConsole);
						if (!myLua.ExecuteCommand(myMultiLineCommand))
						{
							myDisplay += "\n" + "ERROR: INVALID COMMAND. REFER TO LOG FOR MORE DETAILS.";

							myLua = new LuaAPI();
							GDLog.log("LuaConsole.cs: New Lua instance created");
						}
						else
						{
							myDisplay += "\n" + "Lua Command successfully executed!";

							GDLog.log("LuaConsole.cs: Lua Command successfully executed.");
						}
						textHeight += 15;
						scrollPosition = new Vector2(0, Mathf.Infinity);
						myConsole = "";
						myMultiLineCommand = "";

					}
					else
					{
						if (myMultiLineCommand == "")
							myMultiLineCommand = myConsole;
						else
							myMultiLineCommand += "\n" + myConsole;
						
						myConsole = "";
					}
					textHeight += 15;
					scrollPosition = new Vector2(0, Mathf.Infinity);
				}
				break;
			case 2:
				if (newScript)
				{
					
					myDisplay += "\n" + "**********BEGIN WRITING LUA SCRIPT**********";
					textHeight += 15;
					scrollPosition = new Vector2(0, Mathf.Infinity);
					newScript = false;
					GDLog.log("LuaConsole.cs: New Lua script being created.");
				}

				if(GUI.Button(new Rect(300, 30, 150, 25), "Change Filename")) {
					LS.changeFileName(myMultiLineCommand);
					myDisplay += "\n" + "**filename Changed**";
					textHeight += 15;
					
						
				}
				
				if (GUI.Button(new Rect(455, 30, 50, 25), "Save"))
				{
					try
					{
						LS.write(myMultiLineCommand);
						myDisplay += "\n" + "**********SUCCESS: LUA SCRIPT SAVED**********";
						GDLog.log("LuaConsole.cs: Lua Script Saved");
					}
					catch
					{
						myDisplay += "\n" + "**********ERROR: COULD NOT SAVE LUA SCRIPT***********";
						GDLog.log("LuaConsole.cs: Lua script cannot be saved");
						myLua = new LuaAPI();
					}
					finally
					{
						toolbarInt = 0; // go back to single mode
						myMultiLineCommand = "";
						textHeight += 15;
						scrollPosition = new Vector2(0, Mathf.Infinity);
						newScript = true;
					}
				}
				if (timeToExecute)
				{
					myDisplay += "\n" + myConsole;
					if (myMultiLineCommand == "")
						myMultiLineCommand = myConsole;
					else
						myMultiLineCommand += "\n" + myConsole;
						
					myConsole = "";
					textHeight += 15;
					scrollPosition = new Vector2(0, Mathf.Infinity);
				}
				break;
			default:
				break;
			}
		}
	}
}
