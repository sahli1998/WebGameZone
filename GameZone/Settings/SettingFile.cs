namespace GameZone.Settings
{
    public static class SettingFile
    {
        public const string pathImage = "/Assets/Images/Games";
        public const  string _allowedExtensions = ".jpg,.jpeg,.png,.jfif";
        public const  int _maxInMb = 2;
        public const  int _maxInBytes = _maxInMb * 1024 * 1024;
    }
}
