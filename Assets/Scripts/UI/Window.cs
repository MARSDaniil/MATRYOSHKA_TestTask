using CookingPrototype.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

namespace CookingPrototype.UI {
	public class Window : MonoBehaviour {

		protected bool _isInit = false;

		#region Helpers
		protected virtual void Init() {

		}

		#endregion

		#region Public
		public virtual void Show() {
			if ( !_isInit ) {
				Init();
			}
			gameObject.SetActive(true);

		}

		public virtual void Hide() {
			gameObject.SetActive(false);
		}

		#endregion
	}
}
