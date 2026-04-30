using Microsoft.AspNetCore.Components.Forms;

namespace BlazorAppBB.Services
{
    public class Capabilities
    {
        public event Action? StateChanged;
        private bool _w = false;
        private bool _c = false;

        public bool W
        {
            get => _w;
            set
            {
                _w = value;
                NotifyStateChanged();
            }
        }

        public bool C
        {
            get => _c;
            set
            {
                _c = value;
                NotifyStateChanged();
            }
        }

        public bool ClearAll
        {

            set
            {
                _c = false;
                _w = false;
                NotifyStateChanged();
            }
        }

        public event Action OnChange;

        private void NotifyStateChanged() =>  StateChanged?.Invoke();
        
    }

    public class CorporationSettings
    {
        public string CorpName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
    }

    public class UserSettings
    {
        public Guid session_guid { get; set; } = default;
        public string trusted_name { get; set; } = default;
        public string download_filename { get; set; } = default;
        public string name { get; set; } = string.Empty;
        public string phonenumber { get; set; } = string.Empty;

    }

}
