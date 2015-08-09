/*
 * Brian Tria
 * Aug 09, 2015
 * 
 */

using UnityEngine;
using System.Collections;

public class ScaleSprite : MonoBehaviour 
{
	[SerializeField] private SpriteRenderer m_spriteToScale;

	protected void Awake ()
	{
		if(m_spriteToScale == null)
		{
			Debug.LogError("ScaleSprite: No reference to m_spriteToScale!");
		}
	}

	protected void Start () 
	{
		FitToScreen();
	}

	private void FitToScreen ()
	{
		Vector2 spriteScale = Vector2.one;
		
		float spriteHeight = m_spriteToScale.bounds.size.y;
		float spriteWidth  = m_spriteToScale.bounds.size.x;

		float worldScreenHeight = Camera.main.orthographicSize * 2;
		float wolddScreenWidth  = worldScreenHeight / Screen.height * Screen.width;

		spriteScale.y = worldScreenHeight / spriteHeight;
		spriteScale.x = wolddScreenWidth  / spriteWidth;

		transform.localScale = spriteScale;
	}
}
