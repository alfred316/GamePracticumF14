using UnityEngine;
using System;
using System.Collections;
using LuaInterface; //Reference the LuaInterface DLL

public class LuaAPI  {
	// private members
	private GameObject boat;
	private ShipControls shipControls;
	private Lua lua;
	private string drivers = "drivers.lua"; //Filename of the Lua file to load in the Streaming Assets folder
	Queue movementQueue;
	
	// public members
	public LuaFunction boundMessageFunction; //Reference to bound Lua function set within Lua
	
	// Constructor
	public LuaAPI() {
		// create new instance of Lua
		lua = new Lua();
		
		// Initialize array.
		movementQueue = new Queue();
		
		// Get the UnityEngine reference
		lua.DoString("UnityEngine = luanet.UnityEngine");
		
		// get the boat and its controls
		boat = GameObject.FindGameObjectWithTag("TheBoat");
		shipControls = boat.GetComponent<ShipControls>();
		
		//Tell Lua about the LuaBinding object to allow Lua to call C# functions
		lua["luabinding"] = this;
		
		//Run the code contained within the file
		lua.DoFile(Application.streamingAssetsPath + "/" + drivers);
	}
	
	// Tries to execute a Lua command
	public bool ExecuteCommand(string command) {
		try
		{
			lua.DoString(command);
		}
		catch (Exception e)
		{
			Debug.Log(e);
			return false;
		}
		
		return true;
	}
	
	public void BindMessageFunction(LuaFunction func) {
		//Binding
		boundMessageFunction = func;	
	}
	
	public void delegateMoveTheBoat() {
		shipControls.StartCoroutine(shipControls.MoveTheBoat(movementQueue));
	}
		
	public void addMovement(float speedValue, float turningValue, float secondsValue) {
		movementQueue.Enqueue(speedValue);
		movementQueue.Enqueue(turningValue);
		movementQueue.Enqueue(secondsValue);
	}
}