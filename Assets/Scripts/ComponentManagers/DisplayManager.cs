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
		this.gameObject.SetActive (true);
	}
	public void Close ()
	{
		this.gameObject.SetActive (false);
	}
}
