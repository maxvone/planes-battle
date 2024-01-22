using System.Threading.Tasks;
using CodeBase.AssetManagement;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    private const string UIRootPath = "UIRoot";
    private readonly IAssetProvider _assets;
    
    private Transform _uiRoot;

    public UIFactory(IAssetProvider assets)
    {
      _assets = assets;
    }

    public void CreateShop()
    {
      // WindowConfig config = _staticData.ForWindow(WindowId.Shop);
      // ShopWindow window = Object.Instantiate(config.Template, _uiRoot) as ShopWindow;
      // window.Construct(_adsService,_progressService);
    }

    public async Task CreateUIRoot()
    {
      GameObject root = await _assets.Instantiate(UIRootPath);
      _uiRoot = root.transform;
    }
  }
}