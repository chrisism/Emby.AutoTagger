using Emby.AutoTagger.Domain;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Logging;

namespace Emby.AutoTagger.Strategies
{
    public class TaggingStrategy
    {
        private readonly ILibraryManager libraryManager;
        private readonly ILogger logger;

        public TaggingStrategy(ILibraryManager libraryManager, ILogger logger)
        {
            this.libraryManager = libraryManager;
            this.logger = logger;
        }

        public void Apply(Video item, Tag tag)
        {
            if (tag.Rules == null)
                return;

            foreach (TagRule rule in tag.Rules)
            {
                if (rule.IsValidFor(item, this.logger))
                {
                    if (rule.Negative)
                    {
                        this.logger.Info($"Skipping tag \"{tag.Name}\" on item \"{item.Name}\" because of rule: {rule.Name}");
                        return;
                    }

                    this.logger.Info($"Applied tag \"{tag.Name}\" on item \"{item.Name}\" using rule: {rule.Name}");

                    if (tag.Name.Equals("RESET$TAGS"))
                    {
                        item.Tags = new string[0];
                    }
                    else
                    {
                        item.AddTag(tag.Name);
                    }

                    this.libraryManager.UpdateItem(item, item.Parent, ItemUpdateType.MetadataEdit);

                    return;
                }
            }
        }
    }
}
