using SporcialAPI.Context;
using SporcialAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace SporcialAPI.Services;

public class ActivityService : IActivityService
{
    private readonly SporcialDbContext _sporcialDbContext;

    public ActivityService(SporcialDbContext context)
    {
        _sporcialDbContext = context;
    }

    public async Task<Activity> AddActivityAsync(Activity activity)
    {
        ArgumentNullException.ThrowIfNull(activity);

        _sporcialDbContext.Add(activity);
        await _sporcialDbContext.SaveChangesAsync();
        return activity;
    }

    public Task<Activity> DeleteActivityAsync(Guid activityId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Activity>> GetActivitiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Activity> GetActivityAsync(Guid activityId)
    {
        throw new NotImplementedException();
    }

    public Task<Activity> UpdateActivityAsync(Activity activity)
    {
        throw new NotImplementedException();
    }
}
