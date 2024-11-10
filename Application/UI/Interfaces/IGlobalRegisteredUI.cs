
using Application.UI.Abstract;
using System.Collections.Generic;

namespace Application.UI.Interfaces
{
    public interface IGlobalRegisteredUI
    {
        public IReadOnlyDictionary<string, RegisteredPanelUI> RegisteredPanelsUI { get; }
    }
}
