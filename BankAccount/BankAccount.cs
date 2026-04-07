namespace BankAccount;
public class BankAccount
{
    private decimal balance;
    private bool isBlocked;
    private readonly object lockObj = new object();

    public BankAccount(decimal initialBalance)
    {
        balance = initialBalance;
        isBlocked = false;
    }

    public void Deposit(decimal amount)
    {
        lock (lockObj)
        {
            if (isBlocked)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: Рахунок заблоковано.Попванення не виконане");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: Сума поповнення має бути додатною");
                return;
            }

            balance += amount;
            Console.WriteLine($"{Thread.CurrentThread.Name}: Поповнення на {amount}. Баланс: {balance}");
        }
    }

    public void Withdraw(decimal amount)
    {
        lock (lockObj)
        {
            if (isBlocked)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: Рахунок заблоковано.Зняття не виконано");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: Сума зняття має бути додатною");
                return;
            }

            if (amount > balance)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: Недостатньо коштів");
                return;
            }

            balance -= amount;
            Console.WriteLine($"{Thread.CurrentThread.Name}: Зняття {amount}. Баланс: {balance}");
        }
    }

    public void Block()
    {
        lock (lockObj)
        {
            isBlocked = true;
            Console.WriteLine($"{Thread.CurrentThread.Name}: Рахунок заблокован");
        }
    }

    public void Unblock()
    {
        lock (lockObj)
        {
            isBlocked = false;
            Console.WriteLine($"{Thread.CurrentThread.Name}: Рахунок розблокован");
        }
    }

    public decimal GetBalance()
    {
        lock (lockObj)
        {
            return balance;
        }
    }
}

