using System.Collections.Generic;
using System.IO;
using System.Text;
using Dummiesman;
using Lean.Touch;
using Sirenix.OdinInspector;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

namespace ARProject.Scripts.Controllers
{
	public class MainController : Controller
	{

		[SerializeField] private GameObject loadedObject;
		[SerializeField] private Transform gameobjectParent;
		public ContentPositioningBehaviour content;
		[SerializeField] private GameObject loadingPanel;

		private void Start()
		{
			App.GetLevelData().listOfDownloadedModels = new List<GameObject>();
			App.GetLevelData().downloadedModelIDs = new List<int>();

		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.PlayButtonPressed:
					App.GetNotificationCenter().Notify(Notification.StartWebservice);
					break;

				case Notification.SelectionButtonPressed:
					hideAllModels();
					OnSelected(param.intData[0]);
					break;
				/*case Notification.ModelDownloaded:
					// load model
					LoadObjFromPath();
					ShowModel();
					break;*/
				case Notification.MaterialDownloaded:
					// load model
					LoadObjFromPath();
					ShowModel();
					break;

			}
		}

		private void ShowModel()
		{
			App.GetLevelData().listOfDownloadedModels[App.GetLevelData().listOfDownloadedModels.Count - 1].Show();
		}

		private void hideAllModels()
		{
			for (int i = 0; i < App.GetLevelData().listOfDownloadedModels.Count; i++)
			{
				App.GetLevelData().listOfDownloadedModels[i].Hide();
			}
		}

		private void OnSelected(int i)
		{
			Debug.Log("Button Pressed " + i);
			Debug.Log("obj link" + App.GetLevelData().root.data[i].model);
			App.GetLevelData().selectionButtonPressed = i;

			//Check whether model is downloaded or not, 
		if (App.GetLevelData().downloadedModelIDs.Contains(i))
			{
//				
				int index = -1;
				for (int j = 0; j < App.GetLevelData().downloadedModelIDs.Count; j++)
				{
					if (App.GetLevelData().downloadedModelIDs[j] == i)
					{
						index = j;
						break;
					}

				}

				App.GetLevelData().listOfDownloadedModels[index].Show();
			}
			else
			{
				// download
				// fire notification on download success
				// load obj from path
				// add it to the gameobject list in level data
				// add downloaded model id  in level data
				// show model
				loadingPanel.gameObject.Show();
				NotificationParam downloadData = new NotificationParam(Mode.stringData);
				downloadData.stringData.Add(App.GetLevelData().root.data[i].model);
				downloadData.stringData.Add(i.ToString());
				downloadData.stringData.Add(".obj");

				App.GetNotificationCenter().Notify(Notification.StartDownloadingModel, downloadData);

			}

//			Debug.Log();
			NotificationParam param = new NotificationParam(Mode.stringData);
			param.stringData.Add(App.GetLevelData().root.data[i].diffused_uv_map);
			param.stringData.Add(i.ToString());
			param.stringData.Add(".mtl");

			App.GetNotificationCenter().Notify(Notification.StartDownloadingMaterial, param);
		}

		private void LoadObjFromPath()
		{
			string objPath = Application.persistentDataPath + "/" + "Model/" +
			                 App.GetLevelData().selectionButtonPressed.ToString() + ".obj";
			string mtlPath = Application.persistentDataPath + "/" + "Material/" +
			                 App.GetLevelData().selectionButtonPressed.ToString() + ".mtl";
			//make www
			/*var www = new WWW("https://webappfactory.co/armodel/obj/cereal_bowl_A_OBJ_high.obj");
			while (!www.isDone)
			{
				System.Threading.Thread.Sleep(1);
			}
				
        
			//create stream and load
			var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
			var wwwMat = new WWW("https://webappfactory.co/armodel/obj/cereal_bowl_A_OBJ_high.mtl");
			while (!wwwMat.isDone)
				System.Threading.Thread.Sleep(1);
        
			//create stream and load
			var materialStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
			loadedObject = new OBJLoader().Load(textStream, materialStream);
			//loadedObject = new OBJLoader().Load(objPath, mtlPath);
			//loadedObject.transform.parent = gameobjectParent;

			App.GetLevelData().listOfDownloadedModels.Add(loadedObject);
			App.GetLevelData().downloadedModelIDs.Add(App.GetLevelData().selectionButtonPressed);
			
			//add gameobject to groundplane parent
			GameObject groundPlane = GameObject.Find("/View/Ground Plane Stage");
			Debug.Log(groundPlane.name);
			loadedObject.transform.SetParent(groundPlane.transform);
			// add lean touch script
			loadedObject.AddComponent<LeanScale>();
			loadedObject.AddComponent<LeanRotate>();

			/*var loadedObj = new OBJLoader().Load(objPath);
			GameObject obj = new OBJLoader().Load(objPath, mtlPath);
			obj.Show();
			
			
			 

			// content.AnchorStage = loadedObject.GetComponent<AnchorBehaviour>();
*/
			
			// Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
			//SceneManager.LoadScene("ar", LoadSceneMode.Additive);
			
			
			GameObject loadedObj = new OBJLoader().Load(objPath,mtlPath);
//			GameObject loadedObj = new OBJLoader().Load(obj);
			SetUpLoadedObject(loadedObj);
		}
		
		private void SetUpLoadedObject(GameObject loadedObj)
		{
			loadedObj.transform.parent = gameobjectParent;
			loadedObj.transform.localScale = Vector3.one;
			loadedObj.transform.localPosition = Vector3.zero;
			loadingPanel.gameObject.Hide();
		}
	}



}
