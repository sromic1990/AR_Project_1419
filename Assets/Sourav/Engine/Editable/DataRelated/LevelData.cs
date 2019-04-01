using System;
using System.Collections.Generic;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.DataRelated
{
	//Serialize this class and store in the disk for savegames
	public class LevelData : Data
	{
		[SerializeField] private SaveGame game;

		public int Coins
		{
			get { return game.coins; }
			set
			{
				game.coins = value;
				DataChanged();
			}
		}
		
		public bool IsTutorialOver
		{
			get { return game.isTutorialOver; }
			set
			{
				game.isTutorialOver = value; 
				DataChanged();
			}
		}

		public bool AdsInactive
		{
			get { return game.adsInactive; }
			set
			{
				game.adsInactive = value;
				DataChanged();
			}
		}

		public bool IsMusicOn
		{
			get { return game.isMusicOn; }
			set
			{
				game.isMusicOn = value;
				DataChanged();
			}
		}

		public bool IsSFXOn
		{
			get { return game.isSfxOn; }
			set
			{
				game.isSfxOn = value;
				DataChanged();
			}
		}

		public bool isAdOn
		{
			get { return game.isAdOn; }
			set
			{
				game.isAdOn = value;
				DataChanged();
			}
		}

		public DateTime dateOfNextReward
		{
			get { return game.dateOfNextReward; }
			set
			{
				game.dateOfNextReward = value;
				DataChanged();
			}
		}

		public int AdLastLevel
		{
			get { return game.lastAdLevel; }
			set
			{
				game.lastAdLevel = value;
				DataChanged();
			}
		}
		public bool isDataChanged;

	
		public List<int> downloadedModelIDs;
		public List<GameObject> listOfDownloadedModels;
		public string BASE_URL;
		public RootObject root;
		public int selectionButtonPressed;

		private void DataChanged()
		{
			isDataChanged = true;
			App.GetNotificationCenter().Notify(Notification.DataChanged);
			#if UNITY_EDITOR
			App.GetNotificationCenter().Notify(Notification.SaveData);
			#endif
		}

		public SaveGame GetCurrentData()
		{
			return game;
		}

		public void LoadData(SaveGame game)
		{
			this.game = game;
		}

		public void SetDefault()
		{
		}
	}

	[System.Serializable]
	public class SaveGame
	{
		public bool isTutorialOver;

		public int coins;

		public bool adsInactive;

		public bool isMusicOn;
		public bool isSfxOn;

		public bool isAdOn;

		public DateTime dateOfNextReward;

		public int lastAdLevel;
	}
	
	[System.Serializable]
	public class RootObject
	{
		public List<Datum> data;
	}

	[System.Serializable]
	public class Datum
	{
		public int id;
		public string model;
		public string model_checksum;
		public string uv_map;
		public string uv_map_checksum;
		public string diffused_uv_map;
		public string diffused_uv_map_checksum;
	}
}
