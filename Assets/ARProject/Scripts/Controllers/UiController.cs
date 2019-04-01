using ARProject.Scripts.View;
using Dummiesman;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace ARProject.Scripts.Controllers
{
	public class UiController : Controller
	{

		[SerializeField] private GameObject mainMenu;
		[SerializeField] private GameObject loadingScreen;
		[SerializeField] private GameObject selectionMenu;
		[SerializeField] private SelectionButtonInitiator buttonInitiator;
		
		
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.PlayButtonPressed:
					HideMainMenu();
					ShowLoadingScreen();
					break;
				
				case Notification.DataSuccessfullyParsed:
					HideLoadingScreen();
					buttonInitiator.CreateButtons();
					ShowSelectionScreen();
					break;
				
				case Notification.SelectionButtonPressed:
					HideButtonPanel();
					break;
			}
		}

	

		private void HideButtonPanel()
		{
			selectionMenu.Hide();
		}

		private void ShowSelectionScreen()
		{
			selectionMenu.Show();
		}

		private void HideMainMenu()
		{
			mainMenu.Hide();
		}

		private void HideLoadingScreen()
		{
			Debug.Log("Loading screen hidden");
		}

		private void ShowLoadingScreen()
		{
			Debug.Log("Show Loading Screen");
		}
		
	}
}
