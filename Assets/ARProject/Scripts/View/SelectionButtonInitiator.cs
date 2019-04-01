using System.Collections.Generic;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace ARProject.Scripts.View
{
	public class SelectionButtonInitiator : GameElement
	{
		[SerializeField] private SelectionButton buttonPrefab;
		[SerializeField] private Transform buttonParent;
		[SerializeField] private float distance;
		[SerializeField] private Vector3 initialPos;

		[SerializeField] private List<SelectionButton> buttons;

		public void CreateButtons()
		{
			for (int i = 0; i < App.GetLevelData().root.data.Count; i++)
			{
				GameObject button = Instantiate(buttonPrefab.gameObject, buttonParent);
				button.transform.position = initialPos;
				button.GetComponent<SelectionButton>().SetUpButton(i);
				buttons.Add(button.GetComponent<SelectionButton>());
				initialPos.y += distance;
			}
		}
	}
}
