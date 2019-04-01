using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ARProject.Scripts.View
{
	public class SelectionButton : GameElement, IPointerClickHandler
	{
		private int buttonId;

		[SerializeField] private Text buttonText;

		public void SetUpButton(int count)
		{
			buttonId = count;
			buttonText.text = count.ToString();
		}
		
		public void OnPointerClick(PointerEventData eventData)
		{
			NotificationParam selection = new NotificationParam(Mode.intData);
			selection.intData.Add(buttonId);
			App.GetNotificationCenter().Notify(Notification.SelectionButtonPressed, selection);
		}
	}
}
