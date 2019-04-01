using System.Collections;
using System.IO;
using System.Text;
using Dummiesman;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace ARProject.Scripts
{	
	public class LoadObjectAtRuntime : GameElement
	{
		[SerializeField] private Transform parent;
		[SerializeField] private string url;
		[SerializeField] private string mtlUrl;

		[SerializeField] private string nameOfModel;
		[SerializeField] private string nameOfMTL;
		
		//FIXME Move it to UI
		[SerializeField] private GameObject LoadingPanel;
		[SerializeField] private GameObject LoadButton;

		private void Start()
		{
			if (File.Exists(Application.persistentDataPath + "/" + nameOfModel) &&
			    File.Exists(Application.persistentDataPath + "/" + nameOfMTL))
			{
				LoadButton.gameObject.Show();
			}
			else
			{
				LoadButton.gameObject.Hide();
			}
		}
		
		[Sirenix.OdinInspector.Button()]
		public void StartLoding()
		{
			ShowModel();
		}

		public void StartLoadingFromURL()
		{
			LoadingPanel.gameObject.Show();
			StartCoroutine(ShowModelFromURL());
		}
		
		private void ShowModel () 
		{
			string obj = Application.persistentDataPath + "/" + nameOfModel; 
			string mtl = Application.persistentDataPath + "/" + nameOfMTL; 

			Debug.Log("mtl = "+mtl);
			
			GameObject loadedObj = new OBJLoader().Load(obj,mtl);
//			GameObject loadedObj = new OBJLoader().Load(obj);
			SetUpLoadedObject(loadedObj);
		}

		private IEnumerator ShowModelFromURL()
		{
			//make www
//			var www = new WWW("https://people.sc.fsu.edu/~jburkardt/data/obj/lamp.obj");
			var mtlwww = new WWW(mtlUrl);
			while (!mtlwww.isDone)
			{
				Debug.Log("MTL is being downloaded");
				yield return null;
			}
			
			var www = new WWW(url);
			while (!www.isDone)
			{
				Debug.Log("Model is being downloaded");
				yield return null;
			}
			
			string downloadPath = Application.persistentDataPath + "/"+nameOfModel;
			System.IO.File.WriteAllBytes(downloadPath, www.bytes);
			
			downloadPath = Application.persistentDataPath + "/"+nameOfMTL;
			System.IO.File.WriteAllBytes(downloadPath, www.bytes);

			//create stream and load
			var textStream_Obj = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
			var textStream_Material = new MemoryStream(Encoding.UTF8.GetBytes(mtlwww.text));
			
			GameObject loadedObj = new OBJLoader().Load(textStream_Obj, textStream_Material);
//			GameObject loadedObj = new OBJLoader().Load(textStream_Obj);
			SetUpLoadedObject(loadedObj);
			
			
			LoadingPanel.gameObject.Hide();

		}
		
		private void SetUpLoadedObject(GameObject loadedObj)
		{
			loadedObj.transform.parent = parent;
			loadedObj.transform.localScale = Vector3.one;
			loadedObj.transform.localPosition = Vector3.zero;
		}

	}
}
