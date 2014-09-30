#pragma strict

public var inZone = false;



function Start () {

}

function Update () {

}

function inTheZone(condition : boolean)
{
	inZone = condition;
	Debug.Log("inZone is now: " + inZone); 
			

	GetComponent(ThirdPersonController).enabled =!GetComponent(ThirdPersonController).enabled;
	Debug.Log("script is now: " + GetComponent(ThirdPersonController).enabled);
	
}

