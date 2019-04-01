namespace Sourav.Engine.Editable.NotificationRelated
{
    //Fill this up with game Notifications  
    
    public enum Notification
    {
        None,
        //Pause Related
        PauseGame,
        ResumeGame,
        GamePaused,
        GameResumed,
        PauseResumeToggle,
        
        //Camera Related
        StartCameraScript,
        StartZoomingCamera,
        StopZoomingCamera,
        StopCameraScript,
        
        //Data Related
        RecordPositionData,
        FetchPositionData,
        PositionData,
        
        //Save Load Related
        LoadData,
        SaveData,
        DataLoaded,
        DataChanged,
        
        //Gameplay related
        StartWebservice,
        WebserviceSuccess,
        WebserviceFailure,
        DataSuccessfullyParsed,
        DataParsingFailure,
        
        
        //Button related
        PlayButtonPressed,
        SelectionButtonPressed,
       
        
        //api related
        StartDownloadingModel,
        ModelDownloaded,
        StartDownloadingMaterial,
        MaterialDownloaded,
    }
}



