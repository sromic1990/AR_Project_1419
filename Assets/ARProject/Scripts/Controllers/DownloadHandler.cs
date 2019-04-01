using System.Collections;
using System.IO;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace ARProject.Scripts.Controllers
{
	public class DownloadHandler : Controller
	{

		[SerializeField] private string modelUrl;
		[SerializeField] private string mtlUrl;
		

		private void DownloadFromURL(string URL, string ID, string fileExtension, string folderName)
		{
			//Download and place code here
		
			//first link
			Debug.Log("Download model method");
		
			StartCoroutine(DownloadFromUrl(URL, ID, fileExtension, folderName));
		}

		private IEnumerator DownloadFromUrl(string url, string modelID, string fileExtension, string folderName)
		{
			Debug.Log("DownloadFromUrl started");
			Debug.Log("url = "+url);
			WWW www = new WWW(url);
			while (!www.isDone)
			{
				yield return null;
				Debug.Log("www is downloading");
				 Debug.Log(Application.persistentDataPath);
			}
		
			Debug.Log("www bytestream = "+www.text);
			string downloadPath = Application.persistentDataPath + "/"+folderName+"/"+modelID+fileExtension;
			System.IO.File.WriteAllBytes(downloadPath, www.bytes);
			if (folderName == "Model")
			{
				App.GetNotificationCenter().Notify(Notification.ModelDownloaded);
			}
			else
			{
				App.GetNotificationCenter().Notify(Notification.MaterialDownloaded);
			}
			
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			
			DirectoryInfo dirModel = new DirectoryInfo(Application.persistentDataPath + "/" + "Model"); 
			if(!dirModel.Exists){ Debug.Log ("Creating subdirectory model"); 
				dirModel.Create(); 
			}
			
			DirectoryInfo dirMat = new DirectoryInfo(Application.persistentDataPath + "/" + "Material"); 
			if(!dirMat.Exists){ Debug.Log ("Creating subdirectory material"); 
				dirMat.Create(); 
			}
			
			switch (notification)
			{
				case Notification.StartDownloadingModel:
					Debug.Log("Downloading model");
					DownloadFromURL(param.stringData[0], param.stringData[1], param.stringData[2], "Model");
				
					break;
				
				case Notification.StartDownloadingMaterial:
					Debug.Log("Downloading model");
					DownloadFromURL(param.stringData[0], param.stringData[1], param.stringData[2], "Material");
				
					break;
			}
		}
	}
}
