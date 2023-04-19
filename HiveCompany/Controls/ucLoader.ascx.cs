using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HiveCompany.Controls
{
    public partial class ucLoader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string AssociatedUpdatePanelID
        {
            set
            {
                upgProgress.AssociatedUpdatePanelID = value;
            }
        }
    }
}