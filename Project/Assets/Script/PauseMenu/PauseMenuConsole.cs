using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

/*
	Responsavel pelo terminal
	
	Para a objs de terminal.

	Comandos do setConsoleText:
		$1 = limpa o console log
		$4 = quebra a linha dupla para quando ouver indentação (pois o \n não quebrará corretamente com os espaçamentos)
		$5 = limpa o console prompt
		$6 = despausa
		\n = quebra de linha simples
		@n = atrasa a velocidade do texto, sendo que n é o quanto ele irá aguardar quando isso for terminado
*/
namespace Devlusion.PauseMenu.Console{
	public class PauseMenuConsole : PauseMenuBase {

		// Responsavel por fazer que o terminal certo execute
		public static int consoleID; // não precisa de GameData, sera resetada em cada lv
		public int currentID;

		public GUISkin skin;
		public AudioSource sucess;

		public SpriteRenderer obj;
		public Sprite spriteOn;
		public Sprite spriteOff;
		
		// Responsavel pela interface
		public static string consoleLog = "";
		public static string console    = "";

        // Responsavel por escrever no console
        private float         velocity  = 0.1f;
		private float   	  time		= 0;
		public static int     index		= 0;
		public static string  text	    = "";
		public static bool	  textEnd  	= true;										// impede que o metodo fique tentando digitar no console

		protected void OnTriggerExit2D(Collider2D other) {
			if (other.CompareTag("Player")){										// vefirica se foi o player que colidiu
				onTerminal = false;													// onTerminal recebe false quando sair da area de contato
				consoleID = 0;
			}
		}

		public virtual void OnTriggerStay2D(Collider2D other) {
			if (other.CompareTag ("Player")) {										// vefirica se foi o player que colidiu
				onTerminal = true;													// onTerminal recebe true quando entra da area de contato
				consoleID = currentID;
			}
		}
	
		public virtual void OnGUI(){
			if (!onTerminal)														// não executa se estiver fora da area de contato
				return;

            float horiSize = Screen.height / 6;

            if (GUI.Button(new Rect(Screen.width - 60, 0, 60, horiSize / 2), Vocabulary.vocabulary[12]) && pauseButton) // cria o botão
                pauseGame();                                                        // pausa se o páuseButtonEnabled for true

            initialize();

			if (consoleIsEnabled)
				GUI.enabled = true;													// se isEnable for true o usuario poderá digitar
			else
				GUI.enabled = false;												// senão, o usuário não poderá

			// ------- botoes -------

			if (!isPaused)															// impede que o terminal apareça se o jogo estiver despausado
				return;

			if (GUI.Button (new Rect (Screen.width - 60, 1 + horiSize/2, 60, (horiSize / 2) - 1), Vocabulary.vocabulary [5]) && pauseButton) { // cria o botão de limpar
				console = "";
				consoleLog = "";
			}

			if (GUI.Button (new Rect (Screen.width - 60, 1 + (horiSize/2)*2, 60, (horiSize / 2) - 1), Vocabulary.vocabulary [4]) && pauseButton ) 										// cria o botão executar
				promptRead();														// tenta executa o comando do jogador

			// ------- Console -------
			GUI.skin = skin;
			GUI.color = new Color (0f,150f,0f,255f);

			GUI.TextField(new Rect(0,0,Screen.width-60,Screen.height-20), "");  // cria a mascara do console de notificações
            GUI.Label(new Rect(0, 0, Screen.width - 60, Screen.height - 20), consoleLog);   // mostra o texto do console de notificações

            GUI.SetNextControlName ("Field");
			console = GUI.TextField(new Rect(0,Screen.height-20,Screen.width-60,20), console);	// cria o console de comandos
		
			GUI.FocusControl ("Field");
		}

		// inicia o evento do terminal
		public virtual void initialize(){
			rollText ();
			if (onTerminal && isPaused && !textEnd) {
				pauseButton = false;												// impede que o jogo seja despausado em meio ao tutorial
				writeInConsole ();													// escreve o texto no console, tem de ser chamada dentro do update, pois os outros lopps tem outras instruções que obstruem a contagem de tempo
			} else
				pauseButton = true;													// permite que o jogador despause pelo botão
		}

