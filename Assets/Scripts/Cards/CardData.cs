using UnityEngine;

/// <summary>
/// ScriptableObject asset that holds references to the game's source CSV data.
/// The asset must be in a Resources folder so it can load itself at runtime.
/// </summary>
[CreateAssetMenu]
public class CardData : ScriptableObject
{

    [SerializeField] private TextAsset firstNamesSource;
    [SerializeField] private TextAsset lastNamesSource;
    [SerializeField] private TextAsset jobsSource;
    [SerializeField] private TextAsset objectsSource;
    [SerializeField] private TextAsset locationsSource;
    [SerializeField] private TextAsset nationalitiesSource;
    [SerializeField] private TextAsset cardsSource;

    [SerializeField] private string firstNameFallback = "John";
    [SerializeField] private string lastNameFallback = "Doe";
    [SerializeField] private string jobFallback = "Engineer";
    [SerializeField] private string objectFallback = "climate controls";
    [SerializeField] private string locationFallback = "ship";
    [SerializeField] private string nationalityFallback = "human";

    private static CardData instance = null;

#if UNITY_EDITOR
    // If play mode has disabled domain reloading, we have to manually reset static variables before play.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        instance = null;
    }
#endif

    public StringLibrary FirstNames { get; private set; }
    public StringLibrary LastNames { get; private set; }
    public StringLibrary Jobs { get; private set; }
    public StringLibrary Objects { get; private set; }
    public StringLibrary Locations { get; private set; }
    public StringLibrary Nationalities { get; private set; }
    public CardLibrary Cards { get; private set; }

    public static CardData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<CardData>(nameof(CardData));
                if (instance == null)
                {
                    Debug.LogError($"Can't load {nameof(CardData)} from Resources");
                }
                else
                {
                    instance.LoadSources();
                }
            }
            return instance;
        }
    }

    private void LoadSources()
    {
        FirstNames = new StringLibrary(firstNamesSource, firstNameFallback);
        LastNames = new StringLibrary(lastNamesSource, lastNameFallback);
        Jobs = new StringLibrary(jobsSource, jobFallback);
        Objects = new StringLibrary(objectsSource, objectFallback);
        Locations = new StringLibrary(locationsSource, locationFallback);
        Nationalities = new StringLibrary(nationalitiesSource, nationalityFallback);
        Cards = new CardLibrary(cardsSource);
    }

}
