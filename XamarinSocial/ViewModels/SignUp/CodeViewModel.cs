using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace XamarinSocial.ViewModels.SignUp
{
    public class CodeViewModel : BaseViewModel<string>
    {
        public CodeViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {

        }

        public override void Prepare(string param)
        {
            if (string.IsNullOrWhiteSpace(param))
            {

                return;
            }
            PhoneNumber = param;
        }

        #region Properties

        private string _phoneNumber;

        public string PhoneNumber {
            get => _phoneNumber;
            set {
                SetProperty(ref _phoneNumber, value);
            }
        }

        #endregion Properties
    }
}
