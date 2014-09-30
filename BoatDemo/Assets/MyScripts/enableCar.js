#pragma strict

public var inCar = false;
var labelText = "Press G to exit car";

function Start () {

}

function Update () {
	
}

function inTheCar(condition : boolean)
{
	inCar = condition;
	Debug.Log("car inZone is now: " + inCar); 
			
	
	GetComponent(ThirdPersonController).enabled = !GetComponent(ThirdPersonController).enabled;
	Debug.Log("car script is now: " + GetComponent(ThirdPersonController).enabled);
	
}

function OnGUI()
{
	if (inCar)
	{
		GUI.Box (new Rect (0,0,300,50), labelText);
	}
}