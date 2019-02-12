using System.Collections.Generic;
using Emby.AutoTagger.Domain;
using Emby.AutoTagger.Factories;
using Emby.AutoTagger.Strategies;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Serialization;

namespace Emby.AutoTagger
{
    public class PluginEntryPoint : IServerEntryPoint
    {
        private readonly ILogger logger;
        private readonly ILibraryManager libraryManager;

        private readonly TagFactory factory;
        private readonly TaggingStrategy taggingStrategy;

        public PluginEntryPoint(ILogger logger, ILibraryManager libraryManager, IJsonSerializer jsonSerializer)
        {
            this.logger = logger;
            this.libraryManager = libraryManager;
            
            this.factory = new TagFactory(jsonSerializer);
            this.taggingStrategy = new TaggingStrategy(this.libraryManager, this.logger);
        }
        public void Run()
        {
            this.libraryManager.ItemAdded += this.LibraryManager_ItemAdded;
        }

        private void LibraryManager_ItemAdded(object sender, ItemChangeEventArgs e)
        {
            if (e.Item is Video)
            {
                IEnumerable<Tag> tags = this.factory.CreateTagRuleset();
                foreach (Tag tag in tags)
                {
                    this.taggingStrategy.Apply(e.Item as Video, tag);
                }
            }
        }

        public void Dispose()
        {
            this.libraryManager.ItemAdded -= this.LibraryManager_ItemAdded;
        }
        
    }
}
