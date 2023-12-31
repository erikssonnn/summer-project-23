using UnityEngine;

[System.Serializable]
public class ResourceClass
{
    public int food;
    public int wood;
    public int stone;
    public int tin;
    public int copper;

    public ResourceClass(int food, int wood, int stone, int tin, int copper)
    {
        this.food = food;
        this.wood = wood;
        this.stone = stone;
        this.tin = tin;
        this.copper = copper;
    }
}

public class ResourceManager : MonoBehaviour
{
    public ResourceClass CurrentResources { get; } = new ResourceClass(
        2,
        3,
        0,
        0,
        0
    );

    public static ResourceManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UIManager.Instance.ChangeResources(CurrentResources);
    }

    public void ChangeResources(ResourceClass changeAmount)
    {
        CurrentResources.food += changeAmount.food;
        CurrentResources.wood += changeAmount.wood;
        CurrentResources.stone += changeAmount.stone;
        CurrentResources.tin += changeAmount.tin;
        CurrentResources.copper += changeAmount.copper;

        CurrentResources.food = Mathf.Clamp(CurrentResources.food, 0, 100000);
        CurrentResources.wood = Mathf.Clamp(CurrentResources.wood, 0, 100000);
        CurrentResources.stone = Mathf.Clamp(CurrentResources.stone, 0, 100000);
        CurrentResources.tin = Mathf.Clamp(CurrentResources.tin, 0, 100000);
        CurrentResources.copper = Mathf.Clamp(CurrentResources.copper, 0, 100000);

        UIManager.Instance.ChangeResources(CurrentResources);
    }

    public bool HasEnoughResources(ResourceClass costResources)
    {
        return Mathf.Abs(costResources.food) <= CurrentResources.food &&
               Mathf.Abs(costResources.wood) <= CurrentResources.wood &&
               Mathf.Abs(costResources.stone) <= CurrentResources.stone &&
               Mathf.Abs(costResources.tin) <= CurrentResources.tin &&
               Mathf.Abs(costResources.copper) <= CurrentResources.copper;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle { fontSize = 20, 
            fontStyle = FontStyle.Bold, 
            normal = new GUIStyleState() { textColor = Color.white } };
        string str = "FOOD: " + CurrentResources.food + 
                     "\nWOOD: " + CurrentResources.wood +
                     "\nSTONE: " + CurrentResources.stone +
                     "\nTIN: " + CurrentResources.tin +
                     "\nCOPPER: " + CurrentResources.copper;
        GUI.Label(new Rect(10, 10, Screen.width / 2.0f, Screen.height / 2.0f), str, style);
    }
}