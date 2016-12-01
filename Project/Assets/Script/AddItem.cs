using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Devlusion.PauseMenu;
/*
	Responsavel por adicionar um 
	item no inventario
	
	Colocar em um objeto Item
*/
namespace Devlusion{
	public class AddItem : MonoBehaviour {

		private string[] txt;
		public int item;
		public AudioSource source;

		// Deleta o objeto se ja existir o item, ja que no load game eles voltam a aparecer
		void Start(){
			txt = new string[3];
			if (Vocabulary.language)
				txt [2] = "Jornal adicionado ao inventário.";
			else
				txt [2] = "Journal added to inventory.";
			

			if (PauseMenuInventory.instance.searchItem(item))		// Se o item estiver no inventario
				Destroy(gameObject);								// destrua o objeto
		}

		void OnCollisionEnter2D(Collision2D other){ 				// Quando o obj colidir com algo.
			if (!PauseMenuInventory.instance.searchItem(item)){	// Se o item não estiver no inventario,
				PauseMenuInventory.instance.addItem(item); 			// então o coloque
				source.Play();										// execute o som e;
				itemInteract(item);
				Destroy(gameObject);								// destrua o objeto
			}
		}

		private void itemInteract(int item){
			if (item == 2) { // jornal
				PauseMenuInventory.instance.showMessageLabel (txt[2]);
				PauseMenuBase.pauseGame ();
			}
		}
	}
}