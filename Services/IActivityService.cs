using SporcialAPI.Entities;

namespace SporcialAPI.Services;

public interface IActivityService
{
    Task<IEnumerable<Activity>> GetActivitiesAsync();
    Task<Activity> GetActivityAsync(Guid activityId);
    Task<Activity> AddActivityAsync(Activity activity);
    Task<Activity> UpdateActivityAsync(Activity activity);
    Task<Activity> DeleteActivityAsync(Guid activityId);
}
