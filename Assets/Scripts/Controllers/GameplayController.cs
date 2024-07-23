using System;

using UnityEngine;

using CookingPrototype.Kitchen;
using CookingPrototype.UI;

using JetBrains.Annotations;

namespace CookingPrototype.Controllers {
	public sealed class GameplayController : MonoBehaviour {
		public static GameplayController Instance { get; private set; }

		public GameObject TapBlock   = null;
		public WinWindow  WinWindow  = null;
		public LoseWindow LoseWindow = null;
		[SerializeField] private StartWindow StartWindow;
		[SerializeField] private GameObject TopOfPanel;
		int _ordersTarget = 0;

		public int OrdersTarget {
			get { return _ordersTarget; }
			set {
				_ordersTarget = value;
				TotalOrdersServedChanged?.Invoke();
			}
		}

		public int        TotalOrdersServed { get; private set; } = 0;

		public event Action TotalOrdersServedChanged;

		private bool isGameStarted;

		public bool IsGameStarted {
			get {
				return isGameStarted;
			}
		}

		void Awake() {
			if ( Instance != null ) {
				Debug.LogError("Another instance of GameplayController already exists");
			}
			Instance = this;
		}

		void OnDestroy() {
			if ( Instance == this ) {
				Instance = null;
			}
		}

		private void Start() {
			Init();
			HideWindows();
		}

		void Init() {
			TotalOrdersServed = 0;
			Time.timeScale = 1f;
			TotalOrdersServedChanged?.Invoke();
		}

		public void CheckGameFinish() {
			if ( CustomersController.Instance.IsComplete ) {
				EndGame(TotalOrdersServed >= OrdersTarget);
			}
		}

		public void StartGame() {
			isGameStarted = true;
			TapBlock?.SetActive(false);
			StartWindow?.Hide();
			TopOfPanel?.SetActive(true);
		}

		void EndGame(bool win) {
			Time.timeScale = 0f;
			TapBlock?.SetActive(true);
			isGameStarted = false;
			if ( win ) {
				WinWindow.Show();
			} else {
				LoseWindow.Show();
			}
		}

		void HideWindows() {
			TapBlock?.SetActive(true);
			StartWindow?.Show();
			WinWindow?.Hide();
			LoseWindow?.Hide();
			TopOfPanel?.SetActive(false);
		}

		[UsedImplicitly]
		public bool TryServeOrder(Order order) {
			if ( !CustomersController.Instance.ServeOrder(order) ) {
				return false;
			}

			TotalOrdersServed++;
			TotalOrdersServedChanged?.Invoke();
			CheckGameFinish();
			return true;
		}

		public void Restart() {
			Init();
			CustomersController.Instance.Init();
			HideWindows();

			foreach ( var place in FindObjectsOfType<AbstractFoodPlace>() ) {
				place.FreePlace();
			}
		}

		public void CloseGame() {
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
		}
	}
}
