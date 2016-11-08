using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageInstantiate : MonoBehaviour {

    public Material[] tireColor;

    public GameObject prefab;
    public GameObject content;

    public GameObject LineCanvas;
    public GameObject[] Lines;

    Vector3 startPos;
    Vector3 lineStartPos;

    bool PrefabOn = false;

    Vector2[] contentOriginalSize = new Vector2[2];

    public AudioClip buttonSound;

    public GameObject sceneTransition;
    public GameObject preGameController;

    public GameObject[] linePos;
    private float lineDist1 = 0;
    private float lineDist2 = 0;

    private int offsetFromBottomOfPanel = 250;

    private bool isNextStage = false;
    private int nextStage = 0;

    public void InstantiatePrefab()
    {
        PrefabOn = true;

        // Number of stages to for loop create prefab
        int number = GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].gettotalNoStage_Cat();
        /*
        if(GameControl.handle.player.SettingsOrientation_Player == 1) // rotate the content first before instantiating , then rotate back to fit landscape
        {
            content.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        */
        contentOriginalSize[0] = content.GetComponent<RectTransform>().offsetMin;
        contentOriginalSize[1] = content.GetComponent<RectTransform>().offsetMax;

        // extend canvas so can do scrolling if it exceeds the size. Hard coded amount 
        content.GetComponent<RectTransform>().offsetMax = new Vector2(content.GetComponent<RectTransform>().offsetMax.x, content.GetComponent<RectTransform>().offsetMax.y - number * -160);

        
            startPos = new Vector3(0, -(content.GetComponent<RectTransform>().offsetMax.y - content.GetComponent<RectTransform>().offsetMin.y) / 2 - offsetFromBottomOfPanel, 0);

        lineStartPos = startPos;
        lineStartPos.x = -50;

        if (GameControl.handle.player.SettingsOrientation_Player == 1) // If landscape , start at diff pos
        {
            content.GetComponent<RectTransform>().offsetMax = new Vector2(content.GetComponent<RectTransform>().offsetMax.x, content.GetComponent<RectTransform>().offsetMax.y - number* 10);
            lineStartPos = new Vector3(0, (content.GetComponent<RectTransform>().offsetMax.y - content.GetComponent<RectTransform>().offsetMin.y) / 2 -270, 0);
            startPos = new Vector3(0, (content.GetComponent<RectTransform>().offsetMax.y - content.GetComponent<RectTransform>().offsetMin.y) / 2 -120, 0);
            //lineStartPos.y = -(content.GetComponent<RectTransform>().offsetMax.y - content.GetComponent<RectTransform>().offsetMin.y) / 2 - 342;
            
            Debug.Log("startpos:" + startPos);
        }

        lineDist1 = linePos[0].transform.localPosition.y - linePos[1].transform.localPosition.y; // distance between first and 2nd
        lineDist2 = linePos[1].transform.localPosition.y - linePos[2].transform.localPosition.y; // distance between 3rd and 2nd , but dist between first and 3rd is fix at 320 , the same scaling as canvas

        

        // Start to instantiate
        // For each stage
        for (int i = 0; i < number; i++)
        {
            // Instantiating the prefab
            GameObject clone = Instantiate<GameObject>(prefab);
         //   Debug.Log(i + " " + clone.transform.rotation.z);
            // assigning a name
            clone.name = "Stage " + (i + 1) + " Panel";

            // set it to be under the canvas
            clone.transform.SetParent(this.transform, true);
            clone.transform.rotation = Quaternion.Euler(0, 0, 0);
             if (GameControl.handle.player.SettingsOrientation_Player == 1) // change the rotation back to the landscape de 
             {
              clone.transform.rotation = Quaternion.Euler(0, 0, 270);
             }
            
            // set it to 1,1,1 if not it will keep its scale
            clone.transform.localScale = new Vector3(1, 1, 1);

            // set the X to have alternate patterns , for asthetics only
    
            if (i % 2 != 0)
            {
                clone.transform.localPosition = new Vector3(-120, startPos.y, startPos.z);
            }
            else
            {
                clone.transform.localPosition = new Vector3(120, startPos.y, startPos.z);
            }
         
            if (GameControl.handle.player.SettingsOrientation_Player == 1) // change the rotation back to the landscape de 
            {
                startPos.y -= 160;
            }
            else
            // Increasing Y 
            startPos.y += 160;
            
            //Check if stage is done
            if (GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].Stages_Cat[i].checkIfLevelIsDone())
            {
                GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].Stages_Cat[i].isDone = true;
            }

            //Check if i + 1 is the next playable stage
            if (i + 1 < number)         //Safety net, "i" might go over "number"
            {
                //If current stage + 1 is not done and if current stage is done
                if (!GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].Stages_Cat[i + 1].isDone && 
                    GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].Stages_Cat[i].isDone)
                {
                    GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].Stages_Cat[i + 1].isNextStage = true;
                }
            }

            //If current stage isnt done, and the current stage isnt the next playable stage, and if it isnt the first stage, set buttons to inactive
            if (!GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].Stages_Cat[i].isDone && 
                !GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].Stages_Cat[i].isNextStage &&
                i > 0)
            {
                    clone.GetComponent<CanvasGroup>().interactable = false;
                    clone.GetComponent<CanvasGroup>().alpha = 0.7f;                    
            }

            // Checking components and changing according to the specific stages
            foreach (Transform child in clone.GetComponentInChildren<Transform>())
            {
                float correctcount = GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].Stages_Cat[i].getCorrectCount_Stage();

                if (child.tag == "NameButton") // Button
                {
                    // Name the button will show
                    child.GetComponentInChildren<Text>().text = "Stage " + (i + 1).ToString();

                    // it will receive elements from previous loop , so declare new variable to pass in .
                    int stageNo = i;
                    child.GetComponent<Button>().onClick.AddListener(() => { pressStage(stageNo); });

                    // adding sound to the button
                    child.gameObject.AddComponent<ButtonSound>();
                    child.gameObject.GetComponent<ButtonSound>().sound = buttonSound;

                    child.GetComponent<Button>().onClick.AddListener(() => { sceneTransition.GetComponent<AnimToGameplay>().startToGoGameplay(); });
                    child.GetComponent<Button>().onClick.AddListener(() => { preGameController.GetComponent<MainMenuScreenController>().MainBorderDisappear(); });
                }
                if (child.tag == "Obj") // Tire
                {
                    // should be wrong , but temporary one
                    int stars = GameControl.handle.getMode().Categories[GameControl.handle.currentCategory].Stages_Cat[i].getStars();
                    child.GetComponent<RawImage>().material = tireColor[stars];
                }
                if (child.tag == "Score") // Score
                {
                    child.GetComponent<Text>().text = correctcount + "/ 10";
                }
                if (child.tag == "Percentage")
                {
                    foreach (Transform children in child.GetComponentInChildren<Transform>())
                    {
                        if (children.tag == "Meter")
                        {
                            float angle = 190 - correctcount * 25;
                            children.transform.localRotation = Quaternion.Euler(0, 0, angle);
                        }
                    }
                }
            }


            // Same thing but for lines
            if (i > 0)
            {
                // 2 diff type of lines , alternate like the pattern
                GameObject line = Instantiate<GameObject>(Lines[(i - 1) % 2]);

                // set it to be under the canvas and fix scaling
                line.transform.SetParent(LineCanvas.transform, true);
                line.transform.localScale = new Vector3(1, 1, 1);
                line.name = "line" + i;
                // set the Y position and the X 
                if (GameControl.handle.player.SettingsOrientation_Player == 1) // change the rotation back to the landscape de 
                {
                    if ((i - 1) % 2 != 0)
                    {
                        line.transform.localPosition = new Vector3(5, lineStartPos.y + 120, lineStartPos.z);//line for bottom left to top right
                        lineStartPos.y -= 160;//the distance between each line for the first to second

                    }
                    else
                    {
                        line.transform.localPosition = new Vector3(linePos[0].transform.localPosition.x + 20, lineStartPos.y + 120, lineStartPos.z);//line for bottom right to top left
                        lineStartPos.y -= 160;//the distance between each line for the second to third
                    }
                }
                else
                {
                    if ((i - 1) % 2 != 0)
                    {
                        line.transform.localPosition = new Vector3(5, lineStartPos.y + 120, lineStartPos.z);//line for bottom left to top right
                        lineStartPos.y -= -160;//the distance between each line for the first to second

                    }
                    else
                    {
                        line.transform.localPosition = new Vector3(linePos[0].transform.localPosition.x + 20, lineStartPos.y + 120, lineStartPos.z);//line for bottom right to top left
                        lineStartPos.y -= -160;//the distance between each line for the second to third
                    }
                }
            }
           
        }
        /*
        if (GameControl.handle.player.SettingsOrientation_Player == 1) // change the rotation back to the landscape de 
        {
            content.transform.rotation = Quaternion.Euler(0, 0, 180);
        }*/

    }
    
    public void RemovePrefab()
    {
        if (PrefabOn)
        {
            content.GetComponent<RectTransform>().offsetMin = contentOriginalSize[0];
            content.GetComponent<RectTransform>().offsetMax = contentOriginalSize[1];

            foreach (Transform child in this.GetComponentInChildren<Transform>())
            {
                Destroy(child.gameObject);
            }

            foreach (Transform child in LineCanvas.GetComponentInChildren<Transform>())
            {
                if(child.gameObject.activeInHierarchy == true)
                    Destroy(child.gameObject);
            }
        }
    }

	void pressStage(int stageNo)
    {
        Selection.handle.PressStage(stageNo);
    }

}
