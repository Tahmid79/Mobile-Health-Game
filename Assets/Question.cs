using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class Question {

	public string questionStatement;
	public int trueSelectionIndex;

	public static List<Option> options;

	public string optionOne;
	public string optionTwo;
	public string optionThree;
	public string optionFour;

	public Question(int questionId, string questionText, string optionOneText, string optionTwoText,
		string optionThreeText, string optionFourText, int correctOptionIndex){

			questionStatement = questionText;
			optionOne = optionOneText;
			optionTwo = optionTwoText;
			optionThree = optionThreeText;
			optionFour = optionFourText;
			trueSelectionIndex = correctOptionIndex;

		}

	

}
