using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StageManager : MonoBehaviour {
    
    //Class that contains the scene 
    [System.Serializable]
    public class StageConfig
    {
        public Object scene;
        public string StageLabel;
        public int UnlockWithStars;
        private Button.ButtonClickedEvent onClick;
    }

    //The button to select stage
    public Button PrefabStageButton;
    //Grid to keep all buttons organized on UI
    public Transform Grid;
    //Delay to load the scene
    public float secs;
    //List of Stages
    public List<StageConfig> StageList;

    
    private bool loaded;
    private AsyncOperation loadingStage;
    

    void Start()
    {
         
        fillStageGrid();
    }

    void Update()
    {
        if (loadingStage != null && !loaded)
        {
            if (loadingStage.progress >= 0.9f)
            {
                //if (loadingStage.isDone){
                
                Debug.Log("Activating Scene in " + secs + "seconds");
                Invoke("activateLoadedScene", secs);
                loaded = true;
                //SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene));
            }
            else
            {
                Debug.Log((loadingStage.progress * 100) / 1 + "%");

            }
        }
    }


    //Fill the grid with buttons for each stage
    private void fillStageGrid()
    {
        int count = 1;
        //For each stage on stage list
        foreach (var Stage in StageList)
        {
            Debug.Log("Stage: " + Stage.scene.name);
            //Create a new button object (null)
            Button newButton;

            //check if this object on the list is an actual unityscene or anyother kind of object
            if(Stage.scene.GetType().ToString() != "UnityEditor.SceneAsset"){
                //If it's not an scene object log an error and skip
                 Debug.LogError("Error creating stage button "+Stage.scene.name+", is it scene asset? Skiped... ");
                 continue;
            }
            else
            {
                //If it is an scene
                //cerate a new instace of button prefab
                newButton = Instantiate(PrefabStageButton) as Button;
                //add this new button to the grid
                newButton.transform.SetParent(Grid, false);
                //Check if the label field for this scene is empty
                if(Stage.StageLabel.Length==0){
                    //If empty, use the scene name on the new button
                    newButton.GetComponentInChildren<Text>().text = count.ToString();
                }
                else
                {
                    //If not empty use the filed value on button
                    newButton.GetComponentInChildren<Text>().text = Stage.StageLabel;
                   
                }
                //Use a propertie inside the button object to keep the scene name to load
                newButton.GetComponent<ButtonConfig>().STAGENAME = Stage.scene.name;
                newButton.GetComponent<ButtonConfig>().UNLOCKWITHSTARS = Stage.UnlockWithStars;
               
                //Add a listner to load the scene
                newButton.onClick.AddListener(() => loadLevel(newButton.GetComponent<ButtonConfig>()));
                
            }

            count++;
        }
    }

    //Check if the player have enough start and load the scene
    void loadLevel(ButtonConfig stage)
    {
        if(checkStars()>=stage.UNLOCKWITHSTARS){
            //Some other code
            StartCoroutine("loadSceneBackground", stage.STAGENAME);
        }
        else{
            Debug.Log("Not enough mojo...");

        }
    }

    //Load scene in background
    private void loadSceneBackground(string stage)
    {
        loadingStage = SceneManager.LoadSceneAsync(stage);
        loadingStage.allowSceneActivation = false;  

    }

    //set the loaded scene to active
    private void activateLoadedScene()
    {
        loadingStage.allowSceneActivation = true;
    }


    //Add here a code to check how many stars player alredy have (Playerprefs?)
    private int checkStars()
    {
        return 1;

    }
}
