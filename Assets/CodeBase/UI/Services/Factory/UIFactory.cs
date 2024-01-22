using System.Threading.Tasks;
using CodeBase.AssetManagement;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    private const string UIRootPath = "UIRoot";
    private const string GameOverWindowPath = "GameOverWindow";
    private readonly IAssetProvider _assets;

    public Transform UiRoot { get; private set; }

    public UIFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public async Task<GameOverWindow> CreateGameOverWindow()
    {
      GameObject gameOverWindow = await _assets.Instantiate(GameOverWindowPath);
      return gameOverWindow.GetComponent<GameOverWindow>();
    }

    public async Task CreateUIRoot()
    {
      GameObject root = await _assets.Instantiate(UIRootPath);
      UiRoot = root.transform;
    }
  }
}