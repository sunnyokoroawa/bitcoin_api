using System;
using System.Collections.Generic;
using System.Text;

namespace Bitcoin.Core.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime Date { get; }
    }
}
