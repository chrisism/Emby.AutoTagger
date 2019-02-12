using MediaBrowser.Model.Plugins;

namespace Emby.AutoTagger
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string TaggingRules { get; set; }
    }
}
