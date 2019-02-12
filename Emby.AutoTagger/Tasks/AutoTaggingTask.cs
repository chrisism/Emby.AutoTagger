using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Emby.AutoTagger.Domain;
using Emby.AutoTagger.Factories;
using Emby.AutoTagger.Strategies;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Serialization;
using MediaBrowser.Model.Tasks;

namespace Emby.AutoTagger.Tasks
{
    public class AutoTaggingTask : IScheduledTask
    {
        private readonly ILibraryManager libraryManager;

        private readonly TagFactory factory;
        private readonly TaggingStrategy strategy;

        public AutoTaggingTask(ILibraryManager libraryManager, IJsonSerializer jsonSerializer, ILogger logger)
        {
            this.libraryManager = libraryManager;

            this.factory = new TagFactory(jsonSerializer);
            this.strategy = new TaggingStrategy(this.libraryManager, logger);
        }

        public Task Execute(CancellationToken cancellationToken, IProgress<double> progress)
        {
            return Task.Run(() =>
            {
                IEnumerable<Tag> tags = this.factory.CreateTagRuleset();
                int total = tags.Count();
                int current = 0;
                double mainChunk = (1 / total) * 100;
                double mainProgress = 0;

                foreach (Tag tag in tags)
                {
                    current++;
                    cancellationToken.ThrowIfCancellationRequested();

                    List<Video> untaggedItems = this.libraryManager.GetItemList(new InternalItemsQuery
                    {
                        MediaTypes = new[] {MediaType.Video},
                        IsVirtualItem = false,
                        MinRunTimeTicks = TimeSpan.FromMinutes(10).Ticks,
                        ExcludeTags = new[] { tag.Name }
                    }).OfType<Video>().ToList();

                    int subTotal = untaggedItems.Count;
                    int subCurrent = 0;
                    foreach (var item in untaggedItems)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        strategy.Apply(item, tag);
                        subCurrent++;

                        double subProgress = (subCurrent / subTotal);
                        double subProgressOnTotal = subProgress * mainChunk;
                        progress.Report(mainProgress + subProgressOnTotal);
                    }
                    
                    mainProgress = (current / total) * 100;
                    progress.Report(mainProgress);
                }

            }, cancellationToken);
        }

        public IEnumerable<TaskTriggerInfo> GetDefaultTriggers()
        {
            return new TaskTriggerInfo[0];
        }

        public string Name
        {
            get { return "Auto tagging"; }
        }

        public string Key
        {
            get { return "auto_tagging"; }
        }

        public string Description
        {
            get { return "Auto tagging plugin";  }
        }

        public string Category
        {
            get { return "Library"; }
        }
    }
}
