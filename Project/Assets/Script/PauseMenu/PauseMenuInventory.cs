using UnityEngine;
//using System.Collections;
using UnityEngine.SceneManagement;
/*
	Responsavel por criar o inventario e interface em jogo, alem 
	de todas as interacoes relativas a mesma.
	
	Colocar no objeto Player
	
	inventoryIndex
		0 - nulo
		2 - jornal
        3-5 - keycard
		para adc as botas: inventoryIndex[indice] = 1;
*/
namespace Devlusion.PauseMenu{
	public class PauseMenuInventory : PauseMenuBase {

		public static PauseMenuInventory instance;

        public Vector2   scrollPosition = Vector2.zero;
        public Texture2D journal;
		public Texture2D keycard;

		public static int[] inventoryIndex;
		private bool 		readingJournal;
		private bool		descriptionKeycard;
		private bool		messageLabeIsEnabled;
		private string 		messageLabelText;
        public string       itemJournal = "";

        void Awake (){
			instance = this;
			loadData ();
        }

        void Update(){
			saveData ();
			if (Input.GetKeyDown (KeyCode.Return) && pauseButton)					// Se o botão enter (return) for precionado
				prepareToPause ();
		}

        void OnGUI(){
            if (onTerminal)															// impede de executar se o terminal estiver aberto
				return;

			if (GUI.Button (new Rect (Screen.width - 60, 0, 60, (Screen.height / 6) / 2), // ERROR: NullReferenceException: Object reference not set to an instance of an object
				    Vocabulary.vocabulary [7]))										// Cria o botão de pausa
				prepareToPause();

			if (!isPaused)															// Se isPaused for true então
				return;
			
			if (messageLabeIsEnabled) {
				GUI.Label(new Rect (0,Screen.height/2,Screen.width,Screen.height), messageLabelText);
				return;
			}

			if (searchItem (2) && readingJournal){
                scrollPosition = GUI.BeginScrollView(new Rect(0, 0, Screen.width - 60, Screen.width - 10), scrollPosition, new Rect(0, 0, Screen.width - 50, Screen.width));
                GUI.TextField(new Rect(0, 0, Screen.width - 60, Screen.width), itemJournal);
                GUI.EndScrollView();
                return;
			}
			
			if ((searchItem (3) || searchItem (4)) && descriptionKeycard){
				GUI.Label(new Rect (Screen.width/2,Screen.height/2,Screen.width,Screen.height), "Keycard");
				return;
			}

			// Create inventory
			Texture2D texture;															// texture que recebe a img do item que esta no slot atual
			int	  	  wIvnt = Screen.width/10;											// width do slot
			float 	  yIvnt = Screen.height/2-100;										// posição y do slot

			for (int i = 0, x = wIvnt; i < inventoryIndex.Length; i++, x += wIvnt) {    // Percorre o vetor para criar os slots		                                                    // então, escreve o nome do item
                if (inventoryIndex[i] == 2)
                    texture = journal;
				else if (inventoryIndex[i] == 4)
                    texture = keycard;
				else                                                                    // senão, 
                    texture = null;														// então printa nada no slot

				if (i == 5) {															// "Quebra" o inventario em duas linhas
					yIvnt += wIvnt;
					x = wIvnt;
				}

				if (GUI.Button (new Rect (x + wIvnt + 20, yIvnt, Screen.width / 9.8f, wIvnt), texture))// Cria o slot
					inventoryAction(i);
			}
			
			// Create exit and help button
			float yExit = Screen.height/3+(yIvnt-20);									// posição y do botão sair
			float xExit = (Screen.width/2)-(wIvnt);										// posição x do botão sair
		
			if (GUI.Button(new Rect(xExit,yExit,Screen.width/5,Screen.height/9),
					Vocabulary.vocabulary[8])){ 										// Cria o botão sair
				pauseGame();															// Despausa o jogo
				SceneManager.LoadScene("Menu",LoadSceneMode.Single);					// Volta para a cena Menu
			}
			/*	
			if (GUI.Button(new Rect((wIvnt+wIvnt)+20,yExit,Screen.width/10,
					Screen.height/9),Vocabulary.vocabulary[11])){
				showMessageLabel("Você realmente achou que iriamos lhe ajudar em algo?\nAprenda programando.\n70 101 99 104 101 32 105 115 115 111 32 97 103 111 114 97\n32 101 32 118 195 161 32 106 111 103 97 32 68 111 111 109 46 ");
			}
			*/
		}

		public void showMessageLabel (string text){
			messageLabelText = text;
			messageLabeIsEnabled = true;
		}

		public void hideMessageLabel(){
			messageLabelText = "";
			messageLabeIsEnabled = false;
		}

		private void prepareToPause(){
			hideMessageLabel();
			readingJournal = false;
			descriptionKeycard = false;
			pauseGame ();														// Pausa/Despausa o jogo
		}

		private void loadData (){
			if (PlayerPrefs.HasKey ("inventoryItemJournal"))
				itemJournal = PlayerPrefs.GetString ("inventoryItemJournal");

			inventoryIndex = new int[10];
			for (int i = 0; i < 10; i++) {
				if (PlayerPrefs.HasKey ("inventoryIndex" + i))
					inventoryIndex [i] = PlayerPrefs.GetInt ("inventoryIndex" + i);
			}
		}

		public void saveData (){
			GameData.inventoryItemJournal = itemJournal;
		}

		// usa o item no indice atual do vetor
		private void inventoryAction (int index){
			int i = getIndexItem (index);

			if (i == 2)
				readingJournal = true;
			else if (i == 4) // 3 e 5 são keycards ocultos
				descriptionKeycard = true;
			//6 é terminal 7 completo
			//7 é terminal 9 completo
		}

		// retorna um true se o item existir
		public bool searchItem (int item){												// Verifica se o item existe no inventario
			for(int i = 0; i < inventoryIndex.Length; i++)								// Percurre o array do inventario,
				if (inventoryIndex[i] == item)											// verifica se o item existe no inventario
					return true; 														// e retorna true se existir e impede mais laços do for
			return false;																// senão achar, retorna false.
		}

		// procura o indice do item
		public int seachIndexItem (int item){
			for(int i = 0; i < inventoryIndex.Length; i++)								// Percurre o array do inventario,
				if (inventoryIndex[i] == item)
					return i;
			return 0;		
		}

		// retorna item do incide atual do inventario
		public int getIndexItem(int index){
			return inventoryIndex [index];
		}

		public void delItem (int item){
			for(int i = 0; i < inventoryIndex.Length; i++)								// Percurre o array do inventario,
				if (inventoryIndex[i] == item){
					inventoryIndex[i] = 0;
					return;																
				}
		}

		public void addItem (int item){													// Adiciona um item, se tiver espaço no iventario
			if (searchItem(item))
				return;
			for(int i = 0; i < inventoryIndex.Length; i++)								// Percurre o array do inventario,
				if (inventoryIndex[i] == 0){											// verifica se o slot está vazio
					inventoryIndex[i] = item;											// se sim, adiciona o item no slot e,
					return;																// retorna para não executar mais o for.
				}
		}
	}
}