using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Stepper.Highscores;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreCanvas : MonoBehaviour
{
	private int points;

	[SerializeField] private Text _headerText;
	[SerializeField] private Text _highScoreText;
	[SerializeField] private Text _pointText;
	[SerializeField] private GameObject _submitForm;
	[SerializeField] private InputField _nameField;
	
	public void Init(int points)
	{
		this.points = points;
		_pointText.text = points + "dmg";
		
		// show 10 highscores.
		var highscores = HighscoreManager.GetHighscores();

		if (highscores != null)
		{
			for (int i = 0; i < highscores.Count; i++)
			{
				var highscore = highscores[i];
				_highScoreText.text += string.Format("#{0}: {1} \t - {2}dmg\n", 
					i + 1, highscore.Name, highscore.Points);
			}
		}
		
		// get rank, if its lower than 11, show the form!
		int rank = HighscoreManager.GetHighscoreRank(new Highscore(null, points), false);
		if (rank < 11)
		{
			_submitForm.SetActive(true);
			_headerText.text = "You're in!";
		}
		else
		{
			_submitForm.SetActive(false);
			_headerText.text = "Maybe another time!";
		}
	}

	public void Submit()
	{
		if (string.IsNullOrEmpty(_nameField.text)) return;

		int length = _nameField.text.Length < 13 ? _nameField.text.Length : 12;
		Highscore highscore = new Highscore(_nameField.text.Substring(0,length), points);

		HighscoreManager.GetHighscoreRank(highscore, true);
		Back();
	}

	public void Back()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
	}
}
