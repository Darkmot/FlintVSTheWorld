using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PLabel {
	Start,
	Miss,
	Bad,
	Good,
	Perfect,
	None
}


public class PrototypeLabel : MonoBehaviour {

	public void ChangeLabel (PLabel pl)
	{
		Text t = GetComponent<Text> ();
		switch (pl) {
		case PLabel.Start:
			t.text = "PRESS TO START";
			t.color = Color.red;
			break;
		case PLabel.Miss:
			t.text = "MISS";
			t.color = Color.red;
			break;
		case PLabel.Bad:
			t.text = "BAD";
			t.color = new Color(1.0f,0.35f,0f,1f);
			break;
		case PLabel.Good:
			t.text = "GOOD";
			t.color = Color.yellow;
			break;
		case PLabel.Perfect:
			t.text = "PERFECT";
			t.color = Color.green;
			break;
		case PLabel.None:
			t.text = "";
			break;
		}
	}

}
