using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace ARProject.Scripts.Controllers
{
	public class JSonClass : Controller
	{
		private void SetData(string jsonData)
		{
			Debug.Log("Data set");
			App.GetLevelData().root = JsonUtility.FromJson<RootObject>(jsonData);

			if (App.GetLevelData().root != null)
			{
				App.GetNotificationCenter().Notify(Notification.DataSuccessfullyParsed);
			}
			else
			{
				App.GetNotificationCenter().Notify(Notification.DataParsingFailure);
			}
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.WebserviceSuccess:
					SetData(param.stringData[0]);
					break;
			}
		}
	}
}


