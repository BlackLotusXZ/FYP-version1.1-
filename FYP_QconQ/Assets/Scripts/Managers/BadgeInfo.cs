using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BadgeInfo : MonoBehaviour {

    public enum BadgeType
    {
        ArcadeAttempt,
        SpeedRun,
        ConsecutiveCorrects,
        TotalPlayTime,
        ArcadeScore,
        ShopPercent,
        StageAllCorrect,
        CumulativeScore
    }

    

    //This int specifies which badge it is. E.G: If you set this value to 1 in the editor, it will get the info for badge number 1
    public BadgeType Type;
    //Handle to the stars representing the tiers
    public Image[] stars;
    //Badges' description text UI component
    private Text[] badgeDescription;
    //Specifies if you've attained a certain tier level. 
    private bool[] achievedLevel;
    //Constant value for the badges' message if you've maxed out the badge
    private const string MAX_BADGE_TIER_MESSAGE = "You've finished everything!";

    //This section is where you set the limits/tiers of each badge
    #region Badge tier limits

    //Badge 1
    private const int ARCADEATTEMPTS_EASY = 10;
    private const int ARCADEATTEMPTS_MED = 20;
    private const int ARCADEATTEMPTS_HIGH = 30;

    //Badge 3
    private const int SPEEDRUN_EASY = 40;
    private const int SPEEDRUN_MED = 30;
    private const int SPEEDRUN_HIGH = 20;

    //Badge 4
    private const int CORRECTSINAROW_EASY = 2;
    private const int CORRECTSINAROW_MED = 4;
    private const int CORRECTSINAROW_HIGH = 6;

    //Badge 5
    private const int TOTALPLAYTIME_EASY = 600;         //10 MINS
    private const int TOTALPLAYTIME_MED = 1200;         //20 MINS
    private const int TOTALPLAYTIME_HIGH = 1800;        //30 MINS

    //Badge 7
    private const int ARCADESCORE_EASY = 20;
    private const int ARCADESCORE_MED = 30;
    private const int ARCADESCORE_HIGH = 40;

    //Badge 8
    private const float SHOPPERCENT_EASY = 30.0f;
    private const float SHOPPERCENT_MED = 50.0f;
    private const float SHOPPERCENT_HIGH = 100.0f;

    //Badge 9
    private const int STAGEALLCORRECT_EASY = 1;
    private const int STAGEALLCORRECT_MED = 2;
    private const int STAGEALLCORRECT_HIGH = 3;

    //Badge 10
    private const int CUMULATIVESCORE_EASY = 100;
    private const int CUMULATIVESCORE_MED = 200;
    private const int CUMULATIVESCORE_HIGH = 300;

    #endregion

    // Use this for initialization
    void Start()
    {
        achievedLevel = new bool[3];
        for (int i = 0; i < achievedLevel.Length; ++i)
            achievedLevel[i] = false;

        badgeDescription = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UITextHandler();
        checkAchievedLevel();
        StarHandler();
    }

    public int getCurrentTier()
    {
        for (int i = 0; i < achievedLevel.Length; ++i)
        {
            if (achievedLevel[i] == true)
            {
                return i;
            }
        }
        return 0;
    }

    //This is where the badge text is handled
    void UITextHandler()
    {
        switch (Type)
        {
            //DONE
            case BadgeType.ArcadeAttempt:
                #region No. of attempts for arcade (10/20/30)
                badgeDescription[0].text = "Play arcade mode for 'x' times \n";

                if (GameControl.handle.player.arcademodeAttempts < ARCADEATTEMPTS_EASY)
                    badgeDescription[1].text = "Next goal: 10 times";

                else if (GameControl.handle.player.arcademodeAttempts >= ARCADEATTEMPTS_EASY && GameControl.handle.player.arcademodeAttempts < ARCADEATTEMPTS_MED)
                    badgeDescription[1].text = "Next goal: 20 times";

                else if (GameControl.handle.player.arcademodeAttempts >= ARCADEATTEMPTS_MED && GameControl.handle.player.arcademodeAttempts < ARCADEATTEMPTS_HIGH)
                    badgeDescription[1].text = "Next goal: 30 times";

                else
                    badgeDescription[1].text = MAX_BADGE_TIER_MESSAGE;

                break;
                #endregion

            //case 2:
            //    #region completing a level in a specific environment(5 times/10 times/15 times)
            //    badgeDescription[0].text = "Complete a stage in the same environment for 'x' times";
            //    badgeDescription[1].text = "Next level: 5 times";
            //    break;
            //    #endregion

            //DONE
            case BadgeType.SpeedRun:
                #region finish stage within specific time with at least 70% efficiency (40s, 30s, 20s)
                badgeDescription[0].text = "Complete a level within 'x' seconds with at least 70% efficiency";
                if (GameControl.handle.player.speedrunTimer > SPEEDRUN_EASY || GameControl.handle.player.speedrunTimer <= 0)
                    badgeDescription[1].text = "Next level: 40 seconds";
                else if (GameControl.handle.player.speedrunTimer > SPEEDRUN_MED && GameControl.handle.player.speedrunTimer <= SPEEDRUN_EASY)
                    badgeDescription[1].text = "Next level: 30 seconds";
                else if (GameControl.handle.player.speedrunTimer > SPEEDRUN_HIGH && GameControl.handle.player.speedrunTimer <= SPEEDRUN_MED)
                    badgeDescription[1].text = "Next level: 20 seconds";

                else
                    badgeDescription[1].text = MAX_BADGE_TIER_MESSAGE;
                break;
                #endregion

            //DONE
            case BadgeType.ConsecutiveCorrects:
                #region Corrects in a row (E:2, M:4, H:6)
                badgeDescription[0].text = "Get 'x' corrects in a row \n";
                if (GameControl.handle.player.correctsInARow < CORRECTSINAROW_EASY)
                    badgeDescription[1].text = "Next level: 2";

                else if (GameControl.handle.player.correctsInARow < CORRECTSINAROW_MED && GameControl.handle.player.correctsInARow >= CORRECTSINAROW_EASY)
                    badgeDescription[1].text = "Next level: 4";

                else if (GameControl.handle.player.correctsInARow < CORRECTSINAROW_HIGH && GameControl.handle.player.correctsInARow >= CORRECTSINAROW_MED)
                    badgeDescription[1].text = "Next level: 6";

                else
                    badgeDescription[1].text = MAX_BADGE_TIER_MESSAGE;
                break;
                #endregion

            //DONE
            case BadgeType.TotalPlayTime:
                #region Total Playtime > a certain number (E: > 10mins/M: > 20mins/H: > 30mins)
                badgeDescription[0].text = "Play the game for more than 'x' minutes";
                if (GameControl.handle.player.totalPlaytime < TOTALPLAYTIME_EASY)
                    badgeDescription[1].text = "Next level: 10 mins";
                else if (GameControl.handle.player.totalPlaytime < TOTALPLAYTIME_MED && GameControl.handle.player.totalPlaytime >= TOTALPLAYTIME_EASY)
                    badgeDescription[1].text = "Next level: 20 mins";
                else if (GameControl.handle.player.totalPlaytime < TOTALPLAYTIME_HIGH && GameControl.handle.player.totalPlaytime >= TOTALPLAYTIME_MED)
                    badgeDescription[1].text = "Next level: 30 mins";

                else
                    badgeDescription[1].text = MAX_BADGE_TIER_MESSAGE;
                break;
                #endregion
            //case 5:
            //    #region % of questions attempted in all stages (story) (25%, 50%, 100%)
            //    for (int i = 0; i < GameControl.handle.Modes[0].Categories.Length; i++)
            //    {
            //        //Debug.Log(GameControl.handle.Modes[0].Categories[i].gettotalAttempted_Cat());
            //    }

            //    badgeDescription[0].text = "Attain 'x' % of all questions \n answered in story mode";
            //    badgeDescription[1].text = "Next level: 10% answered";
            //    break;
            //    #endregion

            //DONE
            case BadgeType.ArcadeScore:
                #region total score for 1 round of arcade (20, 30, 40)
                badgeDescription[0].text = "Score 'x' in 1 round of arcade \n";
                if (GameControl.handle.player.arcadeScore < ARCADESCORE_EASY)
                    badgeDescription[1].text = "Next level: 20";
                else if (GameControl.handle.player.arcadeScore < ARCADESCORE_MED && GameControl.handle.player.arcadeScore >= ARCADESCORE_EASY)
                    badgeDescription[1].text = "Next level: 30";
                else if (GameControl.handle.player.arcadeScore < ARCADESCORE_HIGH && GameControl.handle.player.arcadeScore >= ARCADESCORE_MED)
                    badgeDescription[1].text = "Next level: 40";
                else
                    badgeDescription[1].text = MAX_BADGE_TIER_MESSAGE;
                break;
                #endregion

            //DONE
            case BadgeType.ShopPercent:
                #region % of items from the shop unlocked (30/50/100)
                badgeDescription[0].text = "Attain 'x' % of all items in the shop unlocked";
                if (GameControl.handle.player.percentOfItemsUnlocked < SHOPPERCENT_EASY)
                    badgeDescription[1].text = "Next level: 30%";
                else if (GameControl.handle.player.percentOfItemsUnlocked < SHOPPERCENT_MED && GameControl.handle.player.percentOfItemsUnlocked >= SHOPPERCENT_EASY)
                    badgeDescription[1].text = "Next level: 50%";
                else if (GameControl.handle.player.percentOfItemsUnlocked < SHOPPERCENT_HIGH && GameControl.handle.player.percentOfItemsUnlocked >= SHOPPERCENT_MED)
                    badgeDescription[1].text = "Next level: 100%";
                else
                    badgeDescription[1].text = MAX_BADGE_TIER_MESSAGE;
                break;
                #endregion

            //DONE
            case BadgeType.StageAllCorrect:
                #region all correct for a stage (1 stage, 2 stages, 3 stages)
                badgeDescription[0].text = "Get all correct for 'x' stages in story mode";
                if (GameControl.handle.player.numberOfStagesAllCorrect < STAGEALLCORRECT_EASY)
                    badgeDescription[1].text = "Next level: 1 stage";

                else if (GameControl.handle.player.numberOfStagesAllCorrect < STAGEALLCORRECT_MED && GameControl.handle.player.numberOfStagesAllCorrect >= STAGEALLCORRECT_EASY)
                    badgeDescription[1].text = "Next level: 2 stages";

                else if (GameControl.handle.player.numberOfStagesAllCorrect < STAGEALLCORRECT_HIGH && GameControl.handle.player.numberOfStagesAllCorrect >= STAGEALLCORRECT_MED)
                    badgeDescription[1].text = "Next level: 3 stages";

                else
                    badgeDescription[1].text = MAX_BADGE_TIER_MESSAGE;
                break;
                #endregion

            //DONE
            case BadgeType.CumulativeScore:
                #region Total Score for story (100/200/300)
                badgeDescription[0].text = "Get a total score of 'x' for story \n mode";
                if (GameControl.handle.player.cumulativeScore < CUMULATIVESCORE_EASY)
                    badgeDescription[1].text = "Next goal: 100 points";

                else if (GameControl.handle.player.cumulativeScore >= CUMULATIVESCORE_EASY && GameControl.handle.player.cumulativeScore < CUMULATIVESCORE_MED)
                    badgeDescription[1].text = "Next goal: 200 points";

                else if (GameControl.handle.player.cumulativeScore >= CUMULATIVESCORE_MED && GameControl.handle.player.cumulativeScore < CUMULATIVESCORE_HIGH)
                    badgeDescription[1].text = "Next goal: 300 points";

                else
                    badgeDescription[1].text = MAX_BADGE_TIER_MESSAGE;
                break;
                #endregion

        }
    }

    //This is where the badge stars are handled
    void StarHandler()
    {
        switch (Type)
        {
            //DONE
            case BadgeType.ArcadeAttempt:
                #region No. of attempts for arcade (10/20/30)
                if (GameControl.handle.player.arcademodeAttempts >= ARCADEATTEMPTS_EASY && GameControl.handle.player.arcademodeAttempts < ARCADEATTEMPTS_MED)
                    achievedLevel[0] = true;

                else if (GameControl.handle.player.arcademodeAttempts >= ARCADEATTEMPTS_MED && GameControl.handle.player.arcademodeAttempts < ARCADEATTEMPTS_HIGH)
                    achievedLevel[1] = true;

                else if (GameControl.handle.player.arcademodeAttempts >= ARCADEATTEMPTS_HIGH)
                    achievedLevel[2] = true;
                break;
                #endregion


            //case 2:
            //    #region completing a level in a specific environment(5 times/10 times/15 times)
            //    break;
            //    #endregion

            //DONE
            case BadgeType.ArcadeScore:
                #region finish stage within specific time with at least 70% efficiency (40s, 30s, 20s)
                if (GameControl.handle.player.speedrunTimer <= SPEEDRUN_EASY && GameControl.handle.player.speedrunTimer > SPEEDRUN_MED)
                    achievedLevel[0] = true;
                else if (GameControl.handle.player.speedrunTimer <= SPEEDRUN_MED && GameControl.handle.player.speedrunTimer > SPEEDRUN_HIGH)
                    achievedLevel[1] = true;
                else if (GameControl.handle.player.speedrunTimer <= SPEEDRUN_HIGH && GameControl.handle.player.speedrunTimer > 0)
                    achievedLevel[2] = true;
                break;
                #endregion

            //DONE
            case BadgeType.ConsecutiveCorrects:
                #region Corrects in a row (E:2, M:4, H:6)
                if (GameControl.handle.player.correctsInARow >= CORRECTSINAROW_EASY && GameControl.handle.player.correctsInARow < CORRECTSINAROW_MED)
                    achievedLevel[0] = true;

                else if (GameControl.handle.player.correctsInARow >= CORRECTSINAROW_MED && GameControl.handle.player.correctsInARow < CORRECTSINAROW_HIGH)
                    achievedLevel[1] = true;

                else if (GameControl.handle.player.correctsInARow >= CORRECTSINAROW_HIGH)
                    achievedLevel[2] = true;
                break;
                #endregion

            //DONE
            case BadgeType.CumulativeScore:
                #region Total Playtime > a certain number (E: > 10mins/M: > 20mins/H: > 30mins)
                if (GameControl.handle.player.totalPlaytime >= TOTALPLAYTIME_EASY && GameControl.handle.player.totalPlaytime < TOTALPLAYTIME_MED)
                    achievedLevel[0] = true;
                else if (GameControl.handle.player.totalPlaytime >= TOTALPLAYTIME_MED && GameControl.handle.player.totalPlaytime < TOTALPLAYTIME_HIGH)
                    achievedLevel[1] = true;
                else if (GameControl.handle.player.totalPlaytime >= TOTALPLAYTIME_HIGH)
                    achievedLevel[2] = true;
                break;
                #endregion


            //case 6:
            //    #region % of questions attempted in all stages (story) (25%, 50%, 100%)
            //    break;
            //    #endregion

            //DONE
            case BadgeType.ShopPercent:
                #region total score for 1 round of arcade (20, 30, 40)
                if (GameControl.handle.player.arcadeScore >= ARCADESCORE_EASY && GameControl.handle.player.arcadeScore < ARCADESCORE_MED)
                    achievedLevel[0] = true;
                else if (GameControl.handle.player.arcadeScore >= ARCADESCORE_MED && GameControl.handle.player.arcadeScore < ARCADESCORE_HIGH)
                    achievedLevel[1] = true;
                else if (GameControl.handle.player.arcadeScore >= ARCADESCORE_HIGH)
                    achievedLevel[2] = true;
                break;
                #endregion

            //DONE
            case BadgeType.SpeedRun:
                #region % of items from the shop unlocked (30/50/100)
                if (GameControl.handle.player.percentOfItemsUnlocked >= SHOPPERCENT_EASY && GameControl.handle.player.percentOfItemsUnlocked < SHOPPERCENT_MED)
                    achievedLevel[0] = true;
                else if (GameControl.handle.player.percentOfItemsUnlocked >= SHOPPERCENT_MED && GameControl.handle.player.percentOfItemsUnlocked < SHOPPERCENT_HIGH)
                    achievedLevel[1] = true;
                else if (GameControl.handle.player.percentOfItemsUnlocked >= SHOPPERCENT_HIGH)
                    achievedLevel[2] = true;
                break;
                #endregion

            //DONE
            case BadgeType.StageAllCorrect:
                #region all correct for a stage (1 stage, 2 stages, 3 stages)
                if (GameControl.handle.player.numberOfStagesAllCorrect >= STAGEALLCORRECT_EASY && GameControl.handle.player.numberOfStagesAllCorrect < STAGEALLCORRECT_MED)
                    achievedLevel[0] = true;

                else if (GameControl.handle.player.numberOfStagesAllCorrect >= STAGEALLCORRECT_MED && GameControl.handle.player.numberOfStagesAllCorrect < STAGEALLCORRECT_HIGH)
                    achievedLevel[1] = true;

                else if (GameControl.handle.player.numberOfStagesAllCorrect >= STAGEALLCORRECT_HIGH)
                    achievedLevel[2] = true;
                break;
                #endregion

            //DONE
            case BadgeType.TotalPlayTime:
                #region Total Score for story (100/200/300)
                if (GameControl.handle.player.cumulativeScore >= CUMULATIVESCORE_EASY && GameControl.handle.player.cumulativeScore < CUMULATIVESCORE_MED)
                    achievedLevel[0] = true;

                else if (GameControl.handle.player.cumulativeScore >= CUMULATIVESCORE_MED && GameControl.handle.player.cumulativeScore < CUMULATIVESCORE_HIGH)
                    achievedLevel[1] = true;

                else if (GameControl.handle.player.cumulativeScore >= CUMULATIVESCORE_HIGH)
                    achievedLevel[2] = true;

                break;
                #endregion
        }
    }

    //This function checks how many stars to display
    void checkAchievedLevel()
    {
        //If achieved 3rd tier, show all stars
        if (achievedLevel[2])
        {
            if (!stars[0].gameObject.activeInHierarchy)
                stars[0].gameObject.SetActive(true);

            if (!stars[1].gameObject.activeInHierarchy)
                stars[1].gameObject.SetActive(true);
            if (!stars[2].gameObject.activeInHierarchy)
                stars[2].gameObject.SetActive(true);
        }

        //else if achieved 2nd tier, show 2 stars
        else if (achievedLevel[1])
        {
            if (!stars[0].gameObject.activeInHierarchy)
                stars[0].gameObject.SetActive(true);
            if (!stars[1].gameObject.activeInHierarchy)
                stars[1].gameObject.SetActive(true);
        }

        //else if achieved 1st tier, show 1 star
        else if (achievedLevel[0])
        {
            if (!stars[0].gameObject.activeInHierarchy)
                stars[0].gameObject.SetActive(true);
        }

        //else set all inactive
        else
        {
            for (int i = 0; i < stars.Length; ++i)
                stars[i].gameObject.SetActive(false);
        }

    }

    //This function is a function to call if you want to reset the badges' data
    void DeleteBadgesData()
    {
        //1
        GameControl.handle.player.arcademodeAttempts = 0;

        //3
        GameControl.handle.player.speedrunTimer = -1;
        //4
        GameControl.handle.player.correctsInARow = 0;
        //5
        GameControl.handle.player.totalPlaytime = 0;


        //7
        GameControl.handle.player.arcadeScore = 0;
        //7
        GameControl.handle.player.percentOfItemsUnlocked = 0;
        //9
        GameControl.handle.player.numberOfStagesAllCorrect = 0;
        //10
        GameControl.handle.player.cumulativeScore = 0;
    }
}
