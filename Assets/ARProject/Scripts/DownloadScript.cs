using System.Collections;
using UnityEngine;

namespace ARProject.Scripts
{
	public class DownloadScript : MonoBehaviour {

		// Use this for initialization
		void Start () {
			StartCoroutine(DownloadModels());
		}
	
		IEnumerator DownloadModels() {
			WWW www = new WWW("http://webappfactory.co/checkplate/public/uploads/restaurants/modelZip/renameUnZip/2019-03-12%2014:45:04_18_iphone.dae");
			yield return www;

			AssetBundle assetBundle = www.assetBundle;
			GameObject g = Instantiate(assetBundle.LoadAsset("New model")) as GameObject;

		}
	}
}