		// Le os comandos do jogador
		public string getFirstLine(){
			int index = console.IndexOfAny(new char[] { '\r', '\n' });			// index recebe do final da primeira linha do console
			return index == -1 ? console : console.Substring(0, index); 		// recebe a primeira linha do console  ; retirado: consoleFirstLine =
		}
			
		// Liga console
		public void turnOn (){
			obj.sprite = spriteOn;
		}
		
		// Desliga console
		public void turnOff (){
            obj.sprite = spriteOff;
            isPaused = false;
            consoleIsEnabled = true;
        }
		
		// ------------------------ Impressor

		public void onStart(){
			time = Time.deltaTime;
		}

		// Escreve no console             						       //
		public virtual void writeInConsole(){
			time += Time.deltaTime;

			if (time <= velocity)													// verifica se todos os requisitos foram sastifeitos (os outros estão no if do Update())
				return;

			if (index < text.Length) {												// se o indice atual for menor que o tamanho do texto
				if (text [index].ToString () == "$")
					checkLogCode ();
				else if (text [index].ToString () == "@") {							// se ondice atual for § então
					int wait = strToInt(text [index].ToString ());					// converte o proximo indice para ínt
					time = wait - (wait*2);											// atribui a tme o valor de wait negativo
					index++;														// almenta o index para quando checar na proxima incrementação ele pular o numero que fica apos o §
				} else {
					consoleLog += text [index].ToString ();							// senão adiciona o caractere do indice atual no console
					time = 0;														// e time recebe 0
				}
				index++;															// index recebe mais 1
			} else{
				textEnd = true;														// impede que o jogo seja despausado pelo botão
				consoleIsEnabled = true;
			}
		}

		// Checa o qual a instrução que deve ser realizada no comando $
		private void checkLogCode(){
			if (text [index + 1].ToString () == "1") {
				consoleLog = "";
				text = text.Substring (index + 2, text.Length - index - 2);
				textEnd = false;
				
			} else if (text [index + 1].ToString () == "4")
				consoleLog += "\n\n";
			else if (text [index + 1].ToString () == "5")
				console = "";
			else if (text [index + 1].ToString () == "6") {
                turnOff();
			}
			
			index++;
		}

		// Ativa writeInConsole() e define um texto para ser escrito   //
		public static void setLogText(string txt){								// reinicia tudo para o texto aparecer no terminal
			consoleLog = "";
			index = 0;
			text = txt;
			textEnd = false;
			consoleIsEnabled = false;
		}

		public void rollText(){
			int contador = 0; 
			foreach (char c in consoleLog)
				if (c == '\n')
					++contador;
			
			int i;
			if (Application.platform == RuntimePlatform.WindowsEditor) i = 20;
			else i = 30;
				
			if (contador >= i)
				consoleLog = consoleLog.Substring (consoleLog.IndexOf ("\n") + 1, consoleLog.Length - consoleLog.IndexOf ("\n") - 2);  // console recebe o console sem a primeira linha 	//if (i > 1) não foi necessário
		}

		// ------------------------ Interpretador

		public void printPromptError (int indexInVocabulary){
			if (indexInVocabulary < Vocabulary.vocabulary.Length){
                rollText();
                consoleLog += "\nroot@devlusion>" + console + " >> " + Vocabulary.vocabulary [indexInVocabulary];
                console = "";
            }
		}
		
		// Verifica se o comando é executavel  						   //
		public virtual bool promptRead (){
			string conFirstLine = getFirstLine ().Trim ();
			
			if (conFirstLine == "" || conFirstLine == ";") // Se for vazio ou só tiver um ";"
				return false;
			
			if (conFirstLine [conFirstLine.Length - 1].ToString () == ";") {
				promptInterpreter();
				return true;
			}
			
			printPromptError(10);
			return false;
		}

