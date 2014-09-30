using UnityEngine;  
using System.Collections;  
using System.Collections.Generic;  
using System.IO; 

public class InventoryGUI : MonoBehaviour {
	
	//holds the directory path
	public string directoryPath; 
	//holds the file names with their respective extensions
	private List<string> fileNames;
	//stores the full file path
	private string fullFilePath;
	
	//stores the selected file or an error message
	private string outputMessage = "";

	
	///Variables for displaying the directory contents
	private string dirOutputString = "";
	
	//The initial position of the scroll view
	private Vector2 scrollPosition = Vector2.zero;



	void Start () 
	{
		//Append the '@' verbatim to the directory path string
		this.directoryPath = Application.dataPath + "/Inventory/" + this.directoryPath;
		
		try
		{
			//Get the path of all files inside the directory and save them on a List
			this.fileNames = new List<string>(Directory.GetFiles(this.directoryPath));
			
			//For each string in the fileNames List 
			for (int i = 0; i < this.fileNames.Count; i++)
			{
				//Remove the file path, leaving only the file name and extension
				this.fileNames[i] = Path.GetFileName(this.fileNames[i]);
				//Append each file name to the outputString at a new line
				this.dirOutputString += i.ToString("D5") + "\t-\t" + this.fileNames[i] + "\n";
			}	
		}
		//Catch any of the following exceptions and store the error message at the outputMessage string
		catch (System.UnauthorizedAccessException UAEx)
		{
			this.outputMessage = "ERROR: " + UAEx.Message;
		}
		catch (System.IO.PathTooLongException PathEx)
		{
			this.outputMessage = "ERROR: " + PathEx.Message;
		}
		catch(System.IO.DirectoryNotFoundException DirNfEx)
		{
			this.outputMessage = "ERROR: " + DirNfEx.Message;
		}
		catch(System.ArgumentException aEX)
		{
			this.outputMessage = "ERROR: " + aEX.Message;
		} 
	}
	
	void OnGUI () 
	{
		//If the outputMessage string contains the expression "ERRROR: "
		if(outputMessage.Contains("ERROR: "))
		{
			//Display an error message
			GUI.Label(new Rect(25,Screen.height-50,Screen.width,100),this.outputMessage);
			//Force an early out return of the OnGUI() method. No code below this line will get executed.
			return;
		}
		
		//Display the directory path and the number of files listed
		GUI.Label(new Rect(25, Screen.height - 140 ,Screen.width,100),"Directory: " + this.directoryPath + "   Files found: " + this.fileNames.Count);
		
		//Begin GUILayout
		GUILayout.BeginArea (new Rect (25, Screen.height - 120, Screen.width-50, Screen.height-100));
		//Create a scroll view.
		this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, GUILayout.Width(Screen.width-50), GUILayout.Height(Screen.height-100));
		//Display all of the file names on a TextArea
		GUILayout.TextArea(this.dirOutputString);
		//End of the scroll view
		GUILayout.EndScrollView();
		//End of the GUI Layout
		GUILayout.EndArea();

		//Display the selected file path or an error message at the bottom of the screen
		GUI.Label(new Rect(380, Screen.height - 40, Screen.width, 300),this.outputMessage);
	}
}
