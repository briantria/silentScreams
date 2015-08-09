/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class MonoSingleton <T> : MonoBehaviour where T : Component
{
	private static T m_instance = null;
	public  static T Instance { get {return m_instance; }}

	protected virtual void Awake ()
	{
		if (m_instance == null)
		{
			m_instance = this as T;
		}
	}
}
