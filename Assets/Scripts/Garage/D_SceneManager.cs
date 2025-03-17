using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class D_SceneManager : MonoBehaviour
{
    private static D_SceneManager instance;

    public static D_SceneManager Instance
    {
        get
        {
            if(instance == null)
                instance = FindObjectOfType<D_SceneManager>();
            
            return instance;
        }
    }
    
    private List<RCC_CarControllerV3> allPlayerVehicles = new List<RCC_CarControllerV3>();

    public Transform spawnPoint;

    public RCC_CarControllerV3 currentVehicle;

    public GameObject[] panels;
    
    public TextMeshProUGUI panelTitleText;
    //public TextMeshProUGUI vehiclePriceText;
    public TextMeshProUGUI dollarText;
    public TextMeshProUGUI euroPriceText;

    public TextMeshProUGUI carName;
    //public GameObject selectVehicleButton;
    //public GameObject purchaseVehicleButton;

    public Slider Acceleration;
    public Slider Brake;
    public Slider Handling;
    public Slider Speed;

    /*[Space()] public Image vehicleStats_Engine_Upgraded;
    public Image vehicleStats_Handling_Upgraded;
    public Image vehicleStats_Speed_Upgraded;*/

    public int selectedVehicleIndex = 0;


    private void Awake()
    {
        allPlayerVehicles = new List<RCC_CarControllerV3>();
        selectedVehicleIndex = GetVehicleIndex();
    }

    private void Start()
    {
        SpawnAllPlayerVehicles();

        EnableVehicle();

        panelTitleText.text = SceneManager.GetActiveScene().name;
        carName.text = D_Vehicles.Instance.playerVehicles[selectedVehicleIndex].carName;
    }

    private void Update()
    {
        dollarText.text = D_Parameter.GetDollarMoney().ToString("F0");
        euroPriceText.text = D_Parameter.GetEuroMoney().ToString("F0");

        Acceleration.value = D_Vehicles.Instance.playerVehicles[selectedVehicleIndex].acceleration;
        Brake.value = D_Vehicles.Instance.playerVehicles[selectedVehicleIndex].brake;
        Handling.value = D_Vehicles.Instance.playerVehicles[selectedVehicleIndex].handling;
        Speed.value = D_Vehicles.Instance.playerVehicles[selectedVehicleIndex].speed;
        
        //D_Vehicles.Instance.playerVehicles[selectedVehicleIndex];
        /*if (currentVehicle)
        {
            selectVehicleButton.SetActive(D_Parameter.IsOwnedVehicle(selectedVehicleIndex));
            purchaseVehicleButton.SetActive(!selectVehicleButton.activeSelf);
        }*/
    }
    private void SpawnAllPlayerVehicles()
    {
        for (int i = 0; i < D_Vehicles.Instance.playerVehicles.Length; i++)
        {
            RCC_CarControllerV3 spawned = RCC.SpawnRCC(D_Vehicles.Instance.playerVehicles[i].vehicle,
                spawnPoint.transform.position, spawnPoint.transform.rotation, true, false, false);
            allPlayerVehicles.Add(spawned);
            
            spawned.gameObject.SetActive(false);
        }
    }

    public int GetVehicleIndex()
    {
        return D_Parameter.GetVehicle();
    }

    public void SelectVehicle()
    {
        D_Parameter.SetVehicle(selectedVehicleIndex);
    }

    public void NextVehicle()
    {
        selectedVehicleIndex++;
        if(selectedVehicleIndex >= D_Vehicles.Instance.playerVehicles.Length)
            selectedVehicleIndex = 0;

        EnableVehicle();
    }

    public void PreviousVehicle()
    {
        selectedVehicleIndex--;
        if(selectedVehicleIndex < 0)
            selectedVehicleIndex = D_Vehicles.Instance.playerVehicles.Length - 1;

        EnableVehicle();
    }

    public void EnableVehicle()
    {
        for (int i = 0; i < allPlayerVehicles.Count; i++)
        {
            if (allPlayerVehicles[i] != null)
            {
                allPlayerVehicles[i].gameObject.SetActive(false);
            }
        }

        if (allPlayerVehicles[selectedVehicleIndex] != null)
        {
            currentVehicle = allPlayerVehicles[selectedVehicleIndex];
            allPlayerVehicles[selectedVehicleIndex].gameObject.SetActive(true);
            RCC.RegisterPlayerVehicle(allPlayerVehicles[selectedVehicleIndex], false, false);
        }
        
        if(D_Vehicles.Instance.playerVehicles[selectedVehicleIndex].price <= 0)
            D_Parameter.UnlockVehicle(selectedVehicleIndex);
    }

    public void PurchaseVehicle()
    {
        /*int currentMoney = D_Parameter.GetMoney();

        if (currentMoney >= D_Vehicles.Instance.playerVehicles[selectedVehicleIndex].price)
        {
            D_Parameter.UnlockVehicle(selectedVehicleIndex);
            D_Parameter.ChangeMoney(-D_Vehicles.Instance.playerVehicles[selectedVehicleIndex].price);
            EnableVehicle();
        }*/
    }

    public void OpenPanel(GameObject activePanel)
    {
        for(int i = 0 ; i < panels.Length; i++)
            panels[i].SetActive(false);

        if (activePanel)
        {
            activePanel.SetActive(true);
        }
    }

    public void SetPanelTittleText(string tittle)
    {
        panelTitleText.text = tittle;
    }

    public void OpenScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
