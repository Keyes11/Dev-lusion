using UnityEngine;
using Devlusion.PauseMenu;
namespace Devlusion.PauseMenu.Console{
	public class TerminalAct1N5 : PauseMenuConsole {
		private int lesson;
		private string[] txt;
		private string journalText;

        public GameLight[] lights;

		void Start (){
			GameObject[] objs = GameObject.FindGameObjectsWithTag("LabDPowerC");
			lights = new GameLight[objs.Length];

			for(int i = 0; i < objs.Length; i++)
				lights[i] = objs[i].GetComponent<GameLight> ();

			txt = new string[8];
			if (Vocabulary.language){ // pt
				txt[7] = "$4switch (valor){$4   case \"labA\":$4      mensagem = \"Ativar terminal do lab A.\";$4      break;$4   case \"labB\":$4      mensagem = \"Ativar terminal do lab B.\";$4      break;$4   case \"labC\":$4      mensagem = \"Ativar terminal do lab C.\";$4      break;$4   case \"labD\":$4      mensagem = \"Ativar terminal do lab D.\";$4      break;$4   default:$4      mensagem = \"Valor inválido.\";$4      break;$4}$4return mensagem;";
				txt[0] = "---Sistema operacionais---\n ---Laboratório D funcionando a 47% de sua capacidade---\n ---Transferência de energia necessária---$4Ivy: Este é um código switch-case. Ele é parecido com um if/else, porém não é possível nenhum tipo de$4operação aritmética dentro dele.$4Ivy: De acordo com o código a seguir, crie uma variável value com o valor correspondente ao terminal que você queira ativar.$4Ivy: Lembre-se que a variável entre aspas,\n Exemplo:\n   \"variavel\", é do tipo string.$4Ivy: Mostrando código switch-case @1. @1. @1." + txt[7] + "\n $4Ivy: default é quando a variável não tem um valor correspondente a nenhum dos casos do código switch-case.";
				txt[1] = "Ativar terminal do lab A.\n --Terminal 0003 já está ativado--";
				txt[2] = "Ativar terminal do lab B.\n --Ativando terminal 0006--\n --Transferindo energia para outro laboratório--\n --Energia transferida--\n --laboratório D funcionando a 0% de sua capacidade--\n --Desligando--@5$6";
				txt[3] = "Ativar terminal do lab C.\n --Terminal 0004 já está ativado--";
				txt[4] = "Ativar terminal do lab D.\n --Erro no sistema--\n --Terminal não encontrado--";
				txt[5] = "Valor inválido.\n --Tente novamente--";
				txt[6] = "string valor";
				journalText = "O switch serve para checar vários valores de\n uma mesma variavel, sendo que o default é\n equivalente ao else da estrutura if.\n Sem o break a próxima checagem será\n executad.\n switch(variavel){\n case 1:\n   print(\"o valor é 1\");\n   break;\n case:\n   print(\"o valor é 2\");\n   break;\n default:\n   print(\"valor fora do escopo\");\n   break;\n }";
			} else {
				txt[7] = "$4switch (value){$4   case \"labA\":$4      message = \"Activate terminal in lab A.\";$4      break;$4   case \"labB\":$4      message = \"Activate terminal in lab B.\";$4      break;$4   case \"labC\":$4      message = \"Activate terminal in lab C.\";$4      break;$4   case \"labD\":$4      message = \"Activate terminal in lab D.\";$4      break;$4   default:$4      message = \"Invalid value.\";$4      break;$4}$4return message;";
				txt[0] = "---Systems operational---\n ---Laboratory D working at 47% of its capacity---\n ---Energy transfer required---$4Ivy:This is a switch-case code. It is similar to an if/else statement, however it cannot have any mathematical operation.$4Ivy: As per the following code, declare a variable called value with its value equal to the terminal you want to activate.$4Ivy: Remember that a value to a variable between quotation marks is of type string.$4Example:\n   \"labA\".\n $4Ivy: Showing switch-case code @1. @1. @1." + txt[7] + "\n $4Ivy: default shows an output when the variable value$4does not correspond to any of the cases in the switch statement.";
				txt[1] = "Activate terminal in lab A.\n --Terminal 0003 is already activated--";
				txt[2] = "Activate terminal in lab B.\n --Activating terminal 0006--\n --Transfering power to another laboratory--\n --Power transferred--\n --Laboratory D working at 0% of its capacity--\n --Shutting down--@5$6";
				txt[3] = "Activate terminal in lab C.\n --Terminal 0004 is already activated--";
				txt[4] = "Activate terminal in lab D.\n --System error--\n --Terminal not found--";
				txt[5] = "Invalid value.\n --Try again--";
				txt[6] = "string value";
				journalText = "The switch serves to check several values of the\n same variable, the default being equivalent to the\n else of the if structure.\n Without the break the next check will be executed.\n switch (variable) {\n case 1:\n    print ( \"value is 1\");\n    break;\n case 2:\n    print ( \"value is 2\");\n    break;\n default:\n    print ( \"value out of scope\");\n    break;\n }";
			}
			onStart ();
		}

		public override void initialize(){
			base.initialize();

			if (onTerminal && PauseMenuInventory.isPaused && lesson == 0 ) {
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

			if (!keywordIsValid (txt [6], true)) {
				return;
			}
        
            switch (getArgumentStr ("=")){
				
			case "labA":
				promptEnter ();
				setLogText (" >> " + txt[1]);
			break;

			case "labB":
                promptEnter();
                sucess.Play();
				PauseMenuInventory.instance.addItem (5);
				PauseMenuInventory.instance.itemJournal += journalText;
				for (int i = 0; i < lights.Length; i++)
					lights[i].isDisable = true;
                lesson++;
                    setLogText(txt[2]);
                    break;

			case "labC":
				promptEnter ();
				setLogText (" >> " + txt[3] + txt[7]);
			break;

			case "labD":
				promptEnter ();
				setLogText (" >> " + txt[4] + txt[7]);
			break;

			default:
				promptEnter ();
				setLogText (" >> " + txt[5] + txt[7]);
			break;
			}
		}
	}
}