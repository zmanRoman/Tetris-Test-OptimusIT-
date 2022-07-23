using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPlayAgain : MonoBehaviour {

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
		
	
}
