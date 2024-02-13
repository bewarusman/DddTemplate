namespace Domain;


public abstract class Entity
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Entity()
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = CreatedAt;
    }

    public void UpdateTimestamps()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}



