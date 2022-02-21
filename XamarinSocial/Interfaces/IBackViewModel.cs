using MvvmCross.Commands;

namespace XamarinSocial.Interfaces
{
    public interface IBackViewModel
    {
        IMvxCommand BackCommand { get; set; }
    }
}
