using UnityEngine;
using Devlusion.PauseMenu;
namespace Devlusion.PauseMenu.Console{
	public class TerminalAct1N4 : PauseMenuConsole {
		private int lesson;
		private string[] txt;
		private string journalText;
		private bool intLesson;
		private bool strLesson;
		
		void Start (){
			txt = new string[3];
			if (Vocabulary.language){ // pt
				txt[0] = "Ivy: Esta porta está trancada com uma senha pessoal.\n Quebra do decreto número 481.516 artigo 23 parágrafo 42, senhas pessoais são terminantemente proibidas em ambientes públicos.$4Ivy: recuperação de senha em progresso . . .$4   a = (número de mesas no laboratório A - 7) * 100;$4   b = nome do cientista chefe do lab A;$4Recuperação de senha terminada.$4Ivy: Erro no sistema! senha não identificada!\n Ser humano necessário para identificar senha!$4Ivy: Meus sistemas indicam que você é a única pessoa capaz de identificar esta senha.\n Quando conseguir, crie duas variáveis A e B com seu valor equivalente para abrir a porta.$4Ivy: Lembre-se que ao declarar uma variável com texto esta deve ser do tipo string, mas se ela for numérica deve ser do tipo int.$4Ivy: Strings são variáveis que comportam sequência de caracteres.\n Sempre devem ser declaradas entre aspas.$4Exemplo:\n   string a = \"Alice\";$4Ivy: Ints são variáveis que comportam números inteiros.\n Note que ao declará-las você não utiliza aspas.$4   Exemplo: int b = 5;";
				txt[1] = "...Verificação de senha em progresso@2.@2.@2.$4...Decriptando senha@2.@2.@2.$4\"Senha correta. Porta destrancada!\"$4Ivy: Muito bem!$4Ivy: Prossiga para a sala de energia destes laboratórios.@5$6";
				txt[2] = "...Verificação de senha em progresso...$4...Decriptando senha@2.@2.@2. $4\"ha, ha, ha, você não disse a palavra mágica@2.@2.@2.ha, ha, ha\"$1$4Ivy: recuperação de senha em progresso . . .$4	A = (número de mesas no laboratório A - 7) * 100;$4	B = nome do cientista chefe do lab A;$4recuperação de senha terminada.$4Ivy: Tente novamente:";
				journalText = "Uma variavel inteira usa a palavra reservada\n int, ela é posta antes do nome da variavel.\n    int variavel;\n Ela só aceita numeros inteiros.\n    variavel = 10;\n Bem diferente de uma string que necessita\n ficar entre aspas.\n    string strvariavel = \"10\";";
			}else {
				txt[0] ="Ivy: This door is locked with a personal password.$4Violation of the decree number 481.516 article 23 paragraph 42, Personal passwords are strictly prohibited in public environments.$4Ivy: Password recovery in progress . . .$4   a = (number of tables in the laboratory A - 7) * 100;$4   b = name of chief lab scientist A;$4Password recovery completed.$4Ivy: System error! Unidentified password!\n Human being needed to identify password!$4Ivy: My systems indicate that you are the only person capable of identifying this password.\n When you can, create two variables A and B with their equivalent value to open the port.$4Ivy: Remember that when declaring variables with text they must be of type string, but if they contain integer values they must be of type int. $4Ivy: Strings are variables that contain characters string.\n They should always be between quotation marks.$4Example:\n   string a = \"Alice\";$4Ivy: Ints are variables that contain integer values.\n Note that when you declare them, you do not use quotation marks.\n   Example: int b = 5;";
				txt[1] = "...Password validation in progress@2.@2.@2.$4...Decrypting password@2.@2.@2.$4\"Correct password. Door unlocked!\"$4Ivy: Very well!$4Ivy: Proceed to the electric room of the next laboratories.@5$6";
				txt[2] = "...Password validation in progress...$4...Decrypting password@2.@2.@2. $4\"ha, ha, ha, You didn't say the magic word!@2.@2.@2. ha, ha, ha\"$1$4Ivy: Password recovery in progress . . .$4	A = (number of tables in the laboratory A - 7) * 100;$4	B = name of chief lab scientist A;$4Ivy: Try again:";
				journalText = "An integer variable uses the reserved word int,\n it is put before the variable name.\n    int variable;\n It only accepts integers.\n    variable = 10;\n Quite different from a string that needs to be\n quoted.\n    string strvariable = \"10\";";
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

		private void lessonEnd(){
			promptEnter ();

			if (intLesson && strLesson){
				PauseMenuInventory.instance.addItem (3);
				PauseMenuInventory.instance.itemJournal += journalText;
				lesson++;
                setLogText(txt[1]);
                return;
			}
		}

		private void error(){
			if (!intLesson || !strLesson){
				promptEnter ();
				setLogText (txt [2]); // print error
			}
		}

		public override void promptInterpreter(){
			if (lesson == 2)
				return;
			
			if (lesson == 1) {

				if (getFirstLine ().Contains ("int a =")) {
					
					if (keywordIsValid ("int a =") && !intLesson && getArgumentInt ("=") == 300) {
                        promptEnter();
                        consoleLog += getArgumentInt ("=");
                        sucess.Play ();
						intLesson = true;
						lessonEnd ();
						return;
					} else {
						error ();
						return;
					}

				} else if (getFirstLine ().Contains ("string b =")) {
					
					if (keywordIsValid ("string b =") && !strLesson && getArgumentStr ("=").ToLower() == "ivo"){
                        promptEnter();
                        consoleLog += "Ivo";
                        sucess.Play ();
						strLesson = true;
						lessonEnd ();
						return;
					} else{
						error();
						return;
					}
				}
				promptEnter (); // se digitar algo que ñão entre no segundo if
			}
		}
	}
}