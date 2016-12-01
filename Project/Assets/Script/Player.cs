using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Devlusion.PauseMenu;
/*
	Responsavel pelo jogador
		Animator mov
		1 ^ playerLow
		2 < playerRight
		3 > playerLeft
		4 v playerTop
		fazer animacao parado
*/
namespace Devlusion{
	public class Player : MonoBehaviour {

		public static Player instance;

		private float 		animCurrSpeed;
		public Scrollbar 	hpBar;
		public Animator 	animator;
		private Rigidbody2D rbody;

		private float hp;
		public  float size =  0.05f;
		private float speed = 3f;
        private float timeToGO;
        private bool  gameOver;

		void Start(){
			instance = this;
			loadData ();
			rbody = GetComponent<Rigidbody2D> ();
		}
		
		//------------------------ Loop members ------------------------//

		void Update(){
            if (gameOver){
                timeToGO += Time.deltaTime;
                PauseMenu.PauseMenuInventory.isPaused = false;
                if (timeToGO > 1f)
                    SceneManager.LoadScene("GameOver", LoadSceneMode.Single);               // Termina o jogo
                return;
            }

            hpBar.size = hp/100f;                                                       // Atualiza valor da barra de hp

            if (PauseMenuBase.isPaused)													// para que o personagem não possa se mover com o jogo pausado mesmo com time scale 1 (quando esta no console)
				return;

            if (hp <= 0.0f){
                Fade.instance.callFade(1);
                gameOver = true;
            }

            if (Application.platform != RuntimePlatform.Android)						// Verifica se jogo esta rodando em um windows ou no unity no windows
				movimentationPC();														// Chama metodo de movimentação em desktop
		}
		
		void OnTriggerStay2D(Collider2D other){
            if (other.CompareTag("Acid"))
                damage(5 * Time.deltaTime);
		}
			
		// Controle e movimentacao android
		void OnGUI(){
			if (PauseMenuBase.isPaused || Application.platform != RuntimePlatform.Android)
				return;

			float sizeH = Screen.height / 9;
			float sizeV = Screen.width / 9;
			float wIvnt = sizeV/2;														// width do slot
			float yIvnt = Screen.height - sizeH;										// posicao y do slot

			animator.SetBool("walk", false);

			while (GUI.RepeatButton (new Rect (wIvnt, yIvnt - sizeH * 2, sizeV, sizeH), "^"))
				movimentationMobile (0f, 1f, Vector2.up * speed);
			while (GUI.RepeatButton (new Rect(wIvnt,yIvnt, sizeV, sizeH), "V"))
				movimentationMobile (0f, -1f, -Vector2.up* speed);
			while (GUI.RepeatButton (new Rect(sizeV ,yIvnt-sizeH, sizeV, sizeH), ">"))
				movimentationMobile (1f, 0f, -Vector2.left* speed);
			while (GUI.RepeatButton (new Rect (0, yIvnt - sizeH, sizeV, sizeH), "<"))
				movimentationMobile (-1f, 0f, Vector2.left* speed);
		}

		//------------------------ Void members ------------------------//

		private void loadData (){
		//	if (PlayerPrefs.HasKey ("playerPositionX") && PlayerPrefs.HasKey ("playerPositionY") && PlayerPrefs.HasKey ("playerPositionZ")) transform.position = new Vector3 (PlayerPrefs.GetFloat ("playerPositionX"), PlayerPrefs.GetFloat ("playerPositionY"), PlayerPrefs.GetFloat ("playerPositionZ"));

			if (PlayerPrefs.HasKey ("playerHP"))
				hp = PlayerPrefs.GetFloat ("playerHP");
			else
				hp = 100.0f;
		}

		public void saveData (){
			//GameData.playerPosition = transform.position;	
			GameData.playerHP = hp;
		}

		private void movimentationMobile(float x, float y, Vector2 dir){
			animator.SetFloat ("x", x);
			animator.SetFloat ("y", y);
			animator.SetBool("walk", true);
			rbody.MovePosition (rbody.position + dir* Time.deltaTime);
		}

		private void movimentationPC(){
			Vector2 mov = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

			if (mov != Vector2.zero) {
				animator.SetBool ("walk", true);
				animator.SetFloat ("x", mov.x);
				animator.SetFloat ("y", mov.y);
				rbody.MovePosition (rbody.position + mov * speed * Time.deltaTime);
			} else 
				animator.SetBool ("walk", false);
		}
		
		public void damage(float value){
			if (hp < 0.0f)
				hp = 0;
			else if (hp > 100.0f)
				hp = 100;
			else
				hp -= value;
		}
			
		public void cure(float value){
			if (hp < 100)
				hp += value;
			else
				hp = 100;
		}

		//------------------------ Static members ------------------------//
			
		//public static void setHP(float value){
		//	if (value > 0.0f && value < 100.0f)
		//	//	hp = value;																// Altera valor do hp
		//}
	}
}