using UnityEngine;
using System.Collections;

namespace Devlusion{
	public class Teleporter : MonoBehaviour {
		
		public 	 bool 	     locked;
		public 	 int 		 indexToTeleport;
		public	 AudioSource portalSound;
		public   Transform[] localObject;
		public 	 Animator 	 anim;
		private  GameObject  player;

		void Start(){
			indexToTeleport = 0;
			player = GameObject.Find ("Player");
			istLocked (locked);
		}

		public void istLocked(bool boolean){
			locked = boolean;
			anim.enabled = !locked;
		}

		void OnTriggerEnter2D(Collider2D other) {
			// 0 = self / 1 = lab a / 2 = lab f / 3 = end
			if (other.CompareTag ("Player") && !locked) {		
				player.transform.position = new Vector3 (localObject[indexToTeleport].position.x, localObject[indexToTeleport].position.y, transform.position.z); // transporta o player para onde o objeto determinado via inspector esta
				portalSound.Play();
			}
		}
	}
}