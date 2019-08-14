using System.Threading;
using System.Threading.Tasks;
using BaGet.Core.Entities;
using BaGet.Protocol;

namespace BaGet.Core.Search
{
    /// <summary>
    /// BaGet's extensions to the NuGet Search resource. These additions
    /// are not part of the official protocol.
    /// </summary>
    public interface IBaGetSearchService : ISearchService
    {
        /// <summary>
        /// Add a package to the search index.
        /// </summary>
        /// <param name="package">The package to add.</param>
        /// <param name="cancellationToken">A token to cancel the task.</param>
        /// <returns>A task that completes once the package has been added.</returns>
        Task IndexAsync(Package package, CancellationToken cancellationToken = default);

        /// <summary>
        /// Perform a search query.
        /// </summary>
        /// <param name="request">The search request.</param>
        /// <param name="cancellationToken">A token to cancel the task.</param>
        /// <returns>The search response.</returns>
        Task<SearchResponse> SearchAsync(BaGetSearchRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find the packages that depend on a given package.
        /// </summary>
        /// <param name="request">The dependents request.</param>
        /// <param name="cancellationToken">A token to cancel the task.</param>
        /// <returns>The dependents response.</returns>
        Task<DependentsResponse> FindDependentsAsync(DependentsRequest request, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// BaGet's extensions to a search request. These additions
    /// are not part of the official protocol.
    /// </summary>
    public class BaGetSearchRequest : SearchRequest
    {
        public static BaGetSearchRequest FromSearchRequest(SearchRequest request)
        {
            return new BaGetSearchRequest
            {
                Skip = request.Skip,
                Take = request.Take,
                IncludePrerelease = request.IncludePrerelease,
                IncludeSemVer2 = request.IncludeSemVer2,
                Query = request.Query,

                PackageType = null,
                Framework = null,
            };
        }

        /// <summary>
        /// The type of packages that should be returned.
        /// </summary>
        public string PackageType { get; set; }

        /// <summary>
        /// The Target Framework that results should be compatible.
        /// </summary>
        public string Framework { get; set; }
    }
}