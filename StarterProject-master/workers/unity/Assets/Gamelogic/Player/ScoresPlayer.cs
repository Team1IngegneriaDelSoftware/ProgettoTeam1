using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class ScoresPlayer : MonoBehaviour {


	[Require] private Scores.Writer ScoresWriter;


	public void addScores(float scores){
		var oldScores = ScoresWriter.Data.scores;
		var update = new Scores.Update ();
		update.SetScores (scores + oldScores);
		ScoresWriter.Send (update);
	}

	public void reset(){
		var update = new Scores.Update ();
		update.SetScores (0f);
		ScoresWriter.Send (update);
	}

}