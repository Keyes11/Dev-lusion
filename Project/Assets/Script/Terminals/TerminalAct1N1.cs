using UnityEngine;
using Devlusion.PauseMenu;

namespace Devlusion.PauseMenu.Console{
	public class TerminalAct1N1 : PauseMenuConsole {		

		private int lesson;
		private string[] txt;
		private string journalText;

		public GameLight GLight;

		void Start (){
			txt = new string[4];

			//criar tela de loading map

			if (Vocabulary.language){ // pt
				txt[0] = "???: Olá, bem vinda ao Dev'lusion.$4Ivy: Sou Inteligencia Artificial V-9 Y7, ou Ivy,como os cientistas costumavam me chamar.$4Ivy: Eu a trouxe aqui pois preciso de suas para resolver um problema no meu sistema principal.$4...FALHA NO SISTEMA IMINENTE...@1\n Ivy: Eu a guiarei nos passos necessários.\n Digite new user; para criar um novo usuário:";
				txt[1] = "Ivy: Sou Inteligencia Artificial V-9 Y7, ou Ivy,\n Bem-vinda Alice à Dev'Lusion!\n Você é o usuário número\n 9,223,372,036,854,775,808$4ERRO: OVERFLOW.$4...@1 O sistema encontrou um erro e deve ser terminado ...@1\n ... Reiniciando o sistema @1.@1.@1.$1$4... Analisando @1.@1.@1.\n Nome: Alice -- Armazenado na memória.\n Idade: 14 -- Armazenado na memória.\n Altura: 1,50 -- Armazenado na memória.\n ID: 0000000000000000000$4Bem vindo(a) Alice à 0x446576276c7573696f6e !\n Você é o usuário número 0,000,000,000,000,000,000\n SUPERUSER Atribuido à Alice:\n Privilégios concedidos.$4SUPERUSER SHELL ...@1$4... UMA TAREFA NECESSITA DE ATENÇÃO ...@1@0$4Bem vinda SUPERUSER, você não entra no sistema a -2.147.483.648 dias.\n Falha no sistema principal de login.\n ...Acesso aos laboratórios negado...@1\n ...Identifique-se:_________...@2\n ...117 115 101 114 32 110 111 116 32 102 111\n 117 110 100 10...@1\n ...Error...\n ...Falha no sist7072696d617 27920737973...@1\n ...436f727...\n ...275707465642064...$4...617461...@1\n ...Desligando em 3, 2...@2$4Ivy: Você precisará usar a lógica de programação para entrar com seus dados.$4Ivy: Para abrir esta porta um comando simples como um print deve resolver.$4Ivy: Print nada mais é que a saída de dados para o usuário. Tudo que estiver dentro do print será mostrado na tela para o usuário.\n Isto deve bastar para o terminal identificar seus privilégios de superuser.\n Alice, digite seu nome dentro do comando print.$4Não se esqueça de digitar entre aspas, você está digitando uma string!";
				txt[2] = "root@devlusion>print(\"Alice\"); >> Alice$4...Validando usuário, Aguarde @1. @1. @1.$4Usuário encontrado.\n Bem vindo, Alice!@5$6";
					//"Entre com o código da porta:" +
					//"\n $2\n Ivy: Você deverá informar o ID da porta para que ela abra." +
					//"\n No momento, o valor está null (nulo)." +
					//"\n Imprimirei na tela a variável responsável por abri-la," +
					//"\n altere seu valor com o valor desta porta (doorToBeOpened)." +
					//"\n Lembre-se que como você está digitando uma variável, ela não estará entre aspas!";
                txt[3] = "Keycard adicionado ao inventário.";
				journalText = "O método print() serve para imprimir um string no monitor, a sua sintaxe é:\n 	print(\"string\");\n Pode-se também colocar um variavel do tipo string:\n 	string var = \"texto\";\n 	print(var);\n ";
			}else {
				txt[0] = "???: Hello, welcome to Dev'lusion.$4Ivy: I am the artificial inteligence V-9 Y7, or Ivy, as scientists often call me.$4Ivy: I brought you here for I require your intellectual human capabilities to fix a problem in my core system$4...SYSTEM FAILURE IMMINENT...@1$4Ivy: I will guide you on the steps required.\n Type new user; to create a new user:";
				txt[1] = "Welcome Alice to Dev'Lusion!\n You are user number 9,223,372,036,854,775,808$4ERROR: OVERFLOW.$4...@1 The system found an error and must be terminated ...@1\n ... System Restarting @1.@1.@1.$1$4... Analyzing @1.@1.@1.\n Name: Alice -- Stored in memory.\n Age: 14 -- tored in memory.\n Height: 1,50 -- tored in memory.\n ID: 0000000000000000000$4Welcome Alice to 0x446576276c7573696f6e !\n You are user number\n 0,000,000,000,000,000,000\n SUPERUSER granted to Alice:\n Privileges bestowed.$4SUPERUSER SHELL ...@1$4... A TASK NEEDS ATTENTION ...@1@0$4Welcome SUPERUSER, you last login logged in to -2,147,483,648 days.\n Main login system failure.\n ...Access to laboratories denied...@1\n ...Identify:_________...@2\n ...117 115 101 114 32 110 111 116 32 102 111 117 110 100 10...@1\n ...Error...\n ...failure in syt7072696d617 27920737973...@1\n ...436f727...\n ...275707465642064...$4...617461...@1\n ...Shutting down in 3, 2...@2$4Ivy:  You will need to use programming logic to log in to the system.$4Ivy: The command print outputs a string argument, or a line of text, on the screen to the user. $4Ivy: print is nothing more than outputting data to the user. Everything inside the print will be shown on the screen to the user.\n It can show much more than a line of text but let's  keep it simple for now.\n That should be enough for the system to identify  your superUser privileges.\n Alice, type your name inside the command print.$4Do not forget to type between quotation marks, you are typing a string!!";
				txt[2] = "root@devlusion>print(\"Alice\"); >> Alice$4...Validating user, Please wait @1. @1. @1.$4User found.\n Welcome Alice!@5$6";
				txt[3] = "Keycard added to inventory.";
				journalText = "The method print() can be used to print a string to the monitor, and to call it you type:\n 	print(\"string\");\n You can also type in a variable of type string:\n 	string var = \"text\";\n 	print(var);\n ";
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
			if (PauseMenu.Console.PauseMenuConsole.consoleID == currentID) {
				base.OnGUI ();
			}
		}
		
		public override void promptInterpreter(){
			if (lesson == 3)
				return;

			if (lesson == 1){
				if (keywordIsValid ("new user", true)) { // por default, retorna o erro 1 se falso
					sucess.Play ();
					promptEnter ();
					setLogText (txt [1]);
					lesson++;
					return;
				}
			} else {
				if (keywordIsValid ("print(\"Alice\");")) {

                    promptEnter ();
					sucess.Play();
                    PauseMenuInventory.instance.addItem (3);	
					PauseMenuInventory.instance.itemJournal += journalText;
					lesson++;
                    GLight.isDisable = false;
                    PauseMenuInventory.instance.showMessageLabel(txt[2]);
                    setLogText(txt[2]);
					return;
				} else {
                    printPromptError (1);
                    return;
				}
			}
		}
	}
}