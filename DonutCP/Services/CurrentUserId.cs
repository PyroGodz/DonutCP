using DonutCP.Model;
using DonutCP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonutCP.Services
{
    public static class CurrentUserId 
    {
		private static int _currentUserId;
		public static int _CurrentUserID
        {
            get { return _currentUserId; }
            set
            {
				_currentUserId = value;
			}
        }
        private static Note _currentNote;
        public static Note _CurrentNote
        {
            get { return _currentNote; }
            set
            {
                _currentNote = value;
            }
        }

        private static High_Lights currentHightLight;
        public static High_Lights _CurrentHightLight
        {
            get { return currentHightLight; }
            set
            {
                currentHightLight = value;
            }
        }

        public static string currentWindowText;

    }
}
