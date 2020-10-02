using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed;
	public Text countText;
	public Text winText;
	public Text failureText;
	private int count;
	private int lives;
	public  Vector3 respawnPosition;
	private Quaternion respawnRotation;
	public Button Restart;
	public Button Nextlevel;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		lives = 3;
		SetCountText ();
		winText.text = "";
		failureText.text = "";
		/*GameObject NextLevel=GameObject.FindWithTag("NextLevel");
		NextLevel.SetActive (false);
		GameObject Restart=GameObject.FindWithTag("Restart");
		Restart.SetActive (false);*/
		Restart.gameObject.SetActive (false);
		Nextlevel.gameObject.SetActive (false);

		// store initial position as respawn location
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;
	}


	void FixedUpdate(){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical);
		rb.AddForce (movement*speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		} 
		else if (other.gameObject.CompareTag ("Enemy")||other.gameObject.CompareTag("DeadZone")) 
		{
			lives = lives - 1;
			if (lives == 2) 
			{
				GameObject life1 = GameObject.FindWithTag ("life1");
				Destroy (life1, 0.0f);
			}
			if (lives == 1) 
			{
				GameObject life2 = GameObject.FindWithTag ("life2");
				Destroy (life2, 0.0f);
			}
			if (lives == 0) 
			{
				GameObject life3 = GameObject.FindWithTag ("life3");
				Destroy (life3, 0.0f);
			}
			CheckLives ();
		}
	}

	void SetCountText()
	{
		if (count <= 5) 
		{
			countText.text = count.ToString () + " of 5";
		} 
		else 
		{
			countText.text = (count-5).ToString () + " of 8";
		}

		if (count >= 5) 
		{
			GameObject Door = GameObject.FindWithTag ("Door");
			Destroy (Door, 0.0f);
			if (count >= 10) 
			{
				this.gameObject.SetActive (false);
				winText.text="Congratulation！You Win!";
				Nextlevel.gameObject.SetActive (true);
				Restart.gameObject.SetActive (true);

			}

		}
		/*if (count >= 5) 
		{
			

			//winText.text="You Win!";
		}*/
	}

	void CheckLives()
	{
		if (lives > 0) {
			wait ();
			transform.position = respawnPosition;    // reset the player to respawn position
			transform.rotation = respawnRotation;
			
		} else 
		{
			this.gameObject.SetActive(false);
			failureText.text="Game Over!";
			Restart.gameObject.SetActive (true);

		}

	}

	void CheckCount()
	{
		if (count >= 5) 
		{
			
		}
	}
	IEnumerator wait()
	{

		yield return new WaitForSeconds(1);

	}


}
