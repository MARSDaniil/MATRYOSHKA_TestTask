using CookingPrototype.Controllers;
using CookingPrototype.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class StartWindow : Window
{
	#region Fields

	[SerializeField] protected TMP_Text GoalText;
	[SerializeField] protected Button StartButton;

	#endregion

	#region Unity Events

	private void OnDestroy() {
		GameplayController.Instance.TotalOrdersServedChanged -= OnOrdersChanged;
	}

	#endregion

	#region Helpers

	protected override void Init() {
		if( StartButton ) {
			StartButton.onClick.AddListener(GameplayController.Instance.StartGame);
		} else {
			Debug.LogError($"Start button don't exist in {gameObject}");
		}
	}

	private void OnOrdersChanged() {
		if ( GoalText ) {
			GoalText.text = $"Цель на эту игру: {GameplayController.Instance.OrdersTarget}";
		}
	}

	#endregion


	#region Public

	public override void Show() {
		base.Show();

		GameplayController.Instance.TotalOrdersServedChanged += OnOrdersChanged;
	}


	public override void Hide() {
		base.Hide();
	}


	#endregion
}
