namespace Example_06.ChainOfResponsibility
{
    public enum CurrencyType
    {
        Eur,
        Dollar,
        Ruble
    }

    public interface IBanknote
    {
        CurrencyType Currency { get; }
        int Value { get; }
    } 

    public class Bancomat
    {
        private readonly BanknoteHandler _handler;

        public Bancomat()
        {
            // Banknote value ascending order please ^_^
            _handler = new TenRubleHandler(null);
            _handler = new TenDollarHandler(_handler);
            _handler = new FiftyDollarHandler(_handler);
            _handler = new HundredDollarHandler(_handler);
        }

        public bool Validate(IBanknote banknote)
        {
            return _handler.Validate(banknote);
        }

        public bool Withdraw(CurrencyType currency, int amount)
        {
            return _handler.Withdraw(currency, amount);
        }
    }

    public abstract class BanknoteHandler
    {
        private readonly BanknoteHandler _nextHandler;

        protected abstract CurrencyType Currency { get; }
        protected abstract int Value { get; }

        protected BanknoteHandler(BanknoteHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public virtual bool Validate(IBanknote banknote)
        {
            if (banknote.Currency == Currency && banknote.Value == Value)
            {
                return true;
            }
            return _nextHandler != null && _nextHandler.Validate(banknote);
        }

        public virtual bool Withdraw(CurrencyType currency, int amount)
        {
            if (currency == Currency)
            {
                amount -= amount / Value * Value;
            }

            if (amount == 0)
            {
                return true;
            }

            return _nextHandler != null && _nextHandler.Withdraw(currency, amount);
        }
    }

    public class TenRubleHandler : BanknoteHandler
    {
        protected override CurrencyType Currency => CurrencyType.Ruble;
        protected override int Value => 10;
        
        public TenRubleHandler(BanknoteHandler nextHandler)
            : base(nextHandler) { }
    }

    public abstract class DollarHandlerBase : BanknoteHandler
    {
        protected override CurrencyType Currency => CurrencyType.Dollar;

        protected DollarHandlerBase(BanknoteHandler nextHandler)
            : base(nextHandler) { }
    }

    public class HundredDollarHandler : DollarHandlerBase
    {
        protected override int Value => 100;

        public HundredDollarHandler(BanknoteHandler nextHandler)
            : base(nextHandler) { }
    }

    public class FiftyDollarHandler : DollarHandlerBase
    {
        protected override int Value => 50;

        public FiftyDollarHandler(BanknoteHandler nextHandler)
            : base(nextHandler) { }
    }

    public class TenDollarHandler : DollarHandlerBase
    {
        protected override int Value => 10;

        public TenDollarHandler(BanknoteHandler nextHandler)
            : base(nextHandler) { }
    }
}
