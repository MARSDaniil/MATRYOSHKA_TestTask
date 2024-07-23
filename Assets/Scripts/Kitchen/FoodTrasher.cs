using System;

using UnityEngine;

using JetBrains.Annotations;

namespace CookingPrototype.Kitchen {
	[RequireComponent(typeof(FoodPlace))]
	public sealed class FoodTrasher : MonoBehaviour {

		FoodPlace _place = null;
		float     _timer = 0f;

		private int clickCount = 0;
		private float clickTime = 0;
		[SerializeField] private const float clickDelay = 0.5f;

		void Start() {
			_place = GetComponent<FoodPlace>();
			_timer = Time.realtimeSinceStartup;
		}

		/// <summary>
		/// Освобождает место по двойному тапу если еда на этом месте сгоревшая.
		/// </summary>
		[UsedImplicitly]
		public void TryTrashFood() {

			if(clickCount == 0 ) {
				clickCount++;
				clickTime = Time.time;
				return;
			}

			if(Time.time >= clickTime + clickDelay) {
				clickCount = 0;
				return;
			}


			if ( _place != null ) {
				if(!_place.IsFree && !_place.IsCooking ) {
					_place.FreePlace();
				}
			} else {
				throw new NotImplementedException("TryTrashFood: this feature is not implemented");
			}
		}
	}
}
