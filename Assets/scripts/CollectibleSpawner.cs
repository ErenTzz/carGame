using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Pooling Ayarlarý")]
    public ObjectPooler pooler;
    public string poolTag;

    [Header("Spawn Merkezi")]
    public Transform spawnCenter; // Inspector'dan atanacak merkez obje

    [Header("Spawn Pattern")]
    public SpawnPattern pattern = SpawnPattern.Grid;
    public int spawnCount = 50;

    [Header("Grid Ayarlarý (Grid Pattern için)")]
    [Tooltip("Grid için satýr sayýsý (0 = otomatik hesapla)")]
    public int gridRows = 0;
    [Tooltip("Grid için sütun sayýsý (0 = otomatik hesapla)")]
    public int gridColumns = 0;
    [Tooltip("Objeler arasý boþluk")]
    public float gridSpacing = 2f;

    [Header("Circle Ayarlarý (Circle Pattern için)")]
    [Tooltip("Dairenin yarýçapý")]
    public float circleRadius = 10f;
    [Tooltip("Y ekseninde yükseklik farký (spiral için)")]
    public float circleHeightVariation = 0f;

    [Header("Line Ayarlarý (Line Pattern için)")]
    [Tooltip("Çizgi yönü")]
    public LineDirection lineDirection = LineDirection.Forward;
    [Tooltip("Objeler arasý mesafe")]
    public float lineSpacing = 2f;

    [Header("Random Area Ayarlarý (Random Pattern için)")]
    public Vector3 areaSize = new Vector3(20f, 0f, 20f);

    [Header("Genel Ayarlar")]
    [Tooltip("Y pozisyonu sabit mi kalacak?")]
    public bool fixedYPosition = true;
    [Tooltip("Sabit Y deðeri")]
    public float yPosition = 0f;

    public enum SpawnPattern
    {
        Grid,           // Kare grid düzeni
        Circle,         // Dairesel düzen
        Line,           // Düz çizgi
        Random,         // Rastgele alan
        Spiral,         // Spiral düzen
        HexGrid         // Altýgen grid
    }

    public enum LineDirection
    {
        Forward,    // Z ekseni (ileri)
        Right,      // X ekseni (sað)
        Up          // Y ekseni (yukarý)
    }

    private void Start()
    {
        // Spawn center atanmamýþsa bu objeyi kullan
        if (spawnCenter == null)
            spawnCenter = transform;

        SpawnAll();
    }

    public void SpawnAll()
    {
        Vector3[] positions = GeneratePositions();

        for (int i = 0; i < positions.Length && i < spawnCount; i++)
        {
            pooler.SpawnFromPool(poolTag, positions[i], Quaternion.identity);
        }
    }

    private Vector3[] GeneratePositions()
    {
        switch (pattern)
        {
            case SpawnPattern.Grid:
                return GenerateGridPositions();
            case SpawnPattern.Circle:
                return GenerateCirclePositions();
            case SpawnPattern.Line:
                return GenerateLinePositions();
            case SpawnPattern.Random:
                return GenerateRandomPositions();
            case SpawnPattern.Spiral:
                return GenerateSpiralPositions();
            case SpawnPattern.HexGrid:
                return GenerateHexGridPositions();
            default:
                return GenerateRandomPositions();
        }
    }

    private Vector3[] GenerateGridPositions()
    {
        Vector3[] positions = new Vector3[spawnCount];

        // Otomatik satýr/sütun hesaplama
        int rows = gridRows;
        int cols = gridColumns;

        if (rows == 0 && cols == 0)
        {
            // Kare grid oluþtur
            cols = Mathf.CeilToInt(Mathf.Sqrt(spawnCount));
            rows = Mathf.CeilToInt((float)spawnCount / cols);
        }
        else if (rows == 0)
        {
            rows = Mathf.CeilToInt((float)spawnCount / cols);
        }
        else if (cols == 0)
        {
            cols = Mathf.CeilToInt((float)spawnCount / rows);
        }

        Vector3 center = spawnCenter.position;
        float startX = -(cols - 1) * gridSpacing / 2f;
        float startZ = -(rows - 1) * gridSpacing / 2f;

        int index = 0;
        for (int row = 0; row < rows && index < spawnCount; row++)
        {
            for (int col = 0; col < cols && index < spawnCount; col++)
            {
                float x = startX + col * gridSpacing;
                float z = startZ + row * gridSpacing;
                float y = fixedYPosition ? yPosition : center.y;

                positions[index] = new Vector3(
                    center.x + x,
                    y,
                    center.z + z
                );
                index++;
            }
        }

        return positions;
    }

    private Vector3[] GenerateCirclePositions()
    {
        Vector3[] positions = new Vector3[spawnCount];
        Vector3 center = spawnCenter.position;

        for (int i = 0; i < spawnCount; i++)
        {
            float angle = i * (360f / spawnCount) * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * circleRadius;
            float z = Mathf.Sin(angle) * circleRadius;

            // Spiral efekti için yükseklik deðiþimi
            float heightOffset = (circleHeightVariation * i) / spawnCount;
            float y = fixedYPosition ? yPosition : center.y + heightOffset;

            positions[i] = new Vector3(
                center.x + x,
                y,
                center.z + z
            );
        }

        return positions;
    }

    private Vector3[] GenerateLinePositions()
    {
        Vector3[] positions = new Vector3[spawnCount];
        Vector3 center = spawnCenter.position;
        float totalLength = (spawnCount - 1) * lineSpacing;
        float startOffset = -totalLength / 2f;

        for (int i = 0; i < spawnCount; i++)
        {
            float offset = startOffset + i * lineSpacing;
            Vector3 pos = center;

            switch (lineDirection)
            {
                case LineDirection.Forward:
                    pos.z += offset;
                    break;
                case LineDirection.Right:
                    pos.x += offset;
                    break;
                case LineDirection.Up:
                    pos.y += offset;
                    break;
            }

            if (fixedYPosition && lineDirection != LineDirection.Up)
                pos.y = yPosition;

            positions[i] = pos;
        }

        return positions;
    }

    private Vector3[] GenerateRandomPositions()
    {
        Vector3[] positions = new Vector3[spawnCount];
        Vector3 center = spawnCenter.position;

        for (int i = 0; i < spawnCount; i++)
        {
            float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float z = Random.Range(-areaSize.z / 2f, areaSize.z / 2f);
            float y = fixedYPosition ? yPosition : center.y + Random.Range(-areaSize.y / 2f, areaSize.y / 2f);

            positions[i] = new Vector3(
                center.x + x,
                y,
                center.z + z
            );
        }

        return positions;
    }

    private Vector3[] GenerateSpiralPositions()
    {
        Vector3[] positions = new Vector3[spawnCount];
        Vector3 center = spawnCenter.position;

        // Eþit aralýklý spiral için
        float spacing = gridSpacing; // Objeler arasý mesafe
        float angle = 0f;
        float radius = 0f;

        for (int i = 0; i < spawnCount; i++)
        {
            // Archimedean spiral formülü
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            float y = fixedYPosition ? yPosition : center.y + (i * circleHeightVariation / spawnCount);

            positions[i] = new Vector3(
                center.x + x,
                y,
                center.z + z
            );

            // Bir sonraki nokta için açý ve yarýçapý hesapla
            // Eþit mesafe için açý artýþýný yarýçapa göre ayarla
            float angleIncrement = spacing / (radius + 0.1f); // +0.1f sýfýra bölmeyi önler
            angle += angleIncrement;
            radius += spacing * angleIncrement / (2f * Mathf.PI); // Archimedean spiral
        }

        return positions;
    }

    private Vector3[] GenerateHexGridPositions()
    {
        Vector3[] positions = new Vector3[spawnCount];
        Vector3 center = spawnCenter.position;

        float hexWidth = gridSpacing;
        float hexHeight = gridSpacing * 0.866f; // sqrt(3)/2

        int rings = Mathf.CeilToInt(Mathf.Sqrt(spawnCount / 3f));
        int index = 0;

        // Merkez
        if (index < spawnCount)
        {
            positions[index] = new Vector3(center.x, fixedYPosition ? yPosition : center.y, center.z);
            index++;
        }

        // Halkalar
        for (int ring = 1; ring <= rings && index < spawnCount; ring++)
        {
            int hexCount = ring * 6;
            for (int i = 0; i < hexCount && index < spawnCount; i++)
            {
                float angle = (i * 60f / ring) * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * ring * hexWidth;
                float z = Mathf.Sin(angle) * ring * hexHeight;
                float y = fixedYPosition ? yPosition : center.y;

                positions[index] = new Vector3(
                    center.x + x,
                    y,
                    center.z + z
                );
                index++;
            }
        }

        return positions;
    }

    // Gizmos ile spawn alanýný görselleþtir
    private void OnDrawGizmosSelected()
    {
        if (spawnCenter == null)
            spawnCenter = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnCenter.position, 0.5f);

        switch (pattern)
        {
            case SpawnPattern.Grid:
                DrawGridGizmos();
                break;
            case SpawnPattern.Circle:
            case SpawnPattern.Spiral:
                DrawCircleGizmos();
                break;
            case SpawnPattern.Random:
                DrawRandomAreaGizmos();
                break;
        }
    }

    private void DrawGridGizmos()
    {
        int rows = gridRows == 0 ? Mathf.CeilToInt(Mathf.Sqrt(spawnCount)) : gridRows;
        int cols = gridColumns == 0 ? Mathf.CeilToInt(Mathf.Sqrt(spawnCount)) : gridColumns;

        Gizmos.color = Color.green;
        float width = (cols - 1) * gridSpacing;
        float height = (rows - 1) * gridSpacing;

        Vector3 center = spawnCenter.position;
        Vector3 topLeft = center + new Vector3(-width / 2f, 0, height / 2f);
        Vector3 topRight = center + new Vector3(width / 2f, 0, height / 2f);
        Vector3 bottomLeft = center + new Vector3(-width / 2f, 0, -height / 2f);
        Vector3 bottomRight = center + new Vector3(width / 2f, 0, -height / 2f);

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }

    private void DrawCircleGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 center = spawnCenter.position;

        int segments = 32;
        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * (360f / segments) * Mathf.Deg2Rad;
            float angle2 = (i + 1) * (360f / segments) * Mathf.Deg2Rad;

            Vector3 point1 = center + new Vector3(
                Mathf.Cos(angle1) * circleRadius,
                0,
                Mathf.Sin(angle1) * circleRadius
            );

            Vector3 point2 = center + new Vector3(
                Mathf.Cos(angle2) * circleRadius,
                0,
                Mathf.Sin(angle2) * circleRadius
            );

            Gizmos.DrawLine(point1, point2);
        }
    }

    private void DrawRandomAreaGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spawnCenter.position, areaSize);
    }
}