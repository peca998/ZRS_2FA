using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.GuiController
{
    public class MainController
    {
        private static MainController? _instance;
        public static MainController Instance => _instance ??= new MainController();
        private FrmMain _form;
        private MainController()
        {
        }
        public void ShowForm(string username)
        {
            _form = new FrmMain();
            _form.Text = $"Main Form - Logged in as {username}";
            _form.ShowDialog();
        }


    }
}
