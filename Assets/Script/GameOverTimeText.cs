using UnityEngine;
using UnityEngine.UI;

public class GameOverTimeText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text text = gameObject.GetComponent<Text>();
        text.text = Timer.GetTotalTimeString();
    }

}
