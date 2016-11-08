using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class creditAppear : MonoBehaviour {

    public CanvasGroup[] two;

	public void creditAppearSS()
    {
        for (int i = 0; i < 2; ++i )
        {
            two[i].alpha = 0;
        }
        this.gameObject.SetActive(true);
            
    }

    public void creditDissappearSS()
    {
        for (int i = 0; i < 2; ++i)
        {
            two[i].alpha = 1.0f;
        }

        this.gameObject.SetActive(false);
    }
}
