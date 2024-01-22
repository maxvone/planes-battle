using System.Threading.Tasks;
using CodeBase.AssetManagement;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    private const string UIRootPath = "UIRoot";
    private readonly IAssetProvider _assets;

    public Transform UiRoot { get; private set; }

    public UIFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public void CreateHUD()
    {
    }

    public async Task CreateUIRoot()
    {
      GameObject root = await _assets.Instantiate(UIRootPath);
      UiRoot = root.transform;
    }
  }
}