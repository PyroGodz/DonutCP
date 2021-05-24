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
	}
}
