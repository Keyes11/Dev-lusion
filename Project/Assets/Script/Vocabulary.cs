using UnityEngine;
using System.Collections;
/*
	Responsavel pelo vocabulario
	
	Colocar na camera
*/
namespace Devlusion{
	public class Vocabulary : MonoBehaviour {
		
		public  static string[] vocabulary;
		public  static bool     language;

		void Start(){
			loadData ();
			changeLanguage ();
		}

		private void loadData () {
			// isso é carregado quando o game iniciar e não quando for dado load game
			if (PlayerPrefs.HasKey ("vocabularyLanguage")) {
				if (PlayerPrefs.GetInt ("vocabularyLanguage") == 0)
					language = true;
				else
					language = false;
			}
		}

		public static void changeLanguage () {
			if (language) {														// Se language for true então o jogo esta em pt
				vocabulary = new string[] {
					"comando inválido!  ",										//0
					"valor inválido!",											//1
					"final de método inesperado, não achado ')'.",				//2
					"Voltar e salvar",											//3
					"Executar",													//4
					"Limpar",													//5
					"numero de aspas deve ser 2.",								//6
					"Pausar",													//7
					"Menu principal",											//8
					"Configurações",											//9
					"comando inválido, espera-se ';' no final da linha.",		//10
					"Ajuda",		//inutil?									//11
					"Terminal",													//12
					"Tela cheia: ",												//13
					"Resolução: ",												//14
					"Novo jogo",												//15
					"Carregar jogo",											//16
					"Português",												//17
					"Sair",														//18
					"English",													//19
					"a string deve estar dentro dos parenteses.",				//20
					"tipo de variável não esperado.",							//21
					"variável declarada incorretamente.",						//22
					"uma variável não deve começar com um número.",				//23
					"uma variável não deve conter espaços."	,					//24
					"uma variável não deve começar com uma letra maiúscula."	//25
				};
			}
			else {
				vocabulary = new string[]{										// senão esta em eng.
					"invalid command!  ",										//0
					"invalid value!",											//1
					"unexpected method end, not found ')'.",					//2
					"Back and save",											//3
					"Run",														//4
					"Clear",													//5
					"the number of quotes must be 2.",							//6
					"Pause",													//7
					"Main menu",												//8
					"Configurations",											//9
					"invalid command, ';' at the end of the line expected.",	//10
					"Help",														//11
					"Terminal",													//12
					"Fullscreen: ",												//13
					"Resolution: ", 											//14
					"New game",													//15
					"Load game",												//16
					"Português",												//17
					"Exit",														//18
					"English",													//19
					"The string must be inside of quotes.",						//20
					"variable type not expected.",								//21
					"variable declared incorrectly",							//22
					"a variable can not begin with a number.",					//23
					"a variable can not contains spaces.",						//24
					"a variable should not begin with an uppercase letter."		//25
				};
			}
		}
	}
}