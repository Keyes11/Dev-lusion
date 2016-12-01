using UnityEngine;
using Devlusion.PauseMenu;
namespace Devlusion.PauseMenu.Console{
	public class TerminalAct1N2 : PauseMenuConsole {
		private int lesson;
		private string[] txt;
		private string journalText;

		void Start (){
			txt = new string[4];
			if (Vocabulary.language){ // pt
				txt[0] = "Ivy: Meus sistemas indicam que o terminal do laboratório A está desligado.\n Precisamos ligá-lo através desta sala de energia.$4Ivy: Aparentemente um dos código ainda está funcional.\n Vou adicionar explicações por meio de comentários dentro do código.$4//Isto é um comentário. Todo comentário começa com duas barras \"//\"$4//esta linha lê o input do usuário, ou o que o usuário digitar e coloca estes dados dentro da variável do tipo //'int' chamada 'entrada' //Note que \"=\" é o sinal para atribuição int entrada = console.readline();$1$4//esta é uma estrutura de decisão\n //Note que \"==\" verifica se um valor é igual ao outro se a variável entrada tiver o valor de 1 então\n if(entrada == 10){$4    //terá a saída de dados \"terminal lab A ativo!\"\n    print(\"terminal lab A ativo!\");\n   \n    //e executará o método que ativa o terminal do lab A\n    terminalA();\n }$4//mas se a variável entrada tiver qualquer outro valor como por exemplo '-3'\n else{$4    //terá a saída de dados \"Código incorreto\"\n    //e não executará nenhum método\n    print(\"Código incorreto\");\n }$4Ivy: ERRO NO BANCO DE DADOS$4    ..Dados corrompidos...$4    Ivy: O vírus em meu sistema está se alastrando mais rápido do que eu calculei.\n Parte do código foi deletada.$4$1entrada = 10;\n if(---){\n    print(\"terminal lab A ativo!\");\n    terminalA();\n }$4Ivy: O parâmetro da estrutura de decisão desapareceu!\n Preciso que você digite o parâmetro novamente conforme meu exemplo:$4Ivy: if(){$4Ivy: Dentro dos parênteses digite a variável entrada com seu respectivo valor:";
				txt[1] = "Ivy: Meus sistemas indicam que o terminal do $4...Substituindo código@2.@2.@2.$4...Substituição completa...\n ...código compilado com sucesso...$4...Terminal do laboratório A ativo...\n ...Bem-vindo Dr. Ivo...$4Ivy: Então o código pertencia ao Dr. Ivo.\n Interessante.$4Ivy: Prossiga para o terminal do laboratório!@5$6";
				txt[2] = "...Substituindo código@2.@2.@2.$4ERRO NO PARÂMETRO IF/ELSE, VERIFIQUE NOVAMENTE$4Ivy: Digite conforme o meu exemplo: if(){\n Dentro dos parênteses digite a variável entrada com seu respectivo valor.$4entrada = 10;\n if(---){\n    print(\"terminal lab A ativo!\");\n    terminalA();\n }";
				txt[3] = "if(entrada==10){";
				journalText = "A palavra reservada if() serve para imprimir\n serve para comparar duas instruções, a sua\n sintaxe é:\n    if (declaração operador declaração){\n        //instruções\n    }\n se a comparação for verdadeira então ela\n executará o que estiver dentro das chaves \"{}\".\n \n else executa um codigo se a comparação\n for falsa.\n    else {\n        //instruções\n    }\n ";
			}else {
				txt[0] = "Ivy: My systems are indicating that the terminal in lab A is offline.\n We need to power it up through the terminal in this electrical room.$4Ivy: Apparently one of the codes is still functional.\n I will add explanations by comments inside the code.$4//This is a comment. Every comment starts with double slash \"//\"$4//The next line reads the user input and stores the data inside  the variable of type 'int' called 'entrance'.\n //Note that \"=\" is an assignment operator\n //Also note that a variable of type 'int' can only store integer values int entrance = console.readline();$1$4//This is a conditional statement\n //Note that \"==\" compares the two values to verify their equality\n //if the variable entrance has the value '1' then\n if(input == 10){$4    //then output this to the user \"lab A terminal\n    //activated!\"\n    print(\"lab A terminal activated!\");$4    //and execute the following method that\n    //activates the terminal in lab A\n    terminalA();\n }$4//but if the variable entrance has any other value like for example '-3'\n else{$4    //the code will output \"Wrong code\"\n    // and will not execute any method\n    print(\"Wrong code\");\n }$4    Ivy: DATABASE ERROR\n    ...Corrupted data...$4Ivy: The virus in my system is spreading faster\n than I calculated.\n Part of the code was deleted.$4$1entrance = 10;\n if(---){\n    print(\"lab A terminal activated!\");\n    terminalA();\n }$4Ivy: The parameter in the conditional statement disappeared!\n I need you to type the parameter again, follow my example:$4Ivy: if(){$4Ivy: Within the parentheses type the variable entrance using the  comparative structure to make the statement true:";
				txt[1] = "...Replacing code@2.@2.@2.$4...Task complete...\n ...Code compiled with success...$4...Laboratory A terminal activated...\n ...Welcome Dr. Ivo...$4Ivy: So the code belonged to Dr. Ivo.\n Interesting.$4Ivy: Proceed to the laboratory terminal!@5$6";
				txt[2] = "...Replacing code@2.@2.@2.$4ERROR IN PARAMETER IF / ELSE, CHECK AGAIN$4Ivy: Type as per my example: if(){\n Ivy: Within the parentheses type the variable entrance using the comparative structure to make the statement true:$4entrance = 10;\n if(---){\n    print(\"lab A terminal activated!\");\n    terminalA();\n }";
				txt[3] = "if(entrance==10){";
				journalText = "The keyword if () is used to print serves to\n compare two statements, its syntax is:\n    if (statement operator statement){\n        /instructions\n    }\n If the comparison is true then it executes\n whatever is inside the \"{}\".\n \n else executes a code if the comparison is false.\n    else {\n        //instruções\n    }\n ";
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

		public override bool promptRead(){
			promptInterpreter();
			return true;
		}

		public override void promptInterpreter(){
			if (lesson == 2)
				return;

			if (getFirstLine ().Replace (" ", "") == txt [3]) {
                promptEnter();
                sucess.Play();
                PauseMenuInventory.instance.addItem (4); // ganha o 4 pois o 3 abriria a porta
				PauseMenuInventory.instance.itemJournal += journalText;				
                lesson++;
                setLogText(txt[1]);
                return;
			} else {
				promptEnter();
				setLogText (txt [2]);
				return;
			}
		}
	}
}