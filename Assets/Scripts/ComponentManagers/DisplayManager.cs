/*
 * Brian Tria
 * Aug 10, 2015
 * 
 * Handles display of gameobject (open, close, animate in, animate out ...)
 * 
 */

using UnityEngine;
using System.Collections;

public class DisplayManager : MonoBehaviour 
{
	public void Open ()
	{
		if(this.gameObject.activeInHierarchy) { return; }
		this.gameObject.SetActive (true);
	}

	public void Close ()
	{
		if(!this.gameObject.activeInHierarchy) { return; }
		this.gameObject.SetActive (false);
	}
}
