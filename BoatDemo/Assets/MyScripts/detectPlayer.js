#pragma strict

public static var inZone = false;
public static var inCar = false;
var labelText = "Press F to enter car!";


var playerObject : Transform;
var carObject : Transform; 

function Start () {

}

function Update () {
	if (inZone)
	{
		if(Input.GetKeyDown("f"))
		{
			inCar = true;
			inZone = false;
			playerObject.gameObject.SendMessage("inTheZone", true);
			carObject.SendMessage("inTheCar", true);
			playerObject.transform.parent = carObject.transform;
			playerObject.transform.position = carObject.transform.position + Vector3(0,0.5,-2);
			playerObject.transform.rotation = carObject.transform.rotation;
		}
	}

	if (inCar)
	{
		if(Input.GetKey("g"))
		{
			inCar = false;
			playerObject.gameObject.SendMessage("inTheZone", false);
			carObject.SendMessage("inTheCar", false);
			
			playerObject.transform.parent = null;
			
		}
	}
}

function OnTriggerEnter(player : Collider)
{
	if(player.tag == "Player")
	{
		inZone = true;
	}
}

function OnGUI()
{
	if(inZone)
	{	
		GUI.Box (new Rect (0,0,300,50), labelText);
	}
}

function OnTriggerExit()
{
	inZone = false;
}

