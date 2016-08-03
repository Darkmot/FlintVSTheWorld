using UnityEngine;
using System.Collections;

public enum PGameType {
	Start,
	Run,
	Stop
}

public class PrototypeManager : MonoBehaviour {

	public PrototypeLabel plabel;
	public Animator questionHalo;
	public Animator answerHalo;
	public PGameType gameType;

	// Use this for initialization
	void Start () {
		Restart ();
	}

	public void Restart() {
		gameType = PGameType.Start;
		plabel.ChangeLabel (PLabel.Start);
	}

	public void Begin() {
		gameType = PGameType.Run;
		plabel.ChangeLabel (PLabel.None);
	}

	public void TapAction() {

		switch (gameType) {
		case PGameType.Start:
			Begin ();
			break;
		case PGameType.Run:
			answerHalo.SetTrigger ("Anim");
			plabel.ChangeLabel (PLabel.Perfect);
			break;
		case PGameType.Stop:
			break;
		}

	}
	
}
