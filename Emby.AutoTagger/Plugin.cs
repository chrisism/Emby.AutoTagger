using System;
using System.Collections.Generic;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;

namespace Emby.AutoTagger
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages //, IHasThumbImage
    {
        private static readonly Guid PluginId = new Guid("cd18c687-8abb-4119-b24e-71b4d4606c94");

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public static Plugin Instance
        {
            get; private set;
        }

        public override Guid Id
        {
            get { return PluginId; }
        }

        public override string Name
        {
            get { return "Auto tagging"; }
        }

        public override string Description
        {
            get { return "Trigger automatic tagging of movies"; }
        }

        public IEnumerable<PluginPageInfo> GetPages()
        {
            return new[]
            {
                new PluginPageInfo
                {
                    Name = "Auto tagging settings",
                    EmbeddedResourcePath = this.GetType().Namespace + ".UI.configuration.html"
                }
            };
        }

        //public Stream GetThumbImage()
        //{
        //    var type = this.GetType();
        //    return type.Assembly.GetManifestResourceStream(type.Namespace + ".UI.couchpotato_icon.png");
        //}

        public ImageFormat ThumbImageFormat
        {
            get
            {
                return ImageFormat.Png;
            }
        }
    }
}