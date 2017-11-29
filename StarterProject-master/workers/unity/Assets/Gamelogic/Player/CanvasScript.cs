using UnityEngine;
using UnityEngine.UI;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

[WorkerType(WorkerPlatform.UnityClient)]
public class CanvasScript : MonoBehaviour
{

	[Require] private Health.Reader HealthReader;
	[Require] private Scores.Reader ScoresReader;
	[Require] private Death.Reader DeathReader;
	[Require] private PlayerInput.Writer PlayerInputWriter;
	private Slider HealthBar;
	private Slider ScoreBar;
	private Text mortoText;
	private float time;

	void OnEnable(){
		time =  Time.time;
		HealthBar = GameObject.Find ("SliderHealth").GetComponent<Slider> ();
		ScoreBar = GameObject.Find ("SliderScore").GetComponent<Slider> ();
		mortoText = GameObject.Find ("MortoText").GetComponent<Text> ();
	}

	void Update (){
		var vita = HealthReader.Data.health;
		var punti = ScoresReader.Data.scores;

		if(DeathReader.Data.death){
			mortoText.text = "SEI MORTO :)";
			time = Time.time + 3;
		}

		if (time <= Time.time) {
			mortoText.text = "";
		}

		HealthBar.value = vita;
		Text[] TextBar = GameObject.Find ("SliderHealth").GetComponentsInChildren<Text> ();
		foreach (Text text in TextBar)
		    text.text = vita.ToString ();
		ScoreBar.value = punti;
		TextBar = GameObject.Find ("SliderScore").GetComponentsInChildren<Text> ();
		foreach (Text text in TextBar)
		    text.text = punti.ToString ();
	}


}