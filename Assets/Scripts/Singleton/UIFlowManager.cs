/*
 * Brian Tria
 * Aug 10, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class UIFlowManager : MonoSingleton <UIFlowManager> 
{
	private GameObject m_objActiveScreen;

	private string m_strCurrScreen;
	private string m_strNextScreen;

	private delegate void ProcessUIFlow ();
	private ProcessUIFlow m_processUIFlow;

	protected override void Awake ()
	{
		base.Awake ();

		m_strCurrScreen = "";
		m_strNextScreen = "";
		m_processUIFlow = null;
		m_objActiveScreen = null;
	}

	protected void Update ()
	{
		if(m_processUIFlow != null)
		{
			m_processUIFlow ();
		}
	}

	private void WaitForScreenChange ()
	{

	}

	public void SwitchScreen (string p_strScreenName)
	{
		m_strNextScreen = p_strScreenName;
	}
}
