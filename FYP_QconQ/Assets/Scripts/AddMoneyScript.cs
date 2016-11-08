using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddMoneyScript : MonoBehaviour
{
    private Button button { get { return GetComponent<Button>(); } }

    // Use this for initialization
    void Start()
    {
        button.onClick.AddListener(() => addMoney());
    }

    void addMoney()
    {
        GameControl.handle.player.Player_Money += 50;
    }

}
