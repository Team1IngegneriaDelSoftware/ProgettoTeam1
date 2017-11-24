using Assets.Gamelogic;
using Assets.Gamelogic.EntityTemplates;
using Assets.Gamelogic.Player;
using Assets.Gamelogic.Utils;
using Assets.Gamelogic.Core;
using Improbable.Unity.Core;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenController : MonoBehaviour
{
	[SerializeField]
	private Button ConnectButton;

	[SerializeField]
	private GameObject NotReadyWarning;

	public void AttemptSpatialOsConnection()
	{
		DisableConnectionButton();
		AttemptConnection();
	}

	private void DisableConnectionButton()
	{
		ConnectButton.interactable = false;
	}

	private void AttemptConnection()
	{
		FindObjectOfType<Bootstrap>().ConnectToClient();
		StartCoroutine(TimerUtils.WaitAndPerform(SimulationSettings.ClientConnectionTimeoutSecs, ConnectionTimeout));
		NotReadyWarning.SetActive (false);
	}

	private void ConnectionTimeout()
	{
		if (SpatialOS.IsConnected)
		{
			SpatialOS.Disconnect();
		}

		ConnectButton.interactable = true;
		NotReadyWarning.SetActive (true);
	}
}