using UnityEngine;
using System.Collections;
using Devlusion.PauseMenu;
/*
	Abre e fecha a porta
	position 1: abrir
			 2: fechar
*/
namespace Devlusion{
	public class Door : MonoBehaviour {

		public Animator 	 animator;
		public BoxCollider2D graphicDoor;
		
		public bool locked;
		public int 	keyItem;

		private void checkDoor (){
			if (locked)
				if (keyItem != 0 && PauseMenuInventory.instance.searchItem(keyItem)){
					locked = false;
					graphicDoor.isTrigger = false;
					PauseMenuInventory.instance.delItem(keyItem);
					openDoor();
				}	
		}

		private void openDoor(){
			animator.SetInteger ("position", 1);
			graphicDoor.isTrigger = true;
		}		
		
		private void closeDoor(){
			animator.SetInteger ("position", 2);
			graphicDoor.isTrigger = false;
		}
		
		void Start(){
			graphicDoor.isTrigger = !locked;
		}
		
		void OnTriggerEnter2D(Collider2D other) {
			checkDoor ();
			if (other.CompareTag ("Player") && !locked)			// vefirica se a se foi o player que colidiu
				openDoor ();
		}
		
		void OnTriggerStay2D(Collider2D other) {
			checkDoor ();
		}

		void OnTriggerExit2D(Collider2D other) {
			checkDoor ();
			if (other.CompareTag ("Player") && !locked)			// vefirica se foi o player que colidiu
				closeDoor();
		}
	}
}