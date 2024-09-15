namespace Base;

public abstract class Base_UOW <DataBase>
    where DataBase : class
{
    protected readonly DataBase _db;

    protected Base_UOW(DataBase db)
    {
        _db = db;
    }
}