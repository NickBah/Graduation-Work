using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MM_Controller : MonoBehaviour
{
	static MM_Controller Instance = null;
	
	public GameObject ButtonPhonemic;
	public GameObject ButtonGrammar;
	public GameObject ButtonDictionarySurvey;
	
	public GameObject ButtonPhonemicPerception;
	public GameObject ButtonPhonemicAnalysis;
	public GameObject ButtonPhonemicSynthesis;
	public GameObject ButtonPhonemicRepresentation;	
	
	public GameObject ButtonDictionarySurveyNouns;	
	public GameObject ButtonDictionarySurveyAdjectives;
	public GameObject ButtonDictionarySurveyNumerals;
	
	public GameObject ButtonPhonemicAnalysisTask57;
	public GameObject ButtonPhonemicAnalysisTask11;
	public GameObject ButtonPhonemicAnalysisTask12;
	
	public GameObject ButtonPhonemicRepresentationTask21;
	
	public GameObject ButtonPhonemicSynthesisTask17;
	
	public GameObject ButtonPhonemicPerceptionTask1;
	
	public GameObject Screens;
	public GameObject MainScreen;
	public GameObject PhonemicScreen;
	public GameObject GrammarScreen;
	public GameObject DictionarySurveyScreen;
	public GameObject LoadingScreen;

	public GameObject ButtonGrammarTask1;

	public GameObject PhonemicPerceptionScreen;
	public GameObject PhonemicAnalysisScreen;
	public GameObject PhonemicSynthesisScreen;	
	public GameObject PhonemicRepresentationScreen;	
	
	public GameObject DictionarySurveyNounsScreen;
	public GameObject DictionarySurveyAdjectivesScreen;
	public GameObject DictionarySurveyNumeralsScreen;
	
	public GameObject ButtonDictionarySurveyNounsTask1;
	public GameObject ButtonDictionarySurveyNounsTask2;
	public GameObject ButtonDictionarySurveyNounsTask3;
	
	public GameObject ButtonDictionarySurveyAdjectivesTask1;
	public GameObject ButtonDictionarySurveyAdjectivesTask2;
	
	public GameObject ButtonDictionarySurveyNumeralsTask1;
	
	public Button ButtonBack;
	
	bool BackBtnEnable = true;
	
	string state = "Main";
		
	float LoadingProgress = 0.0f;
	
	MM_LoadingController loading;
	
	public AsyncOperation asyncLoad;
	
	public float GetLoadingProgress(){
		return LoadingProgress;
	}
		
	void Awake(){
		if (Instance == null){
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}
		
	void Start(){
		loading = transform.GetChild(0).GetComponent<MM_LoadingController>();
		
		ButtonPhonemic.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemic.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicClicked());

		ButtonGrammar.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonGrammar.GetComponent<Button>().onClick.AddListener(() => ButtonGrammarClicked());

		ButtonDictionarySurvey.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurvey.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyClicked());

		ButtonBack.onClick.RemoveAllListeners();
		ButtonBack.onClick.AddListener(() => ButtonBackClicked());

		ButtonPhonemicPerception.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicPerception.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicPerceptionClicked());	

		ButtonPhonemicAnalysis.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicAnalysis.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicAnalysisClicked());	

		ButtonPhonemicSynthesis.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicSynthesis.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicSynthesisClicked());	

		ButtonPhonemicRepresentation.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicRepresentation.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicRepresentationClicked());	

		ButtonDictionarySurveyNouns.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurveyNouns.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyNounsClicked());	

		ButtonDictionarySurveyAdjectives.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurveyAdjectives.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyAdjectivesClicked());	
	
		ButtonDictionarySurveyNumerals.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurveyNumerals.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyNumeralsClicked());	
		
		ButtonPhonemicAnalysisTask57.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicAnalysisTask57.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicAnalysisTask57Clicked());

		ButtonPhonemicAnalysisTask11.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicAnalysisTask11.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicAnalysisTask11Clicked());		
	
		ButtonPhonemicAnalysisTask12.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicAnalysisTask12.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicAnalysisTask12Clicked());		
		
		ButtonPhonemicRepresentationTask21.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicRepresentationTask21.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicRepresentationTask21Clicked());

		ButtonPhonemicSynthesisTask17.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicSynthesisTask17.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicSynthesisTask17Clicked());
		
		ButtonPhonemicPerceptionTask1.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonPhonemicPerceptionTask1.GetComponent<Button>().onClick.AddListener(() => ButtonPhonemicPerceptionTask1Clicked());		

		ButtonDictionarySurveyNounsTask1.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurveyNounsTask1.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyNounsTask1Clicked());

		ButtonDictionarySurveyNounsTask2.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurveyNounsTask2.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyNounsTask2Clicked());		
				
		ButtonDictionarySurveyNounsTask3.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurveyNounsTask3.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyNounsTask3Clicked());
		
		ButtonDictionarySurveyAdjectivesTask1.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurveyAdjectivesTask1.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyAdjectivesTask1Clicked());

		ButtonDictionarySurveyAdjectivesTask2.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurveyAdjectivesTask2.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyAdjectivesTask2Clicked());
				
		ButtonDictionarySurveyNumeralsTask1.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonDictionarySurveyNumeralsTask1.GetComponent<Button>().onClick.AddListener(() => ButtonDictionarySurveyNumeralsTask1Clicked());
				
		ButtonGrammarTask1.GetComponent<Button>().onClick.RemoveAllListeners();
		ButtonGrammarTask1.GetComponent<Button>().onClick.AddListener(() => ButtonGrammarTask1Clicked());				
				
	}
	
	void ButtonBackClicked(){
		BackBtnEnable = false;		
		if (state == "GrammarScreen"){
			MainScreen.SetActive(true);
			GrammarScreen.SetActive(false);	
			state = "Main";
			ButtonBack.gameObject.SetActive(false);
			BackBtnEnable = true;
			return;
		}
		if (state == "PhonemicScreen"){
			MainScreen.SetActive(true);
			PhonemicScreen.SetActive(false);	
			state = "Main";
			ButtonBack.gameObject.SetActive(false);
			BackBtnEnable = true;
			return;
		}
		if (state == "DictionarySurveyScreen"){
			MainScreen.SetActive(true);
			DictionarySurveyScreen.SetActive(false);	
			state = "Main";
			ButtonBack.gameObject.SetActive(false);
			BackBtnEnable = true;
			return;
		}
		if (state == "PhonemicPerceptionExercises"){
			state = "PhonemicScreen";
			PhonemicScreen.SetActive(true);
			PhonemicPerceptionScreen.SetActive(false);
			BackBtnEnable = true;
			return;
		}
		if (state == "PhonemicAnalysisExercises"){
			state = "PhonemicScreen";
			PhonemicScreen.SetActive(true);
			PhonemicAnalysisScreen.SetActive(false);
			BackBtnEnable = true;
			return;
		}	
		if (state == "PhonemicSynthesisExercises"){
			state = "PhonemicScreen";
			PhonemicScreen.SetActive(true);
			PhonemicSynthesisScreen.SetActive(false);	
			BackBtnEnable = true;	
			return;
		}
		if (state == "PhonemicRepresentationExercises"){
			state = "PhonemicScreen";
			PhonemicScreen.SetActive(true);
			PhonemicRepresentationScreen.SetActive(false);
			BackBtnEnable = true;	
			return;
		}
		if (state == "DictionarySurveyNounsExercises"){
			state = "DictionarySurveyScreen";
			DictionarySurveyScreen.SetActive(true);
			DictionarySurveyNounsScreen.SetActive(false);
			BackBtnEnable = true;	
			return;
		}
		if (state == "DictionarySurveyAdjectivesExercises"){
			state = "DictionarySurveyScreen";
			DictionarySurveyScreen.SetActive(true);
			DictionarySurveyAdjectivesScreen.SetActive(false);
			BackBtnEnable = true;	
			return;
		}
		if (state == "DictionarySurveyNumeralsExercises"){
			state = "DictionarySurveyScreen";
			DictionarySurveyScreen.SetActive(true);
			DictionarySurveyNumeralsScreen.SetActive(false);
			BackBtnEnable = true;	
			return;
		}		
		
	}	
	
	IEnumerator AsyncLoading(string pSceneName){
		
		asyncLoad = SceneManager.LoadSceneAsync(pSceneName);
		
		asyncLoad.allowSceneActivation = false;
		
		while (!asyncLoad.isDone)
        {
			LoadingProgress = asyncLoad.progress;
            yield return null;
        }
		
		LoadingScreen.SetActive(false);	
		
		yield break;
	}
	
	void Load(string pSceneName){
		
		LoadingScreen.SetActive(true);		
		Screens.SetActive(false);
		ButtonBack.gameObject.SetActive(false);
			
		loading.Reset();
		
		LoadingProgress = 0.0f;	
		StartCoroutine(AsyncLoading(pSceneName));
		
		BackBtnEnable = false;
	}
	
	// Главный экран
	void ButtonPhonemicClicked(){
		MainScreen.SetActive(false);
		PhonemicScreen.SetActive(true);
		
		state = "PhonemicScreen";
		
		ButtonBack.gameObject.SetActive(true);
	}
	
	// Грамматика
	void ButtonGrammarClicked(){
		MainScreen.SetActive(false);
		GrammarScreen.SetActive(true);
		
		state = "GrammarScreen";
		ButtonBack.gameObject.SetActive(true);
	}

	void ButtonGrammarTask1Clicked(){
		Load("Scenes/Grammar_task_1");	
	}

	void ButtonDictionarySurveyClicked(){
		MainScreen.SetActive(false);
		DictionarySurveyScreen.SetActive(true);
		
		state = "DictionarySurveyScreen";
		ButtonBack.gameObject.SetActive(true);
	}
	
	// Фонематика
	void ButtonPhonemicPerceptionClicked(){
		PhonemicScreen.SetActive(false);
		PhonemicPerceptionScreen.SetActive(true);
		state = "PhonemicPerceptionExercises";
	}
	
	void ButtonPhonemicAnalysisClicked(){
		PhonemicScreen.SetActive(false);
		PhonemicAnalysisScreen.SetActive(true);
		state = "PhonemicAnalysisExercises";
	}
	
	void ButtonPhonemicSynthesisClicked(){
		PhonemicScreen.SetActive(false);
		PhonemicSynthesisScreen.SetActive(true);
		state = "PhonemicSynthesisExercises";
	}
	
	void ButtonPhonemicRepresentationClicked(){
		PhonemicScreen.SetActive(false);
		PhonemicRepresentationScreen.SetActive(true);
		state = "PhonemicRepresentationExercises";
	}
	
	// Обследование словаря
	void ButtonDictionarySurveyNounsClicked(){
		DictionarySurveyScreen.SetActive(false);
		DictionarySurveyNounsScreen.SetActive(true);
		state = "DictionarySurveyNounsExercises";		
	}

	void ButtonDictionarySurveyAdjectivesClicked(){
		DictionarySurveyScreen.SetActive(false);
		DictionarySurveyAdjectivesScreen.SetActive(true);
		state = "DictionarySurveyAdjectivesExercises";		
	}
	
	void ButtonDictionarySurveyNumeralsClicked(){
		DictionarySurveyScreen.SetActive(false);
		DictionarySurveyNumeralsScreen.SetActive(true);
		state = "DictionarySurveyNumeralsExercises";				
	}
	
	// Задания по теме "Существительные"
	void ButtonDictionarySurveyNounsTask1Clicked(){
		Load("Scenes/DictionarySurvey_task_1");	
	}
	
	void ButtonDictionarySurveyNounsTask2Clicked(){
		Load("Scenes/DictionarySurvey_task_2");	
	}
	
	void ButtonDictionarySurveyNounsTask3Clicked(){
		Load("Scenes/DictionarySurvey_task_3");		
	}
	
	// Задания по теме "Прилагательные"
	void ButtonDictionarySurveyAdjectivesTask1Clicked(){
		Load("Scenes/DictionarySurvey_task_4");		
	}
	
	void ButtonDictionarySurveyAdjectivesTask2Clicked(){
		Load("Scenes/DictionarySurvey_task_5");		
	}
	
	// Задания по теме "Числительные"
	void ButtonDictionarySurveyNumeralsTask1Clicked(){
		Load("Scenes/DictionarySurvey_task_6");		
	}
	
	// Фонематический анализ
	void ButtonPhonemicAnalysisTask57Clicked(){
		Load("Scenes/PhonemicAnalysis");	
	}
	
	void ButtonPhonemicAnalysisTask11Clicked(){
		Load("Scenes/PhonemicAnalysis_task_11");	
	}
	
	void ButtonPhonemicAnalysisTask12Clicked(){
		Load("Scenes/PhonemicAnalysis_task_12");	
	}
	
	// Фонематическое представление
	void ButtonPhonemicRepresentationTask21Clicked(){
		Load("Scenes/PhonemicRepresentation");	
	}
	
	// Фонематический синтез
	void ButtonPhonemicSynthesisTask17Clicked(){
		Load("Scenes/PhonemicSynthesis");	
	}
	
	// Фонематическое восприятие
	void ButtonPhonemicPerceptionTask1Clicked(){
		Load("Scenes/PhonemicPerception");	
	}
	
	public void ReverseLoadingDone(){
		Screens.SetActive(true);			
		ButtonBack.gameObject.SetActive(true);
		LoadingScreen.SetActive(false);
		BackBtnEnable = true;
	}
	
	public static void HS_ExitBtnClicked(){
		
		if (Instance == null){
			GameObject g = GameObject.FindWithTag("MainMenuTag");
			if (g != null){
				MM_Controller menu = g.GetComponent<MM_Controller>();
				menu.LoadingScreen.SetActive(true);
				menu.loading.SetReverseOn();	
			}
		} else {
			Instance.LoadingScreen.SetActive(true);
			Instance.loading.SetReverseOn();		
		}
		SceneManager.LoadScene("Scenes/MainMenu");
	}
	
	void Update(){
		if (Application.platform == RuntimePlatform.Android){
				if (Input.GetKeyDown(KeyCode.Home))
                {
					
                }
				if (Input.GetKeyDown(KeyCode.Menu)){
					
				}
				// Нажатие кнопки "Назад"
				if (Input.GetKeyDown(KeyCode.Escape) && BackBtnEnable == true){
					if (state != "Main"){
						ButtonBackClicked();
					}
				}
        }
	}
}
