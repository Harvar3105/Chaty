namespace Base;

public abstract class BaseUOW <DataBase>
    where DataBase : class
{
    protected readonly DataBase _db;

    protected BaseUOW(DataBase db)
    {
        _db = db;
    }
}