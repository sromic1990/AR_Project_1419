using System.Collections;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace ARProject.Scripts.Controllers
{
	public class ApiScript : Controller 
	{
		

		// Start is called before the first frame update
		private void StartWebserviceCall()
		{
			StartCoroutine(GenerateRequest());
		}

		IEnumerator GenerateRequest() 
		{
			WWW www = new WWW(App.GetLevelData().BASE_URL);
			while (!www.isDone)
				yield return null;
		
			if (string.IsNullOrEmpty (www.error)) 
			{
				Debug.Log (www.text);
				string jsonResponse = www.text;
				
				NotificationParam response = new NotificationParam(Mode.stringData);
				response.stringData.Add(jsonResponse);
				App.GetNotificationCenter().Notify(Notification.WebserviceSuccess, response);

			}
			else
			{
				Debug.Log (www.error);
				App.GetNotificationCenter().Notify(Notification.WebserviceFailure);
			}
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.StartWebservice:
					StartWebserviceCall();
					break;
			}
		}
	}
}
