namespace bcwAllSpice.Repositories;

public class RepositoryBase {
  protected readonly IDbConnection _db;

  public RepositoryBase(IDbConnection db)
  {
    _db = db;
  }
}