﻿/*
 * Brian Tria
 * Aug 12, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class LevelManager : MonoSingleton <LevelManager>
{
	private DisplayManager m_displayManager;
	private Transform m_evidenceContainer;
	
	public int CurrentLevel { get; set; }
	
	protected override void Awake ()
	{
		base.Awake ();
		m_displayManager = this.GetComponent<DisplayManager>();
	}
	
	public void Open ()
	{
		m_displayManager.Open();
	}
	
	public void Close ()
	{
		m_displayManager.Close ();
	}
	
	public void AddLevel (Transform p_level)
	{
		p_level.SetParent(this.transform);
		p_level.localPosition = Vector3.zero;
		p_level.localScale = Vector3.one;
		p_level.gameObject.SetActive (false);

		m_evidenceContainer = p_level;
	}

	public void AddEvidence (Transform p_evidence)
	{
		p_evidence.SetParent (m_evidenceContainer);
		p_evidence.localScale = Vector3.one;
	}
	
	public void OpenCurrentLevel ()
	{
		this.transform.GetChild(CurrentLevel).gameObject.SetActive (true);
		//GameStateMachine.Instance.ChangeGameState (GameState.Running);
	}

	public void CloseCurrentLevel ()
	{
		this.transform.GetChild(CurrentLevel).gameObject.SetActive (false);
	}
}
