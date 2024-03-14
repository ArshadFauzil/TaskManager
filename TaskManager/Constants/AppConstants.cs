namespace TaskManager.Constants;

public static class AppConstants
{
    public const string UserTaskCachingKey = "_UserTasks";
    public const int cachingSlidingExpiration = 60;
    public const int cachingAbsoluteExpiration = 3600;
    public const int cachingMemorySize = 1024;
}