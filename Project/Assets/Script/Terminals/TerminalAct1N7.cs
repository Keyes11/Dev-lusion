using UnityEngine;
using Devlusion.PauseMenu;
namespace Devlusion.PauseMenu.Console{
	public class TerminalAct1N7 : PauseMenuConsole {
		private int lesson;
		private int? currentValueA = null;
		private int? currentValueB = null;
		private string[] txt;
		private string journalText;
		private bool[] way;

		public GameLight[] lights;
		public Teleporter teleporter;

		void Start (){
			GameObject[] objs = GameObject.FindGameObjectsWithTag("LabBCPowerB");
			lights = new GameLight[objs.Length];

			for(int i = 0; i < objs.Length; i++)
				lights[i] = objs[i].GetComponent<GameLight> ();

			way = new bool[4];
			txt = new string[8];
			if (Vocabulary.language){ // pt
				txt[0] = "Ivy: O teleporte adiante está sem energia.\n Você precisa transferir a eletricidade destes laboratórios para poder utilizá-lo.$4Ivy: O comando está protegido por um código de if/else encadeado.\n Ivy: Dessa vez temos operadores lógicos nos parâmetros da estruturade decisão.$4Ivy: Operador lógico 'E' (&&), significa que os dois ou mais parâmetros devem ser verdadeiros para prosseguir na estrutura.$4Ex:(2>1 && 5<10) VERDADEIRO\n (4>10 && 1<100) FALSO$4Ivy: Operador lógico 'OU' (||), significa que um dos parâmetros deve ser verdadeiro, não importando se os outros são falsos.\n Exemplo:\n   (8<45 || 70>75) VERDADEIRO\n   (7<1  || 5>10)  FALSO$4Ivy: Como fez anteriormente, use o comando print na ordem correta,mas dessa vez use as variaveis \"a\" e \"b\" já existentes.$4Ivy: Após criá-las, você só precisar atribuir um novo valor para elas com o valor correspondente a parte da estrutura de decisão que você quer chamar.\n Exemplo:\n   int a; //variavel a criada\n   a = 0; //atribuição de valor para a variavel a$4if(a>60 && b==15){$4   if(a==80 || b<10){$4      print(\"Terceiro código!\");$4   } else if((a==420 && b==15) || (a>85 && b<10)){$4      print(\"Quarto código!\");$4   } else {$4      print(\"Primeiro código!\");$4   }$4} else {$4   print(\"Segundo código!\");$4}";
				txt[1] = "Erro na sequência do código.$4if(a>60 && b==15){$4   if(a==80 || b<10){$4      print(\"Terceiro código!\");$4   } else if((a==420 && b==15) || (a>85 && b<10)){$4      print(\"Quarto código!\");$4   } else {$4      print(\"Primeiro código!\");$4   }$4} else {$4   print(\"Segundo código!\");$4}\n Ivy: Tente novamente.";
				txt[3] = "Primeiro código!";
				txt[4] = "Terceiro código!";
				txt[5] = "Quarto código!";
				txt[6] = "Segundo código!";
				txt[7] = "   --Energia transferida para teleporte--\n   --Desligando em @13,@12--@5$6";
				journalText = "Os operadores booleanos servem respectivamente: \"e\" (&&)\n verifica se ambas as comparações são verdadeiras e;\n \"ou\" (||) que verifica se uma das comparações é verdadeira.\n Exemplo:\n if (a < b || a = b){...}\n if (a > b && (a < c || a = C){...}";
			} else {
				txt[0] = "Ivy: The teleport further ahead is powerless.\n You will need to transfer the electricity from labs B and C to power it up.$4Ivy: The command is protected by a nested if/else structure.$4Ivy: But this time we have logical operators inside their parameters.$4Ivy: Logical operator 'AND' (&&) means that two or more parameters must be true to proceed inside the structure.\n Example:\n   (2>1 && 5<10) TRUE\n   (4>10 && 1<100) FALSE$4Ivy: Logical operator 'OR' (||) means that as long as one of the parameters is true the code will execute, even if the rest is false.\n Example:\n   (8<45 || 70>75) TRUE\n   (7<1  || 5>10)  FALSE$4Ivy: But this time use as existing \"a \" and \"b \" variables.$4Ivy: After declaring them, you will need to assign a value to them corresponding with the part of the if/else structure you want to execute.\n Example:\n   int a; //declaring variable a\n   a = 0; //assigning a value to the variable a$4if(a>60 && b==15){$4   if(a==80 || b<10){$4      print(\"Third code!\");$4   } else if((a==420 && b==15) || (a>85 && b<10)){$4      print(\"Fourth code!\");$4   } else {$4      print(\"First code!\");$4   }$4} else {$4      print(\"Second code!\");$4}";
				txt[1] = "Error in the code sequence.$4if(a>60 && b==15){$4   if(a==80 || b<10){$4      print(\"Third code!\");$4   } else if((a==420 && b==15) || (a>85 && b<10)){$4      print(\"Fourth code!\");$4   } else {$4      print(\"First code!\");$4   }$4} else {$4      print(\"Second code!\");$4}\n Ivy: Try again.";
				txt[3] = "First code!";
				txt[4] = "Third code!";
				txt[5] = "Fourth code!";
				txt[6] = "Second code!";
				txt[7] = "   --Electric power transferred to the teleport--\n   ----Shutting down in @13,@12--@5$6";
				journalText = "The Boolean operators respectively serve: \"e\" (&&) checks\n whether both comparisons are true and; \"or\" (||) that checks\n whether one of the comparisons is true.\n Exemple:\n if (a < b || a = b){...}\n if (a > b && (a < c || a = C){...}";
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

        public override void promptEnter() {
            rollText();
            consoleLog += "\nroot@devlusion>" + console + " >> ";
        }

        private void tryAgain(){
            setNull();
            console = "";

            for (int i = 0; i < way.Length; i++)
				way [i] = false;
			setLogText (txt[1]);
		}

		private void setNull(){
            console = "";
            currentValueA = currentValueB = null;
		}

		public override void promptInterpreter(){
			if (lesson == 2)
				return;

			int? value = getArgumentInt ("=");
			
			if (value == null){
				printPromptError(1);
				return;
			}
			
			if (console.Contains("a")){
				if (keywordIsValid("a"))
					currentValueA = value;
			} else{
				if (keywordIsValid("b"))
					currentValueB = value;
			}
			
			
			
			// Retorna se uma das vars não estiver preenchida
			if (currentValueA == null || currentValueB == null) {
				console = "";
				promptEnter();

			} else {
                promptEnter();
                // Checagem do valor

                if (currentValueA > 60 && currentValueB == 15)
                {
                    if (currentValueA == 80 || currentValueB < 10)
                    {
                        way[1] = true;
                        consoleLog += txt[4]; //print("Terceiro código!");
                        console = "";
                        if (!way[3])
                        {
                            tryAgain();
                            return;
                        }
                    }
                    else if ((currentValueA == 420 && currentValueB == 15) || (currentValueA > 85 && currentValueB < 10))
                    {
                        way[2] = true;
                        consoleLog += txt[5]; //print("Quarto código!");
                        console = "";
                        if (!way[1])
                        {
                            tryAgain();
                            return;
                        }
                    }
                    else
                    {
                        way[0] = true;
                        console = "";
                        setNull();
                        consoleLog += txt[3]; //print("Primeiro código!");
                    }
                }
                else
                {
                    way[3] = true;
                    consoleLog += txt[6]; //print("Segundo código!");
                    console = "";
                    if (!way[0])
                    {
                        tryAgain();
                        return;
                    }
                }
                console = "";
                setNull();

                for (int i = 0; i < way.Length; i++)
                    if (!way[i])
                        return;

                // Fim do metodo
                sucess.Play();
                PauseMenuInventory.instance.addItem(6);
                PauseMenuInventory.instance.addItem(3);
                PauseMenuInventory.instance.itemJournal += journalText;
                lesson++;
                teleporter.istLocked(false);
                for (int i = 0; i < lights.Length; i++)
                    lights[i].isDisable = true;
                setLogText(txt[7]);
            }   
        }
	}
}