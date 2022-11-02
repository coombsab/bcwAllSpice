namespace bcwAllSpice.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;

  public AccountService(AccountsRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    return _repo.Edit(original);
  }

  internal Account UpdateAccount(Account accountData, string accountId)
  {
    Account profile = _repo.GetById(accountId);
    if (profile == null) {
      throw new Exception("Could not find account.  Invalid ID.");
    }

    profile.Name = accountData.Name ?? profile.Name;
    profile.Picture = accountData.Picture ?? profile.Picture;

    return _repo.Edit(profile);
  }
}
