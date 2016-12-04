
using UnityEngine;
using Devlusion.PauseMenu;
namespace Devlusion.PauseMenu.Console{
	public class TerminalAct1N9 : PauseMenuConsole {
		private int lesson;
		private string[] txt;
		private string journalText;
		private bool intLesson;
		private bool strLesson;

		public GameLight[] lights;

		void Start (){
			GameObject[] objs = GameObject.FindGameObjectsWithTag("LabEFHall");
			lights = new GameLight[objs.Length];
			txt = new string[2];
			for(int i = 0; i < objs.Length; i++)
				lights[i] = objs[i].GetComponent<GameLight> ();
			
			if (Vocabulary.language){ // pt
				txt[0] = "Ivi@devlusion> print(terminalTeleporter) >> false.$4Ivi: O terminal que controla o teleporte está offline pois não tem energia suficiente na sala para ele funcionar a completamente, estou configurando para ele direcionar para lá a energia dos laboratórios E e F, mas preciso da permissão de superUser.$4... Redirecionando energia @1.@1.@1.$4redirecionamento concluido!$4Ivi: Agora que há energia, faça o terminal ficar online atribuindo um novo valor para terminalTeleporter que no momento falso (false). Faça ele valer true (verdadeiro) atribuindo o valor true. O tipo bollean (bool) somente aceita valores true e false.";
				txt[1] = "booleans só aceitam true e false.";
				journalText = "Uma variável do tipo boolean (bool) só aceita dois valores, true (verdadeiro) e false (falso).\n Exemplo:\n bool variavel = false;\n if (variavel == false){\n   variavel = true;\n }";
			}else {
				txt[0] = "Ivi@devlusion> print(terminalTeleporter); >> false.$4Ivi: The terminal that controls the teleport is offline because it does not have enough power in the room for it to work completely,\n I'm setting it to direct the energy from the E and F labs there, but I need the superUser permission.$4\n ... Redirecting power @1.@1.@1.$4redirect completed!$4Ivi: Now that there is power, make the terminal come online by assigning a new value to terminalTeleporter that at the moment false.\n Make it true by assigning the value true. The bollean type (bool) only accepts true and false values.";
				txt[1] = "booleans only acept true and false.";
				journalText = "A boolean variable (bool) then accepts\n two values, true and false.\n Exemple:bool variable = false;\n if (variable == false){\n   variable = true;\n }";
			}
			onStart ();
		}

		public override void initialize(){
			base.initialize();

			if (onTerminal && PauseMenuInventory.isPaused && lesson == 0) {
				setLogText (txt[0]);
				lesson++;
			}
		}

		public override void OnGUI(){
			if (PauseMenu.Console.PauseMenuConsole.consoleID == currentID)
				base.OnGUI();
		}

		public override void promptInterpreter(){
			if (lesson == 2)
				return;

			if (!keywordIsValid ("terminalTeleporter", true)) {
				return;
			}

			bool? b = getArgumentBool ("=");

			if (b == true) {
                promptEnter();
                consoleLog += " >> true";
                sucess.Play ();
				PauseMenuInventory.instance.addItem (7);
				PauseMenuInventory.instance.itemJournal += journalText;
				for (int i = 0; i < lights.Length; i++)
					lights [i].isDisable = true;
                lesson++;
                turnOff ();
			} else if (b == false) {
				sucess.Play ();
				promptEnter ();
				consoleLog += " >> false";
			} else {
				promptEnter ();
				consoleLog += txt[1];
			}
		}
	}
}