using UnityEngine;
using System.Collections;
/*
	Responsavel pela base de tudo que envolva o menu pause
*/
namespace Devlusion.PauseMenu{
	public abstract class PauseMenuBase : MonoBehaviour {

		public static bool	isPaused  		   = false;								// verifica se o jogo esta pausado
		public static bool	onTerminal 		   = false;								// verifica se esta dentro do alcance do terminal
		public static bool	pauseButton 	   = true;								// verifica se o usuário pode apertar o botão Terminal para despausar
		public static bool  consoleIsEnabled   = true;								// define se o console esta editavel

		// Pausa/despausa
		public static void pauseGame(){
			isPaused = !isPaused;													// isPaused recebe a negação dela
			if (isPaused && !onTerminal)											// se isPaused faler true então e ele não estiver no terminal
				Time.timeScale = 0;													// faz o fps ir para 0
			else																	// se não,
				Time.timeScale = 1;													// volta para o normal.
		}

		// Faz o parse para os metodos de executar comandos
		public int strToInt(string value){
			int intValue;
			if (int.TryParse (value, out intValue))
				print ("successfully strToInt Parcing!");
			else
				intValue = 0;
			return intValue;
		}
	}
}