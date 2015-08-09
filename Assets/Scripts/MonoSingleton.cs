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

	public static T Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = FindObjectOfType<T>();

				if (m_instance == null)
				{
					GameObject obj = new GameObject();
					m_instance = obj.AddComponent<T>();
					obj.name = m_instance.name;
				}
			}

			return m_instance;
		}
	}

	protected virtual void Awake ()
	{
		if (m_instance == null)
		{
			m_instance = this as T;
		}
		else
		{
			// restrict to one instance only
			Destroy(this.gameObject);
		}
	}
}
