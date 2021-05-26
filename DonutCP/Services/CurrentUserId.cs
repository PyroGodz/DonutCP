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

        private static int _currentNoteId;
        public static int _CurrentNoteID
        {
            get { return _currentNoteId; }
            set
            {
                _currentNoteId = value;
            }
        }

        private static int _currentAthorId;
        public static int _CurrentAthorID
        {
            get { return _currentAthorId; }
            set
            {
                _currentNoteId = value;
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

        public static string currentWindowText;

    }
}
