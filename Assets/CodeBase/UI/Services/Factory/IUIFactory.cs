using System.Threading.Tasks;
using CodeBase.Services;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory: IService
  {
    public Transform UiRoot { get; }
    Task CreateUIRoot();
    Task<GameOverWindow> CreateGameOverWindow();
  }
}