/* using UnityEngine;

public class StartingPlatform : MonoBehaviour
{
    // LevelGenerator referansı, hareket yönü ve hız bilgisi için
    [SerializeField] GameObject startingPlatform;
    private LevelGenerator levelGenerator;
    private float platformSpeed;
    private float platformSize;

    // LevelGenerator’dan gerekli bilgileri almak için Init metodu
    public void Init(LevelGenerator levelGen)
    {
        levelGenerator = levelGen;
        // LevelGenerator’da tanımlı platformSpeed ve platformSize değerlerini alıyoruz.
        // Bu değerlere erişiminiz yoksa, LevelGenerator’da public getter ekleyebilir veya
        // StartingPlatform script'inde SerializeField olarak tanımlayabilirsiniz.
        platformSpeed = levelGen.platformSpeed;
        platformSize = levelGen.platformSize;
    }

    void Update()
    {
        // LevelGenerator'ın transform.right vektörünü kullanarak platformu aynı hızda sola hareket ettiriyoruz.
        transform.Translate(-levelGenerator.transform.right * (platformSpeed * Time.deltaTime));

        // Platform ekran dışına çıktığında (örneğin belirli bir offset'e geldiğinde) 
        // isterseniz platformu yok edebilirsiniz.
        float platformDestroyOffset = platformSize * 2;
        if (transform.position.x < -platformDestroyOffset)
        {
            Destroy(gameObject);
        }
    }
}
 */