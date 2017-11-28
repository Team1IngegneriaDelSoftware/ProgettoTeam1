using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class HealthPlayer : MonoBehaviour {


	[Require] private Health.Writer HealthWriter;
	[Require] private Position.Writer PositionWriter;
	private Rigidbody rigidbody;
	private float time;

	void OnEnable ()
	{
		time =  Time.time;
		rigidbody = GetComponent<Rigidbody>();
	}

	void OnTriggerStay(Collider other){
		
		if (other != null && other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerMover>().space && time <= Time.time && other is CapsuleCollider)
		{
			float newHealth = HealthWriter.Data.health - 25f;
			time = Time.time + 1;

			if (newHealth <= 0) {
				var update = new Health.Update ();
				update.SetHealth (100f);
				HealthWriter.Send (update);
				other.gameObject.GetComponent<ScoresPlayer> ().addScores (25f);
				GetComponent<ScoresPlayer> ().reset ();
				rigidbody.MovePosition (new Vector3(0f,1f,0f));
				var pos = rigidbody.position;
				var positionUpdate = new Position.Update()
					.SetCoords(new Coordinates(pos.x, pos.y, pos.z));
				PositionWriter.Send(positionUpdate);

			} else {
				var update = new Health.Update();
				update.SetHealth(newHealth);
				HealthWriter.Send(update);
				other.gameObject.GetComponent<ScoresPlayer> ().addScores (25f);
			}

		}
		
	}
		
}




