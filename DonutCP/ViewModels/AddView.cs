using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonutCP.ViewModels
{
    class AddView
    {
        public string profileImage;

        public void AddImage(string _profileImage)
        {
            profileImage = _profileImage;
        }

    }
}
