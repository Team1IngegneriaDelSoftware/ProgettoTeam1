using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class PlayerMover : MonoBehaviour {

	[Require] private Position.Writer PositionWriter;
	[Require] private Rotation.Writer RotationWriter;
	[Require] private PlayerInput.Reader PlayerInputReader;

	private Rigidbody rigidbody;

	void OnEnable ()
	{
		rigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		var velocity = SimulationSettings.PlayerAcceleration;
		var joystick = PlayerInputReader.Data.joystick;

		if (joystick.shift) {
			velocity = velocity * 3;
		}
		rigidbody.transform.Translate (0f, 0f, joystick.yAxis * Time.deltaTime * velocity);
		rigidbody.transform.Rotate(0, joystick.xAxis * 150 * Time.deltaTime, 0);


		var pos = rigidbody.position;
		var positionUpdate = new Position.Update()
			.SetCoords(new Coordinates(pos.x, pos.y, pos.z));
		PositionWriter.Send(positionUpdate);

		var rotationUpdate = new Rotation.Update()
			.SetRotation(rigidbody.rotation.ToNativeQuaternion());
		RotationWriter.Send(rotationUpdate);
	}
}
