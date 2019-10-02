using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class CarSelectionManager : MonoBehaviour
{
	public List<CarDataScriptableObject> CarTypes = new List<CarDataScriptableObject>();
	int SelectionIndex = 0;
	CarDataScriptableObject currentSelection;
	public Transform CenterSpawn;
	public PlayerData playerData;
	public Image speedImage,gripImage,accelImage;
	GameObject VisableMesh;

    public TextMeshProUGUI Title;
    // Start is called before the first frame update
    void Start()
    {
    	playerData = PlayerData.instance;
        UpdatePanel();
    }

   

    public void Next(){
    	SelectionIndex++;
    	if(SelectionIndex >= CarTypes.Count)
    		SelectionIndex =0;
    	UpdatePanel();
    }
    public void Previouse(){
    	SelectionIndex--;
    	if(SelectionIndex < 0)
    		SelectionIndex = CarTypes.Count;
    	UpdatePanel();
    }

    void UpdatePanel(){
    	playerData.currentSelection = CarTypes[SelectionIndex];
    	currentSelection = CarTypes[SelectionIndex];

    	if(VisableMesh != null)
    		Destroy(VisableMesh);

    	VisableMesh = Instantiate(currentSelection.MeshObject, Vector3.zero, Quaternion.identity,CenterSpawn);
		VisableMesh.transform.localEulerAngles += Vector3.up *90;
    	speedImage.fillAmount = currentSelection.Speed /1f;
    	gripImage.fillAmount = currentSelection.Grip /5f;
    	accelImage.fillAmount = currentSelection.Acceleration /.1f;
        Title.text = currentSelection.CarType;
    }

    public void Play(){
    	 SceneManager.LoadScene("MainScene");
    }
}
