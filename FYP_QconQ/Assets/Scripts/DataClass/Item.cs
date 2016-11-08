using UnityEngine;
using System.Collections;


/********************************************/
/**
*  \brief
*   - Item base class
***********************************************/
public class Item : MonoBehaviour
{
    public string item_Name;
    public int item_ID;
    public int item_Price;
    public Sprite item_Icon;

    private ItemState item_State;

    public enum ItemState
    {
        nothing = 0,
        equipped,
        bought
    };

    public int getState()
    {
        return (int)item_State;
    }
    public void setState(int state)
    {
        item_State = (ItemState)state;
    }

}
