namespace Core.Database.Interfaces;

public interface IDatabaseInitializer
{
    public Task InitializeAsynchronously();
}