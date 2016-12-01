using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/*
	Responsavel por retornar da scena de game over
*/
namespace Devlusion{
	public class GameOver : MonoBehaviour {

		void Update(){
			if (Input.anyKey)													// se qualquer tecla for apertada e o usuario ter perdido
				SceneManager.LoadScene("Menu",LoadSceneMode.Single);			// retorna ao menu
		}
		
		/*static IEnumerator call(string name){
			print("!");
			float timeOfFade = GameObject.Find("SYSTEM").GetComponent<Fade>().callFade(1);
			yield return new WaitForSeconds(timeOfFade);
			SceneManager.LoadScene(name,LoadSceneMode.Single);					// vai para a cena solicitada
		}*/
	}
}