using System.Collections;
using System.Collections.Generic;
using Sourav.Utilities.Extensions;
using UnityEngine;

public class Test : MonoBehaviour
{

	[SerializeField] private GameObject buttonsPallete;

	[SerializeField] private GameObject noodles;
	[SerializeField] private GameObject watermelon;

	public void OnObjectPlaced()
	{
		Debug.Log("Object Placed");
	}

	public void OnButtonClicked(int i)
	{
		buttonsPallete.gameObject.Hide();
		if (i % 2 == 0)
		{
			noodles.gameObject.Hide();
			watermelon.gameObject.Show();
		}
		else
		{
			watermelon.gameObject.Hide();
			noodles.gameObject.Show();
		}
	}
}
