using UnityEngine;
using System.Collections;
/*
	Responsavel por criar Fade In/Out

	Colocar em objeto SYSTEM
*/
namespace Devlusion{
	public class Fade : MonoBehaviour {
		
		public Texture2D texture;
		public float	 speed = 0.8f;
		
		private int		 direction = -1;
		private float	 alpha 	   = 1.0f;

        public static  Fade instance;

        void Start(){
            instance = this;
        }

		void OnGUI(){
			alpha += direction * speed * Time.deltaTime;
			alpha = Mathf.Clamp01(alpha);										// retorna valores de 0 a 1
			
			GUI.color = new Color(GUI.color.r,GUI.color.g,GUI.color.b,alpha);
			GUI.depth = -1000;
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height),texture);
		}
		
		void sceneLoaded(){
			callFade(-1);
		}
		
		public float callFade(int direction){
            this.direction = direction;
			return(speed);
		}
	}
}