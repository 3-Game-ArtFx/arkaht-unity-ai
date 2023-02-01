using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private ScoreUI blueScoreUI;
	[SerializeField]
    private ScoreUI redScoreUI;

    private List<AIMover> robots = new();

	void Start()
	{
		foreach ( GameObject obj in GameObject.FindGameObjectsWithTag( "Robot" ) )
		{
			AIMover mover = obj.GetComponent<AIMover>();
			mover.OnTeamChanged.AddListener( OnTeamChanged );
			robots.Add( mover );
		}

        UpdateScoresUI();
    }

    void OnTeamChanged()
	{
		int last_team_id = 0;
		Dictionary<int, int> teams_count = new();

		foreach ( AIMover mover in robots )
		{
			last_team_id = mover.TeamID;

            if ( teams_count.TryGetValue( mover.TeamID, out int new_count ) )
				teams_count[mover.TeamID]++;
			else
				teams_count.Add( mover.TeamID, 0 );
		}

		if ( teams_count.Count == 1 )
		{
			switch ( last_team_id )
			{
				case 0:
					PlayerPrefs.SetInt( "BlueScore", PlayerPrefs.GetInt( "BlueScore", 0 ) + 1 );
					break;
				case 1:
					PlayerPrefs.SetInt( "RedScore", PlayerPrefs.GetInt( "RedScore", 0 ) + 1 );
					break;
            }

			UpdateScoresUI();
			print( ( last_team_id == 0 ? "Blue" : "Red" ) + " Team wins!!" );
		}
	}

	public void UpdateScoresUI()
	{
		blueScoreUI.SetScore( PlayerPrefs.GetInt( "BlueScore", 0 ) );
		redScoreUI.SetScore( PlayerPrefs.GetInt( "RedScore", 0 ) );
    }
}
