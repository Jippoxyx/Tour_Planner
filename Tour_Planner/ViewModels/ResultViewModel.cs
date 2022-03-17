using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tour_Planner
{
    public class ResultViewModel : BaseViewModel
    {
        private string resultText = "Hallo, das ist ein Test was hier mal steht :)";

        public string ResultText
        {
            get { return resultText; }
            set
            {
                resultText = value;
                OnPropertyChanged();
            }
        }

        public void DisplayResult(string result)
        {
            this.ResultText = result;
        }
    }
}
