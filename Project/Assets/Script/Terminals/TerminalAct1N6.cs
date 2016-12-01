 using UnityEngine;
using Devlusion.PauseMenu;
namespace Devlusion.PauseMenu.Console{
	public class TerminalAct1N6 : PauseMenuConsole {
		private int lesson;
		private string[] txt;
		private string journalText;
		private string varName = "";
		private string values = "";
		private bool   varIsDeclared;
		private bool[] way;
		
		void Start (){
			way = new bool[4];
			txt = new string[10];
			if (Vocabulary.language){ // pt
				txt[0] = "Ivy: Temos aqui uma estrutura de decisão encadeada.$4Ivy: Note que se o primeiro if for falso, ele irá pular direto para o final do código.$4Ivy: Caso este seja verdadeiro, ele irá entrar em outra estrutura de decisão e após irá retornar o print correspondente.$4Ivy: Crie uma variável de nome x.$4Ivy: Após criar a variável x, você só precisar atribuir um valor para ela com o valor necessário para abrir a porta, seguindo a ordem correta.\n $4if(x >= 1800){$4   if(x < 2300){$4      print(\"Primeiro código!\");$4   } else if(x < 4800){$4      print(\"Terceiro código!\");$4   } else {$4      print(\"Quarto código!\");$4   }$4} else {$4      print(\"Segundo código!\");$4}";
				txt[1] = "Ivy: Tente novamente.$4if(x >= 1800){$4  if(x < 2300){$4	 print(\"Primeiro código!\");$4  } else if(x < 4800){$4	 print(\"Terceiro código!\");$4  } else {$4	 print(\"Quarto código!\");$4  }$4} else {$4  print(\"Segundo código!\");$4}";
				txt[2] = "   --Sistema offline--";
				txt[5] = "Primeiro código!";
				txt[6] = "Terceiro código!";
				txt[7] = "Quarto código!";
				txt[8] = "Segundo código!";
				txt[9] = "variável já declarada.";
				journalText = "Uma estrutura if/else pode ser colocada dentro de outra, está pratica é conhecida como encadeamento.\n Exemplo:\n if (x < 4){\n   if (x> 3){\n      // faça algo.\n   }\n }";				
			} else {
				txt[0] = "Ivy: Here we have a chain of decision-making structure.$4Ivy:Note that if the first 'if' is false then it will jump straight to the end of the code.$4Ivy: But if the line is true, it will go into another if/else structure and return the corresponding print command.$4Ivy: Declare a variable called x.\n Ivy: After creating the variable x, you only have to assign a value to it with the correct value to get into the if/else structure in the order required to open the door.$4if(x >= 1800){$4   if(x < 2300){$4      print(\"First code!\");$4   } else if(x < 4800){$4      print(\"Third code!\");$4   } else {$4      print(\"Fourth code!\");$4   }$4} else {$4      print(\"Second code!\");$4}";
				txt[1] = "Ivy: Please try again.$4if(x> = 1800)$4  if(x <2300) {$4	 print(\"First code!\");$4  } else if (x <4800) {$4 print(\"Third code!\");$4  } else {$4	 print (\"Fourth code!\");$4  }$4} else {$4  print(\"Second code!\");$4}";
				txt[2] = "   --System offline--";
				txt[5] = "First code!";
				txt[6] = "Third code!";
				txt[7] = "Fourth code!";
				txt[8] = "Second code!";
				txt[9] = "variable already declared.";
				journalText = "An if/else structure can be placed inside another, this practice is known as chaining.\n Example:\n if (x < 4){\n   if (x> 3){\n      // make something.\n   }\n }";
			}
			onStart ();
		}

		public override void initialize(){
			base.initialize();

			if (!PauseMenuInventory.instance.searchItem (5)){
				consoleLog = txt[2];
				return;
			}

			if (onTerminal && PauseMenuInventory.isPaused && lesson == 0 ) {
				setLogText (txt[0]);
				lesson++;
			}
		}

		public override void OnGUI(){
			if (PauseMenu.Console.PauseMenuConsole.consoleID == currentID)
				base.OnGUI();
		}

		private void tryAgain(){
			for (int i = 0; i < way.Length; i++)
				way [i] = false;
			setLogText (txt[1]);
		}

		public override void promptInterpreter(){
			if (lesson == 2)
				return;

			// Valores que devem ser atualizados a cada tentativa
			if (Vocabulary.language){ // pt
				txt[3] = "você não pode alterar o nome de sua variável.\nNome: (" + varName +").";
				txt[4] = "você não pode reusar um valor.\nvalores: (" + values +").";
			} else {
				txt[3] = "you can not change the name of your variable.\nName: (" + varName +").";
				txt[4] = "you can not reuse a value.\nvalues: (" + values +").";
			}

			string consoleBackup = console;		// Para imprimir no promptEnter	
			int? value = getArgumentInt ("=");

			if (value == null){
				console = consoleBackup;
				printPromptError(1);
				return;
			}
				
			console = console.Replace (value.ToString(), "").Replace("=", ""); // Remove o valor do texto

			if (!varIsDeclared){
				if (!varIsValid ("int", " ")){ 									// Verifica se é uma var valida
					return;
				}
			} else{
				if (consoleBackup.Trim().Contains("int ")){ 					// Verifica se é uma var valida
					console = consoleBackup;
					promptEnter();
					consoleLog += txt[9];
					return;
				}
			}
			
			if (varName == "")													// Recebe o nome da var, se ela não existir
				varName = getPromptArgument (" ").Trim();
			
			if (varIsDeclared)
				console = "int " + console;
			
			if (getPromptArgument (" ").Trim() != varName) {					// Retorna se o nome for diferente do declarado
				console = consoleBackup;
				promptEnter();
				consoleLog += txt[3];
				return;
			}

			if (values.Contains(value.ToString())){								// Verifica se o valor já foi usado
				console = consoleBackup;
				promptEnter();
				consoleLog +=  txt[4];
				return;
			}
			varIsDeclared = true;
			values += " " + value;
            console = consoleBackup;

            if (value >= 1800){
				if(value < 2300){
					way [0] = true;
					promptEnter ();
					consoleLog += txt[5];
				} else if(value < 4800){
					way [1] = true;
					promptEnter ();
					consoleLog += txt[6];

					if (!way [3]) {
						tryAgain ();
						return;
					}
				} else {
					way [2] = true;
					promptEnter ();
					consoleLog += txt[7];

					if (!way [1]) {
						tryAgain ();
						return;
					}
				}
			} else {
				way [3] = true;
				promptEnter ();
				consoleLog += txt[8];

				if (!way [0]) {
					tryAgain ();
					return;
				}
			}

			for (int i = 0; i < way.Length; i++)
				if (!way[i])
					return;

            console = "";
            sucess.Play();
            PauseMenuInventory.instance.delItem (5);
			PauseMenuInventory.instance.addItem (3);
			PauseMenuInventory.instance.itemJournal += journalText;
            lesson++;
            turnOff ();
		}
	}
}