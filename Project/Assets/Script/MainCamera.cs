using UnityEngine;
using System.Collections;
/*
	Responsavel por fazer a camera (tranfrom) seguir o player
	
	Para a cenas com UIButtons (principalmente cena Menu).
*/
namespace Devlusion{
	public class MainCamera : MonoBehaviour {
		public Transform player;
		
		void LateUpdate(){
			transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x,player.position.y,transform.position.z),1f);
		}
	}
}