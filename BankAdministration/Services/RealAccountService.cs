namespace BankAdministration.Services;

public class RealAccountService : IRealAccountService
{
    public bool Deposit(int accountId, int belopp)
    {
        return false;
    }
    public bool CanWithdraw(int accountId, int belopp)
    {
        return false;
    }
    public enum ErrorCode
    {
        Ok,
        BalanceTooLow,
    }

    IRealAccountService.ErrorCode IRealAccountService.Withdraw(int accountId, int belopp)
    {
        throw new NotImplementedException();
    }
}
