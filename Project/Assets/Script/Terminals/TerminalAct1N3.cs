using UnityEngine;
using Devlusion.PauseMenu;
namespace Devlusion.PauseMenu.Console{
	public class TerminalAct1N3 : PauseMenuConsole {
		private int lesson;
		private string[] txt;
		private string journalText;

		void Start (){
			txt = new string[8];
			if (Vocabulary.language){ // pt
				txt[0] = "Necessário keycard para iniciar.";
				txt[1] = "Ivy: Terminal do lab A ativo.$4Ivy: Compile um print para abrir esta porta exatamente como fez anteriormente, com seu nome de superUser entre as aspas.";
				txt[2] = "root@devlusion>print(\"Alice\"); >> Alice\n ...ERRO NO SISTEMA...\n ...Usuário não encontrado...$4Ivy: Identificação de rede em progresso . . .\n Uma nova rede foi encontrada.\n Esta porta está em uma outra conexão. Um simples print de seu superUser não será suficiente.$4Ivy: Novas variáveis encontradas.\n Mostrando variáveis:$4   string A = \"lab A\"; // Abrir porta do lab A$4   string B = \"lab B\"; // inoperante$4   string C = \"lab C\"; // 404$4   string D = \"lab D\"; // Aberto com keycard,\n terminal quebrado$4Ivy: Usaremos o comando print novamente, mas desta vez usaremos uma variável no lugar de seu nome de superUser.$4Ivy: Neste caso como estamos chamando uma variável, não utilizaremos aspas.$4Ivy: Digite o print como fez anteriormente e compile a porta correta para prosseguirmos.";
				txt[3] = "$4Laboratório A aberto!$4Operação manual necessária.\n Impossível abrir porta pelo terminal.\n Você precisa de um cartão-chave!$4Ivy: Prossiga para o laboratório C.\n Meu sistema de câmeras detecta que este é o único caminho disponível para meu sistema principal.";
				txt[4] = "Erro no sistema.";
				txt[5] = "Falha no sistema!";
				txt[7] = "Porta do labolatório A aberta!@5$6";
				journalText = "Uma variável é um espaço na memória reservado\n para um tipo de informação. Ela é declarada\n dizendo o tipo de valor e seu nome, não são\n aceitos caracteres especiais e ela não pode\n começar com um número ou letra maiúscula.\n Exemplo:\n    int variavel;\n ";
			}else {
				txt[0] = "Required keycard to start.";
				txt[1] = "Ivy: Lab A terminal online.$4Ivy: Compile a print statement to open this door exactly how you have done before with your superUser name between quotation marks.";
				txt[2] = "root@devlusion>print(\"Alice\"); >> Alice\n ...SYSTEM ERROR...\n ...User not found...$4Ivy: Network identification in progress . . .\n A new network has been found.\n This door is connected in another network.\n A simple print of your superUser name will not suffice.$4Ivy: New variable have been found.\n Showing variables:$4   string A = \"lab A\"; // open the door to lab A$4   string B = \"lab B\"; // Out of order$4   string C = \"lab C\"; // 404$4   string D = \"lab D\"; // Open with a keycard, terminal is broken$4We will use the print statement again but this time we will use a variable in place of your superUser name.$4Ivy: Since we are using a variable, quotation marks will not be necessary.$4Ivy: Type the print statement with the variable you want between its parentheses and compile the code to open the door.$4Ivy: We will use the print command again, but this time we will use a variable instead of  its superUser name.$4Ivy: In this case as we are calling a variable, we will not use quotation marks.$4Ivy: Enter the print as you did previously and compile the correct port to proceed.";
				txt[3] = "Laboratory A open!$4Manual operation required.\n Unable to open port through the terminal.\n You need a keycard!$4Ivy: Proceed to lab C.\n My camera system detects that this is the only available path for my main system.";
				txt[4] = "System error.";
				txt[5] = "System failure!";
				txt[7] = "Lab A door opened!@5$6";
				journalText = "A variable is a space in memory reserved for a\n type of information. It is declared by saying the\n value type and its name, no special characters are\n accepted and it can not begin with a capital letter or\n number.\n Example:\n    int variable;\n ";
			}
			txt[6] = "73797374656d206572726f7221";
			onStart ();
		}

		public override void initialize(){
			base.initialize();

			if (!PauseMenuInventory.instance.searchItem (4)){
				consoleLog = txt[0];
				return;
			}

			if (onTerminal && PauseMenuInventory.isPaused && lesson == 0 ) {
				consoleLog = "";
				setLogText (txt[1]);
				lesson++;
			}
		}
		
		public override void OnGUI(){
			if (PauseMenu.Console.PauseMenuConsole.consoleID == currentID)
				base.OnGUI();
		}

		public override void promptInterpreter(){
			if (lesson == 3)
				return;
			
			if (lesson == 1) {
				if (keywordIsValid ("print(\"Alice\");")) {
                    promptEnter();
                    sucess.Play ();
					lesson++;
					setLogText (txt [2]);
					return;
				} else {
					printPromptError (1);
					return;
				}
			} else{
				if (keywordIsValid ("print(A);")) {
                    promptEnter();
                    PauseMenuInventory.instance.addItem (3);
					PauseMenuInventory.instance.delItem (4);
					PauseMenuInventory.instance.itemJournal += journalText;
					consoleLog = "";
					sucess.Play ();
					lesson++;
                    setLogText(txt[7]);
                    return;
				} else if (keywordIsValid ("print(B);")) {
					promptEnter ();
					consoleLog += txt [4];
					return;
				} else if (keywordIsValid ("print(C);")) {
					promptEnter ();
					consoleLog += txt [6];
					return;
				} else if (keywordIsValid ("print(D);")) {
					promptEnter ();
					setLogText (txt [3]);
					return;
				} else {
					printPromptError (1);
					return;
				}
			}
		}
	}
}