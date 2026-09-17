using System.Collections.Immutable;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using CourseLoader.ApiClients;
using CourseLoader.ApiClients.Models;

namespace CourseLoader.Presentation;

public partial record MainModel
{
    private readonly SampleApiClient _sampleApi;
    public MainModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        SampleApiClient sampleApi)
    {
        _sampleApi = sampleApi;
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
    }

    public string? Title { get; }

    public IListFeed<CourseModel> Courses => ListFeed.Async(async ct =>
        (await _sampleApi.Courses.GetAsync(cancellationToken: ct))?.ToImmutableList()
        ?? ImmutableList<CourseModel>.Empty);
}    
