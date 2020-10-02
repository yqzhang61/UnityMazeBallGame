using System.Collections;
using UnityEngine;

public class ScriptManager : MonoBehaviour {

	public Chaser chaser;
	public TwopointsMove enemy;
	public bool counter = true;
	// Use this for initialization
	void Start ()
	{
		chaser = GetComponent<Chaser>();
		enemy= GetComponent<TwopointsMove>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (GameObject.FindWithTag ("Door") == null && counter==true) {
			Debug.Log ("hi");
			wait ();
			chaser.enabled = true;
			enemy.enabled = false;
			counter=false;
			Destroy(this.GetComponent<TwopointsMove>());
		}
	}
	IEnumerator wait()
	{
		yield return new WaitForSeconds(10);
	}
}
