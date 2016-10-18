# region Enums

public enum Color
{
    Blue,
    Red
}

public enum BaseType
{
    Sedan,
    Hatchback,
    Coupe
}

public enum EngineType
{
    ExternalCombustion,
    InternalCombustion
}

# endregion

# region Interfaces

public interface ICarPart
{
    double Weight { get; }
}

public interface IBodyPart : ICarPart
{
    double Width { get; }
    double Depth { get; }
    double Height { get; }
    Color Color { get; }
}

public interface IDoor : IBodyPart
{
    bool HasWindow { get; }
}

public interface IBase : IBodyPart
{
    BaseType Type { get; }
}

public interface IBody : ICarPart
{
    IBase Base { get; }
    IEnumerable<IDoor> Doors { get; }
}

public interface IEngine : ICarPart
{
    EngineType Type { get; }
}

public interface IWheel : ICarPart
{
    double Size { get; }
}

public interface ICarFactory
{
    IBody CreateBody();
    IEngine CreateEngine();
    IWheel CreateWheel();
}

# endregion

# region BMW

public class BmwBase : IBase
{
    BaseType Type => BaseType.Sedan;

    double Width => 250;
    double Depth => 150;
    double Height => 100;
    Color Color => Color.Red;

    double Weight => 123.5;
}

public class BmwDoor : IDoor
{
    bool HasWindow => true;

    double Width => 60;
    double Depth => 10;
    double Height => 60;
    Color Color => Color.Red;

    double Weight => 20;
}

public class BmwBody : IBody
{
    IBase Base { get; private set; }
    IEnumerable<IDoor> Doors { get; private set; }

    double Weight => Base.Weight + Doors.Sum(d => d.Weight);

    public BmwBody()
    {
        Base = new BmwBase();
        Doors = new List<IDoor> {
            new BmwDoor(),
            new BmwDoor(),
            new BmwDoor(),
            new BmwDoor()
        };
    }
}

public class BmwEngine : IEngine
{
    EngineType Type => EngineType.InternalCombustion;

    double Weight => 500.1;
}

public class BmwWheel : IWheel
{
    double Size => 21;

    double Weight => 10.3;
}

public class BmwFactory : ICarFactory
{
    public IBody CreateBody()
    {
        return new BmwBody();
    }

    public IEngine CreateEngine()
    {
        return new BmwEngine();
    }

    public IEnumerable<IWheel> CreateWheels()
    {
        return new BmwWheel();
    }
}

# endregion

# region Audi

public class AudiBase : IBase
{
    BaseType Type => BaseType.Coupe;

    double Width => 230;
    double Depth => 110;
    double Height => 150;
    Color Color => Color.Blue;

    double Weight => 123.1;
}

public class AudiDoor : IDoor
{
    bool HasWindow => true;

    double Width => 40;
    double Depth => 11;
    double Height => 40;
    Color Color => Color.Red;

    double Weight => 19;
}

public class AudiBody : IBody
{
    IBase Base { get; private set; }
    IEnumerable<IDoor> Doors { get; private set; }

    double Weight => Base.Weight + Doors.Sum(d => d.Weight);

    public AudiBody()
    {
        Base = new AudiBase();
        Doors = new List<IDoor> {
            new AudiDoor(),
            new AudiDoor()
        };
    }
}

public class AudiEngine : IEngine
{
    EngineType Type => EngineType.InternalCombustion;

    double Weight => 500.05;
}

public class AudiWheel : IWheel
{
    double Size => 23;

    double Weight => 15;
}

public class AudiFactory : ICarFactory
{
    public IBody CreateBody()
    {
        return new AudiBody();
    }

    public IEngine CreateEngine()
    {
        return new AudiEngine();
    }

    public IEnumerable<IWheel> CreateWheels()
    {
        return new AudiWheel();
    }
}

# endregion