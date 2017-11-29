using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

[WorkerType(WorkerPlatform.UnityClient)]
public class PlayerInputSender : MonoBehaviour
{

	[Require] private PlayerInput.Writer PlayerInputWriter;

	void Update ()
	{
		var xAxis = Input.GetAxis("Horizontal");
		var yAxis = Input.GetAxis("Vertical");
		bool shift = Input.GetKey(KeyCode.LeftShift);
		bool w = Input.GetKey(KeyCode.W);
		bool s = Input.GetKey(KeyCode.S);
		bool space = Input.GetKey(KeyCode.Space);

		w = w || Input.GetKey (KeyCode.UpArrow);
		s = s || Input.GetKey (KeyCode.DownArrow);
		var update = new PlayerInput.Update();
		update.SetJoystick(new Joystick(xAxis, yAxis, shift, w, s, space));
		PlayerInputWriter.Send(update);
	}
}
