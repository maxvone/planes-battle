using System.Threading.Tasks;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory: IService
  {
    public Transform UiRoot { get; }
    Task CreateUIRoot();
  }
}