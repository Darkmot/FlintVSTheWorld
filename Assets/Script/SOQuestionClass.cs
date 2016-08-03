using UnityEngine;
using System.Collections;

[System.Serializable]
public class QuestionData {
	public float length = 4.8f;
	public float[] actionTime;
}

[CreateAssetMenu(fileName = "SOQuestion",menuName = "Prototype/Question",order = 2)]
public class SOQuestionClass : ScriptableObject {
	public QuestionData[] qData;
}