		// Simula a entrada de dados no prompt.	   //
		public virtual void promptEnter(){
            rollText();
			consoleLog += "\nroot@devlusion>" + console + " >> ";
			console = "";
		}

		// Le os comandos do jogador, vazio para ser herdado.	   //
		public virtual void promptInterpreter (){ return; }

		// Metodos expecificos
		
		// Verifica a se existe a keyword
		public bool keywordIsValid (string key, bool printError = false){
			string consoleFirstLine = getFirstLine().Trim();

			if (consoleFirstLine.Contains (key.Trim()))
				return true;

            if (printError)
			    printPromptError (0);
			return false;
		}

		// Verifica se a palavra serve como variavel, não deve ter valor (ex: int x = 10)
		public bool varIsValid (string key, string separator){
			string varName = getPromptArgument (separator).Trim(); 		// Pega o nome definido pelo user

			if (varName == "" || varName == null)
				return false;
			
			string varType = console.Replace (varName, ""); 			// Pega o texto menos varName
			varType = varType.Substring (0, varType.Length - 2).Trim(); 
			
			string userInput = getFirstLine().Trim();					// Pega o texto na integra
			userInput = userInput.Substring (0, userInput.Length - 2).Trim(); 
				
			if (separator == " ") 										// Remove espacos extras, se este for o separador
				userInput.Replace("  ", " ");
				
			if (varName.Contains (" ")) { 								// Verifica se existe espaço
				printPromptError (24);
				return false;
			}
			if (userInput != varType+separator+varName){				// Verifica se o userInput é formado na ordem certa
				printPromptError (22);
				return false;
			}
			if (varType != key){										// Verifica se varType é igual a key
				printPromptError (21);
				return false;
			}
			if (Regex.IsMatch (varName[0].ToString(), "^[A-Z]+$")){  	// Impede a entrada de chars especiais e maiusculas
				printPromptError (25);
				return false;
			}
			if (Regex.IsMatch(varName [0].ToString (), "^[0-9]")){		// Verifica se a primeira caractere é um numero
				printPromptError (23);
				return false;
			}
			return true;
		}

		// Retorna o argumento apos a keyword
		public string getPromptArgument (string separator){
			string value = console.Trim ();
			int index = value.IndexOf (separator);
			int size = value.Length;
			return value.Substring (index + 1, size - index - 2); 					// recebe argumento
		}

		// Retornam os argumentos de cada tipo

		public bool? getArgumentBool (string separator){
			string strValue = getPromptArgument (separator).Trim();
			if (strValue.Equals ("true"))
				return true;
			else if (strValue.Equals ("false"))
				return false;
			return null;
		}

		public string getArgumentStr (string separator){
			string value = getPromptArgument (separator).Trim();
			int quote = 0;

			for (int i = 0; i < value.Length; i++) {

				// pega todas as aspa que achar
				if (value [i].ToString () == "\"")
					quote += 1;
			}

			// retorna se o numero de aspas for diferente de dois 
			if (quote != 2){
				printPromptError (6);
				return null;
			}

			// retorna o valor puro, sem aspas ou key
			if (value [0].ToString () == "\"" && value [value.Length - 1].ToString () == "\"")
				return (value.Substring (1, (value.Length - 1) - 1));

			printPromptError (20);
			return "";
		}
		
		public float? getArgumentFloat (string separator){
			float floatValue = 0f;

			if (float.TryParse (getPromptArgument (separator), out floatValue)) 							// tenta converter a string para um int
				return floatValue;													// executa o som de sucesso
	
			printPromptError (1);
			return 0f;
		}

		public int? getArgumentInt (string separator){
			string strValue = getPromptArgument (separator);						//recebe primeira linha sem os espacos no comeco e final e sem o ponto e virgula
			int intValue = 0; 														// recebe a posição do igual

			if (int.TryParse (strValue, out intValue)) 								// tenta converter a string para um int
				return intValue;													// executa o som de sucesso

			printPromptError (1);
			return null;
		}
	}
}