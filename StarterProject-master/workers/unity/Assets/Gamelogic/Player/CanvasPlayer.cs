using UnityEngine;
using UnityEngine.UI;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

[WorkerType(WorkerPlatform.UnityClient)]
public class CanvasPlayer : MonoBehaviour
{

	[Require] private Scores.Reader ScoresReader;
	public Slider ScoreBar;
	public Text scoreText; 

	void Update (){
		var punti = ScoresReader.Data.scores;
		ScoreBar.value = punti;
		scoreText.text = punti.ToString ();
	}
}