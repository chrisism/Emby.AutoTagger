using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Model.Logging;

namespace Emby.AutoTagger.Domain
{
    public class TagRule
    {
        public string Name { get; set; }

        public string Field { get; set; }

        public string Expression { get; set; }

        public bool Negative { get;set; }

        public bool IsValidFor(Video item, ILogger logger)
        {
            if (item == null)
                return false;

            Type videoType = item.GetType();
            PropertyInfo property = videoType.GetProperty(this.Field);

            if (property == null)
            {
                logger.Warn($"Cannot find property {this.Field}");
                return false;
            }

            if (property.PropertyType.IsArray)
            {
                IEnumerable propertyValues = property.GetValue(item) as IEnumerable;

                if (propertyValues == null)
                    return false;

                foreach (object singleValue in propertyValues)
                {
                    if (singleValue == null)
                        continue;

                    bool singleMatch = Regex.IsMatch(singleValue.ToString(), this.Expression, RegexOptions.Singleline);
                    if (singleMatch)
                        return true;
                }

                return false;
            }

            object propertyValue = property.GetValue(item);
            
            if (propertyValue == null)
                return false;

            bool matches = Regex.IsMatch(propertyValue.ToString(), this.Expression, RegexOptions.Singleline);
            return matches;
        }
    }
}
