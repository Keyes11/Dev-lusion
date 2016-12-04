using UnityEngine;
using Devlusion.PauseMenu;
namespace Devlusion.PauseMenu.Console{
	public class TerminalAct1N8 : PauseMenuConsole {
		private int lesson;
		private string[] txt;
		private bool intLesson;
		private bool strLesson;
		public Teleporter teleporter;

		void Start (){
			txt = new string[3];
			if (Vocabulary.language){ // pt
				txt[0] = "   --Sistema offline--\n ";
				txt[1] = "   --Sistema funcionando a 52% de sua capacidade--\n    --Transporte ao próximo ponto de energia ativo--";
				txt[2] = "   --Sistema funcionando a 100% de sua capacidade--\n    --Teletransporte ativo--\n    --Selecione local de aterrissagem--$4int local = 0;$4switch(local){$4   case 1:$4      teleportTo(local.labA);$4      break;$4   case 2:$4      teleportTo(local.labF);$4      break;$4   case 3:$4      teleportTo(local.SistemaPrincipal);$4      break;$4   default:$4      teleportTo(local.Nenhum);$4      break;$4}$4Ivy: De um valor para \"local\".";
			}else {
				txt[0] = "   --System offline--";
				txt[1] = "   --System operating at 52% of its capacity--\n    --Teleportation to the next energy point activated--";
				txt[2] = "   --System operating at 100% of its capacity--\n    --Teleport activated--\n    --Select a landing spot--$4int local = 0;$4switch(local){$4   case 1:$4      teleportTo(local.labA);$4      break;$4   case 2:$4      teleportTo(local.labF);$4      break;$4   case 3:$4      teleportTo(local.SistemaPrincipal);$4      break;$4   default:$4      teleportTo(local.None);$4      break;$4}$4Ivy: Assing a value for \"local\".";
			}
			onStart ();
		}

		public override void initialize(){
			base.initialize();


			if (!PauseMenuInventory.instance.searchItem (6)){ // se o usuário utilizar o terminal 0008 antes de terminar o que tem que fazer no 7
				consoleLog = txt[0];
				return;
			}

			if (!PauseMenuInventory.instance.searchItem (7)){ //se o usuário utilizar o terminal de terminar o que tem que fazer no 9
                consoleLog = txt[1];
				return;
			}

			if (onTerminal && PauseMenuInventory.isPaused && lesson == 0) {
				setLogText (txt[2]);
				lesson++;
			}
		}

		public override void OnGUI(){
            if (PauseMenu.Console.PauseMenuConsole.consoleID == currentID)
				base.OnGUI();
		}
			
		public override void promptInterpreter(){
			if (!keywordIsValid ("local", true)) {
				return;
			}

			int n = 0;
			switch (getArgumentInt ("=")){
				case 1:
					n = 1;
					break;
				case 2:
					n = 2;
					break;
				case 3:
					n = 3;
					break;
				default:
					promptEnter ();
					return;
			}
			promptEnter ();
			sucess.Play ();
			teleporter.indexToTeleport = n;
			LevelController.canPassLevel = true;
			turnOff ();
			obj.sprite = spriteOn; // pois ele desliga mas continua ativo
		}
	}
}