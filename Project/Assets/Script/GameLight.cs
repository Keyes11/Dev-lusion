using UnityEngine;
using System.Collections;

namespace Devlusion{
	public class GameLight : MonoBehaviour {

		private float velocity = 2;
		private float time = 0;
		private bool status;
		public bool isFlashing;
		public bool isDisable;
		public Light objLight;


		void Start (){
			status = false;
		}

		void Update (){
			if (isDisable) {
				changeStatus (false);
				return;
			}

			if (isFlashing) {
				flashing ();
				return;
			}

			changeStatus (true);
		}

		private void changeStatus (bool status){
			objLight.enabled = status;
		}

		private void flashing (){
			time += Time.deltaTime;

			if (velocity <= time) {
				status = !status;
				changeStatus (status);
				time = 0;
			}
		}
	}
}