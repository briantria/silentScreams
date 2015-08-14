/*
 * Brian Tria
 * Aug 14, 2015
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvidenceBoxManager : MonoSingleton <EvidenceBoxManager> 
{
	[SerializeField] private Transform m_evidenceContainer;

	private DisplayManager m_displayManager;
	private List<Evidence> m_listSelectedEvidences = new List<Evidence> ();

	protected override void Awake ()
	{
		base.Awake ();
		m_displayManager = this.GetComponent<DisplayManager>();
	}
	
	private void UpdateEvidencePositions ()
	{

	}
	
	public void Open ()
	{
		m_displayManager.Open();
		this.transform.SetAsLastSibling ();
	}
	
	public void Close ()
	{
		m_displayManager.Close ();
	}

	public void OnClickAccept ()
	{
		// compute score
		int score = 0;
		foreach (Evidence evidence in m_listSelectedEvidences)
		{
			score += evidence.Score;
		}

		ResultManager.Instance.Score = score;
		UIStateMachine.Instance.ChangeUIState (UIState.OnResultsScreen);
		GameStateMachine.Instance.ChangeGameState (GameState.Result);
	}

	public void OnClickCancel ()
	{
		GameHUDManager.Instance.OnClickEvidenceBox ();
	}

	public void Reset ()
	{
		foreach (Evidence evidence in m_listSelectedEvidences)
		{
			evidence.Reset ();
		}

		m_listSelectedEvidences.Clear ();
	}

	public void AddEvidence (Transform p_evidence)
	{
		m_listSelectedEvidences.Add (p_evidence.GetComponent<Evidence>());
		p_evidence.SetParent (m_evidenceContainer);
		p_evidence.localScale = Vector3.one;
		UpdateEvidencePositions ();
	}

	public void RemoveEvidence (Transform p_evidence)
	{
		m_listSelectedEvidences.Remove (p_evidence.GetComponent<Evidence>());
		LevelManager.Instance.AddEvidence (p_evidence);
		UpdateEvidencePositions ();
	}
}
