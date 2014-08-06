using UnityEngine;
using System.Collections;

public class DefaultPlayer : Player
{
	
		bool grounded = false;
		bool isRightFacing = true;

		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{

				ApplyInputs ();
				SetAnimationParameters ();

				if (rigidbody.velocity.x < 0)
						transform.localScale = new Vector3 (-1, 1, 1);
				else if (rigidbody.velocity.x > 0)
						transform.localScale = new Vector3 (1, 1, 1);
		}

		override protected void ApplyInputs ()
		{

		}

		override protected void SetAnimationParameters ()
		{
				//GetComponent<Animator>().SetBool("Running",rigidbody.velocity.x != 0);
				//GetComponent<Animator>().SetInteger("VerticalMovement",Mathf.RoundToInt(rigidbody.velocity.y));
				/*
		if(currSpeed.y > 0){
			GetComponent<Animator>().SetInteger("VerticalMovement",1);
		}else if (currSpeed.y < 0){
			GetComponent<Animator>().SetInteger("VerticalMovement",-1);
		}else{
			GetComponent<Animator>().SetInteger("VerticalMovement",0);
		}
		*/
		}

		override public void JumpAction ()
		{
				if (grounded)
						rigidbody.AddForce (transform.up * PropertyManager.getInstance ().JumpHeight);
		}

		override public void DashAction ()
		{
				float dashDistance = PropertyManager.getInstance ().JumpHeight * 5;
				if (isRightFacing) {
						if (willNotCollide ()) {
								//Needs to be MovePosition instead of AddForce for dashes.  At each loop interval, check if you will dash into a wall.
								rigidbody.AddForce (transform.right * dashDistance);
						} 
				} else {
						if (willNotCollide ()) {
								//Needs to be MovePosition instead of AddForce for dashes.  At each loop interval, check if you will dash into a wall.
								rigidbody.AddForce (transform.right * dashDistance * -1);
						}
				}
		}

		override public void UseAction ()
		{

		}

		override public void SetGrounded (bool ground)
		{
				grounded = ground;
		}

		override public void KillPlayer ()
		{
				//GetComponent<Animator>().SetBool("Alive",false);
				rigidbody.isKinematic = true;
				IsAlive = false;
		}

		public void OnDeath ()
		{
				LevelLoader.getInstance ().ReloadCurrentLevel ();
		}

		void OnCollisionEnter(Collision col){
			if(col.gameObject.name == "tile_34(Clone)"){
			Destroy(col.gameObject);
			}
		}
		override public void setDirection (bool isRight)
		{
				isRightFacing = isRight;
		}

		bool willNotCollide ()
		{
		 
				return true;
		}
}
