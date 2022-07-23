using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HBDStack.Framework.Extensions.Tests.TestObjects;

public enum TestEnum
{
    [Display(Name = "Enum 1")] Enum1,
    Enum2
}

public interface ITem
{
    #region Properties

    int Id { get; set; }

    string Name { get; set; }

    #endregion Properties
}

public class TestItem : ITem
{
    #region Properties

    public string Details { get; set; }

    public int Id { get; set; }

    public string Name { get; set; }

    #endregion Properties
}

public class TestItem2 : ITem
{
    #region Properties

    public int Id { get; set; }

    public string Name { get; set; }

    #endregion Properties
}

public class TestItem3 : ITem, IDisposable
{
    #region Constructors

    public TestItem3()
    {
    }

    public TestItem3(string name)
    {
        Name = name;
    }

    #endregion Constructors

    #region Properties

    public string Description { get; set; }

    public int Id { get; set; }

    public bool IsDisposed { get; private set; }

    public string Name { get; set; }

    [Column("Summ")] public string Summary { get; set; }

    public TestEnum Type { get; set; } = TestEnum.Enum1;

    protected object ProtectedObj { get; set; } = new object();

    private object PrivateObj { get; set; } = new object();

    #endregion Properties

    #region Methods

    public void Dispose() => IsDisposed = true;

    #endregion Methods
}