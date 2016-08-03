using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum PGameType {
	Start,
	Intro,
	Run
}

public class PrototypeManager : MonoBehaviour {

	public PrototypeLabel plabel;
	public Animator questionHalo;
	public Animator answerHalo;
	public PGameType gameType;

	public AudioSource musicSource;
	public AudioSource questionSource;
	public AudioSource answerSource;
	public SOAudioBanksClass audioLibrary;
	public SOQuestionClass[] question;

	public Text perfectText;
	public Text goodText;
	public Text badText;
	public Text missText;

	public float perfectTolerance;
	public float goodTolerance;
	public float badTolerance;

	int selectedTheme;
	int selectedSong;
	int selectedQuestion;
	float startQuestion;
	int curQuestion;

	int perfectCtr;
	int goodCtr;
	int badCtr;
	int missCtr;

	List<int> savedIndex;

	// Use this for initialization
	void Start () {
		savedIndex = new List<int> ();
		Restart ();
	}

	public void Restart() {
		answerSource.Stop ();
		musicSource.Stop ();
		questionSource.Stop ();

		gameType = PGameType.Start;
		plabel.ChangeLabel (PLabel.Start);
		answerSource.clip = audioLibrary.answer;
		selectedTheme = Random.Range (0,audioLibrary.audioTheme.Length);
		musicSource.clip = audioLibrary.audioTheme [selectedTheme].intro;
		questionSource.clip = audioLibrary.audioTheme [selectedTheme].question;

		perfectCtr = 0;
		goodCtr = 0;
		badCtr = 0;
		missCtr = 0;
		UpdateScore ();

		savedIndex.Clear ();

	}

	public void Begin() {
		gameType = PGameType.Intro;
		plabel.ChangeLabel (PLabel.None);
		musicSource.Play ();
	}

	public void TapAction() {

		switch (gameType) {
		case PGameType.Start:
			Begin ();
			break;
		case PGameType.Run:
			answerHalo.SetTrigger ("Anim");
			answerSource.Play ();
			EvaluateTap ();
			break;
		case PGameType.Intro:
			break;
		}

	}

	void RandomizeSong() {
		gameType = PGameType.Run;
		selectedSong = Random.Range (0,audioLibrary.audioTheme[selectedTheme].segments.Length);
		musicSource.clip = audioLibrary.audioTheme [selectedTheme].segments[selectedSong];
		RandomizeQuestion ();
		musicSource.Play ();
	}

	void Update() {
		if (gameType == PGameType.Run) {
			if (!musicSource.isPlaying)
				RandomizeSong ();

			float curDelta = Time.timeSinceLevelLoad - startQuestion;
			if (curDelta >= question[selectedTheme].qData [selectedQuestion].length)
				RandomizeQuestion ();
			else if ((curQuestion < question[selectedTheme].qData [selectedQuestion].actionTime.Length) && (curDelta >= question[selectedTheme].qData [selectedQuestion].actionTime [curQuestion])) {
				PlayQuestion ();
			}
		} else if (gameType == PGameType.Intro) {
			if (!musicSource.isPlaying)
				RandomizeSong ();
		}
	}

	void RandomizeQuestion() {		
		startQuestion = Time.timeSinceLevelLoad;
		curQuestion = 0;
		selectedQuestion = Random.Range (0,question[selectedTheme].qData.Length);
		savedIndex.Clear ();
	}

	void PlayQuestion() {
		curQuestion++;
		questionSource.Play ();
		questionHalo.SetTrigger ("Anim");

	}

	void UpdateScore() {
		perfectText.text = "P: "+perfectCtr;
		goodText.text = "G: "+goodCtr;
		badText.text = "B: "+badCtr;
		missText.text = "M: "+missCtr;
	}

	void EvaluateTap() {
		float curTime = (Time.timeSinceLevelLoad - startQuestion) - (question[selectedTheme].qData [selectedQuestion].length / 2f);
		float [] actionTime= question[selectedTheme].qData [selectedQuestion].actionTime;

		int nearestQIndex = -1;
		float delta = question [selectedTheme].qData [selectedQuestion].length;

		for (int i = 0; i < actionTime.Length; i++) {
			if (Mathf.Abs (curTime - actionTime [i]) < delta) {
				nearestQIndex = i;
				delta = Mathf.Abs (curTime - actionTime [i]);
			}
		}

		if (nearestQIndex >= 0) {
			if (!savedIndex.Contains (nearestQIndex)) {
				if (delta <= perfectTolerance) {
					plabel.ChangeLabel (PLabel.Perfect);
					perfectCtr++;
				} else if (delta <= goodTolerance) {
					plabel.ChangeLabel (PLabel.Good);
					goodCtr++;
				} else if (delta <= badTolerance) {
					plabel.ChangeLabel (PLabel.Bad);
					badCtr++;
				} else {
					plabel.ChangeLabel (PLabel.Miss);
					missCtr++;
				}
				savedIndex.Add (nearestQIndex);
			} else {
				plabel.ChangeLabel (PLabel.Miss);
				missCtr++;
			}
		} else {
			plabel.ChangeLabel (PLabel.Miss);
			missCtr++;
		}
		UpdateScore ();
		print ("TapTime: "+curTime+", near: "+actionTime[nearestQIndex]+"["+nearestQIndex+"]");
	}
}
