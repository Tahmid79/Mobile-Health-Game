using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class GameManager : MonoBehaviour {

private Question currentQuestion;

private List<int> answeredQuestions = new List<int>();

// private List<int> unansweredQuestions = new List<int>();

// private List<Question> questions = new List<Question>();

private List<Question> unansweredQuestions = new List<Question>();


private int questionsAnswered = 0;

private static int sessionScore = 0;


[SerializeField]
private Text questionText;

[SerializeField]
private Text optionOneText;
[SerializeField]
private Text optionTwoText;
[SerializeField]
private Text optionThreeText;
[SerializeField]
private Text optionFourText;

private int selectedOption = 0;

[SerializeField]
private float transitionDelay = 1f;

//Assign int id to options
[SerializeField]

GameObject optionOneLayer, optionTwoLayer, optionThreeLayer, optionFourLayer;

private int trueSelectionIndex;

private string connectionString;

private int currentQuestionNumber = 0;

private int currentQuestionIndex;

void Start(){

	Debug.Log("Startup initialized");

	connectionString = "URI=file:" + Application.dataPath + "/quiz_questions.db";

	//Initialize unansweredQuestions

	//Initialize object references
	optionOneLayer = GameObject.Find("OptionOneLayer");
	optionTwoLayer = GameObject.Find("OptionTwoLayer");
	optionThreeLayer = GameObject.Find("OptionThreeLayer");
	optionFourLayer = GameObject.Find("OptionFourLayer");

	loadQuestions();
	System.Random rnd = new System.Random();

	// //Set the current question index which will be used later to remove the answered question from the List
	// currentQuestionIndex = rnd.Next(0, questions.Count);

	// currentQuestionIndex = rnd.Next(0, unansweredQuestions.Count);

	setCurrentQuestion(rnd.Next(0, unansweredQuestions.Count));

	sessionScore = 0;

	// Debug.Log("Game startup complete");

	
}

private void setCurrentQuestion(int index){

	// Debug.Log("Current question method called.");

	// 	Debug.Log("Current question is being set");
		currentQuestionNumber++;

		currentQuestion = unansweredQuestions[index];

		questionText.text = "Question " + currentQuestionNumber + ":\n" + currentQuestion.questionStatement;
		optionOneText.text = currentQuestion.optionOne;
		optionTwoText.text = currentQuestion.optionTwo;
		optionThreeText.text = currentQuestion.optionThree;
		optionFourText.text = currentQuestion.optionFour;

		trueSelectionIndex = currentQuestion.trueSelectionIndex;

	// Debug.Log("Current question has been set: " + currentQuestion.questionStatement + "with index: " + index + "|" + currentQuestionIndex);


}

void loadQuestions() {

	using (IDbConnection dbConnection = new SqliteConnection(connectionString))
		{
			dbConnection.Open();

			using (IDbCommand dbCmd = dbConnection.CreateCommand())
			{
				string sqlQuery = "SELECT * FROM questions";

				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader())
				{
					int debugIndex = 0;

					while(reader.Read()){
						//Get and set the question from the database
						unansweredQuestions.Add( new Question(
							reader.GetInt32(1),
							reader.GetString(0),
							reader.GetString(2),
							reader.GetString(3),
							reader.GetString(4),
							reader.GetString(5),
							reader.GetInt32(6)
						));
						//TODO: Remove debug statements
						// Debug.Log("Question added: " + reader.GetInt32(1));
						// Debug.Log("Question: " + questions[debugIndex].questionStatement);
						debugIndex++;
					}
					// Debug.Log("All questions from db added");
					dbConnection.Close();
					reader.Close();
				}
			}
		}

	}

	//Get the GameObject reference of the selected option
	public GameObject getSelectedOption(int selectedOptionIndex){
		GameObject optionSelected;
		switch(selectedOptionIndex){
			case 0:
			optionSelected = optionOneLayer;
			break;
			case 1:
			optionSelected = optionTwoLayer;
			break;
			case 2:
			optionSelected = optionThreeLayer;
			break;
			case 3:
			optionSelected = optionFourLayer;
			break;
			default:
			optionSelected = optionOneLayer;
			break;
		}

		return optionSelected;
	}

	IEnumerator NextQuestionTransition(){

		yield return new WaitForSeconds(transitionDelay);

		questionsAnswered++;

		//Reset colours of options
		optionOneLayer.GetComponent<Image>().color = Color.white;
		optionTwoLayer.GetComponent<Image>().color = Color.white;
		optionThreeLayer.GetComponent<Image>().color = Color.white;
		optionFourLayer.GetComponent<Image>().color = Color.white;

		//Remove the answered question from the List so it isn't repeated
		// unansweredQuestions.RemoveAt(currentQuestionIndex);

		if(questionsAnswered < 10){

			// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			System.Random rnd = new System.Random();
			setCurrentQuestion(rnd.Next(0, unansweredQuestions.Count));

		} else {
			SceneManager.LoadScene(2);
		}

	}

	public void onOptionOneClick(){
		selectedOption = 0;
		checkOption();
	}
	public void onOptionTwoClick(){
		selectedOption = 1;
		checkOption();
	}
	public void onOptionThreeClick(){
		selectedOption = 2;
		checkOption();
	}
	public void onOptionFourClick(){
		selectedOption = 3;
		checkOption();
	}

	//Check if the selected option is true
	public void checkOption(){
		if(selectedOption == trueSelectionIndex){
			//Correct option is chosen so add points
			// Debug.Log("CORRECT");
			//Increment to score
			sessionScore++;
			Debug.Log("SCORE: " + sessionScore);
		}
		else{
			//Wrong option is chosen so change color of option to red
			// Debug.Log("WRONG");
			
			getSelectedOption(selectedOption).GetComponent<Image>().color = Color.red;
			// Debug.Log(selectedOption + " is wrong");
			
		}
		//Set the color of the correct option to green
		getSelectedOption(trueSelectionIndex).GetComponent<Image>().color = Color.green;

		//Load the next question
		StartCoroutine(NextQuestionTransition());
	}

	public int getSessionScore(){
		return sessionScore;
	}
}
