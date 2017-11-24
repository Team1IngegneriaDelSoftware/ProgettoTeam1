using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

[WorkerType(WorkerPlatform.UnityClient)]
public class AnimPlayer : MonoBehaviour
{
	[Require] private PlayerInput.Reader PlayerInputReader;
	[SerializeField] private Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
		anim.enabled = true;
	}


	void Update (){
		var joystick = PlayerInputReader.Data.joystick;

		if (joystick.w) {
			anim.SetBool ("walk", true);
		} else {
			anim.SetBool ("walk", false);
		}

		if (joystick.shift) {
			anim.SetBool ("run", true);
		} else {
			anim.SetBool ("run", false);
		}

		if (joystick.s) {
			anim.SetBool ("back", true);
		} else {
			anim.SetBool ("back", false);
		}

		if (joystick.space) {
			anim.SetBool ("attack1", true);
		} else {
			anim.SetBool ("attack1", false);
		}

	}
}
