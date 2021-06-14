using ProtfolioBackend.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProtfolioBackend.Extentions
{
    public class ContentComparer: IEqualityComparer<dtoGithubRepoContentVaribles>
    {
        // This class is used to compare incoming content form github to content in DB to compare if any values have changed.
        // Had to override the Equals
        public bool Equals(dtoGithubRepoContentVaribles x, dtoGithubRepoContentVaribles y)
        {
            if (x.GithubId == y.GithubId
                && x.Name == y.Name
                && x.Content == y.Content
                && x.Url == y.Url)
                return true;

            return false;
        }

        public int GetHashCode(dtoGithubRepoContentVaribles obj)
        {
            return obj.Content.GetHashCode() +
                obj.Name.GetHashCode() +
                obj.Url.GetHashCode();
        }
    }
}
